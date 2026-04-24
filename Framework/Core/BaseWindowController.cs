using UnityEngine;

namespace SimpleUI
{
    public enum WindowPriority
    {
        Enqueue = 0,       // 先入队不直接显示（不影响当前打开的窗口，等待前一个窗口关闭后再显示）
        Foreground = 1,   // 直接显示（需要立即处理的通知）

    }
    [System.Serializable]
    public class WindowArgs : IWindowArgs
    {
        [SerializeField] protected WindowPriority windowQueuePriority;
        [SerializeField] protected bool hideOnForegroundLost = true; // 是否在失去焦点时隐藏
        [SerializeField] protected bool isPopup = false; // 是否为模态弹窗（即确认取消类弹窗，单独放到PopLayer处理）

        public WindowPriority WindowPriority { 
            get{return windowQueuePriority;} 
            set{ windowQueuePriority = value; } 
        }
        public bool HideOnForegroundLost {
             get{return hideOnForegroundLost;} 
             set{ hideOnForegroundLost = value; }
        }
        public bool IsPopup {
            get{return isPopup;}
            set{ isPopup = value; }
        }

    }
    /// <summary>
    /// 窗口基类（弹窗、设置界面等）
    /// </summary>
    public abstract class BaseWindowController<TArgs> : BaseScreen<TArgs>, IWindow where TArgs : IWindowArgs
    {
        public WindowPriority WindowPriority => ScreenArgs.WindowPriority;
        public bool HideOnForegroundLost => ScreenArgs.HideOnForegroundLost;
        public bool IsPopup => ScreenArgs.IsPopup;
    }
    public abstract class BaseWindowController : BaseWindowController<WindowArgs>
    {
        
    }
}