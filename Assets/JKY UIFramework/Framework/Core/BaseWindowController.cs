using UnityEngine;

namespace SimpleUI
{
    public enum WindowPriority
    {
        Enqueue = 0,       // 先入队不直接显示（不影响当前打开的窗口，等待前一个窗口关闭后再显示）
        Foreground = 1,   // 直接显示（需要立即显示）

    }

    /// <summary>
    /// 窗口行为配置 - 在预制体上设置
    /// </summary>
    [System.Serializable]
    public class WindowConfig
    {
        [Header("Window Behavior")]
        [SerializeField] private WindowPriority priority = WindowPriority.Enqueue;
        [SerializeField] private bool hideOnForegroundLost = true;
        [SerializeField] private bool isPopup = false;

        public WindowPriority Priority => priority;
        public bool HideOnForegroundLost => hideOnForegroundLost;
        public bool IsPopup => isPopup;
    }

    /// <summary>
    /// 专注业务参数
    /// </summary>
    [System.Serializable]
    public class WindowArgs : IWindowArgs
    {

    }
    /// <summary>
    /// 窗口基类（弹窗、设置界面等）
    /// </summary>
    public abstract class BaseWindowController<TArgs> : BaseScreen<TArgs>, IWindow where TArgs : IWindowArgs
    {
        [Header("Window Configuration")]
        [SerializeField] private WindowConfig config;
        public WindowConfig Config => config;
        protected override void HierarchyFixOnShow() {
            transform.SetAsLastSibling();
        }

    }
    public abstract class BaseWindowController : BaseWindowController<WindowArgs>
    {
        
    }
}