using System.Collections.Generic;
using UnityEngine;

namespace SimpleUI
{
    /// <summary>
    /// 只能有一个窗口处于焦点
    /// 支持窗口抢占显示（需要立即处理的通知等）
    /// 支持排队显示（不干扰当前窗口的显示，等当前窗口关闭后再显示）
    /// </summary>
    public class WindowLayer : UILayer<IWindow>
    {
        public IWindow CurrentWindow { get; private set; }
        private Stack<WindowHistoryEntry> windowHistory = new();
        private Queue<WindowHistoryEntry> windowQueue = new();
        [SerializeField] private PopupWindowLayer popLayer; // 用于处理模态弹窗

#region Override        
        internal override void ShowScreen(IWindow screen)
        {
            ShowScreen<IWindowArgs>(screen, null);
        }

        internal override void ShowScreen<TArgs>(IWindow screen, TArgs args)
        {
            IWindowArgs windowArgs = args as IWindowArgs;

            // 先检查是入队等待显示还是直接显示
            if(ShouldEnqueue(screen,windowArgs))
            {
                EnqueueWindow(screen, windowArgs);
            }
            else
            {
                DoShow(screen, windowArgs);
            }
        }

        internal override void HideScreen(IWindow screen)
        {
            if(CurrentWindow == screen)
            {
                windowHistory.Pop();
                screen.Hide();

                if(screen.Config.IsPopup) // 原版是在关闭Transition结束里执行，暂时没实现
                {
                    popLayer.RefreshDarken(); // 刷新popLayer背景遮罩的显示
                }

                CurrentWindow = null;

                // 队列（等待显示）、历史栈（之前关闭的）
                if (windowQueue.Count > 0)
                {
                    ShowNextInQueue();
                }else if (windowHistory.Count > 0)
                {
                    ShowPreviousInHistory();
                }
            }
            else
            {
                Debug.LogError($"只能关闭最上层窗口，当前尝试关闭窗口 [{screen.ScreenId}]");
            }
        }
        protected sealed override void ReparentScreen(IWindow screen)
        {
            if (screen is IWindow window)
            {
                ReparentWindow(window);
            }
        }
#endregion

#region 内部方法

        private bool ShouldEnqueue(IWindow screen, IWindowArgs windowArgs)
        {
            if (CurrentWindow == null && windowQueue.Count == 0) // 当前没有在显示的窗口，队列中也没有等待显示的窗口
                return false; 
                
            if(screen.Config.Priority == WindowPriority.Enqueue)
                return true;

            return false;
        }

        private void EnqueueWindow(IWindow screen, IWindowArgs args)
        {
            windowQueue.Enqueue(new WindowHistoryEntry(screen, args));
            Debug.Log($"Enqueued window [{screen.ScreenId}]");
        }

        /// <summary>
        /// 显示执行的逻辑：
        /// 根据当前窗口的HideOnForeground属性决定是否隐藏，
        /// 要显示的窗口是Popup类型开启popLayer背景
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="args"></param>
        private void DoShow(IWindow screen, IWindowArgs args)
        {
            // 重复打开检查
            if(CurrentWindow==screen)
            {
                Debug.LogWarning($"Window {screen.ScreenId} is already open! Ignoring duplicate request.");
                return;
            }
            if(CurrentWindow != null && CurrentWindow.Config.HideOnForegroundLost)
            {
                CurrentWindow.Hide();
            }
            
            if(screen.Config.IsPopup)
            {
                popLayer.ShowDarkBG(); // popLayer的遮罩背景激活显示
            }

            windowHistory.Push(new WindowHistoryEntry(screen, args));
            screen.Show(args);

            CurrentWindow = screen;
        }
        private void ShowNextInQueue()
        {
            var next = windowQueue.Dequeue();
            DoShow(next.Window, next.WindowArgs);
        }
        private void ShowPreviousInHistory()
        {
            var previous = windowHistory.Pop();
            DoShow(previous.Window, previous.WindowArgs);
        }

        private void ReparentWindow(IWindow window)
        {
            if(window.Config.IsPopup) // 模态弹窗防置到PopLayer
            {
                popLayer.AddScreen((window as MonoBehaviour).transform);
                return;
            }

            (window as MonoBehaviour).transform.SetParent(transform, false);
        }

#endregion

    }
}