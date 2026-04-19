using System.Collections.Generic;
using UnityEngine;

namespace SimpleUI
{
    /// <summary>
    /// 窗口层：一次只显示一个，支持关闭当前窗口
    /// </summary>
    public class WindowLayer : UILayer<IWindow>
    {
        public IWindow CurrentWindow { get; private set; }
        // private Stack<WindowHistoryEntry> history = new Stack<WindowHistoryEntry>();

#region Override        
        internal override void ShowScreen(IWindow screen)
        {
            screen.Show();
        }

        internal override void ShowScreen<TArgs>(IWindow screen, TArgs args)
        {
            screen.Show(args);
        }

        internal override void HideScreen(IWindow screen)
        {
            screen.Hide();
        }
        protected sealed override void ReparentScreen(IWindow screen)
        {
            if (screen is IWindow window)
            {
                ReparentWindow(window);
            }
        }
#endregion

        private void ReparentWindow(IWindow window)
        {
            if (window == null) return;
            
            var windowBehaviour = window as MonoBehaviour;
            if (windowBehaviour == null) return;

            windowBehaviour.transform.SetParent(transform, false);
        }
        
        // internal void OpenWindow(string screenId)
        // {
        //     // 注册检查
        //     // if (!TryGetScreen(screenId, out var screen))
        //     // {
        //     //     Debug.LogError($"Window {screenId} not registered!");
        //     //     return;
        //     // }            
            
        //     // var window = screen as IWindow;

        //     // // 类型检查
        //     // if (window == null)
        //     // {
        //     //     Debug.LogError($"Screen {screenId} is not a Window!");
        //     //     return;
        //     // }

        //     // // 防止重复打开
        //     // if (IsWindowOpen(screenId))
        //     // {
        //     //     Debug.LogWarning($"Window {screenId} is already open! Ignoring duplicate request.");
        //     //     return;
        //     // }
            
        //     // // 核心逻辑：打开新窗口前，把当前窗口压入历史栈
        //     // if (CurrentWindow != null && CurrentWindow.IsVisible)
        //     // {
        //     //     Debug.Log($"Pushing current window {CurrentWindow.ScreenId} to history");
        //     //     // 保存当前窗口的信息到历史栈
        //     //     history.Push(new WindowHistoryEntry(CurrentWindow, CurrentWindow.LastShowData));
        //     //     // 隐藏当前窗口
        //     //     CurrentWindow.Hide();
        //     // }
            
        //     // // 显示新窗口
        //     // CurrentWindow = window;
        //     // window.Show();
        // }
        
        // internal void CloseCurrentWindow()
        // {
        //     // if (CurrentWindow == null)
        //     // {
        //     //     Debug.LogWarning("No window is currently open!");
        //     //     return;
        //     // }
            
        //     // // 隐藏当前窗口
        //     // CurrentWindow.Hide();
            
        //     // // 从历史栈中取出上一个窗口
        //     // if (history.Count > 0)
        //     // {
        //     //     var previous = history.Pop();
        //     //     CurrentWindow = previous.Window;
        //     //     // 恢复显示，使用之前保存的数据
        //     //     CurrentWindow.Show(previous.Data);
        //     // }
        //     // else
        //     // {
        //     //     CurrentWindow = null;
        //     // }
        // }
        
        // internal void CloseWindow(string screenId)
        // {
        //     if (CurrentWindow != null && CurrentWindow.ScreenId == screenId)
        //     {
        //         CloseCurrentWindow();
        //     }else
        //     {
        //         Debug.LogWarning($"Can only close the topmost window. Current: {CurrentWindow?.ScreenId}, Requested: {screenId}");
        //     }
        // }

        // /// <summary>
        // /// 清空历史栈（用于切换场景或重置）
        // /// </summary>
        // internal void ClearHistory()
        // {
        //     history.Clear();
        // }

        // internal bool IsWindowOpen(string screenId)
        // {
        //     return CurrentWindow != null && CurrentWindow.ScreenId == screenId && CurrentWindow.IsVisible;
        //     // return CurrentWindow != null && CurrentWindow.ScreenId == screenId;
        // }

        // /// <summary>
        // /// 获取历史栈深度（用于调试）
        // /// </summary>
        // public int HistoryDepth => history.Count;


    }
}