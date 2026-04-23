using System;

namespace SimpleUI
{
    /// <summary>
    /// 所有 UI 屏幕的基础接口（非泛型，用于集合存储）
    /// </summary>
    public interface IUIScreen
    {
        string ScreenId { get; set; }
        bool IsVisible { get; }
        void Show();        
        void Show(IScreenArgs args);        
        void Hide(bool animate = true);
        
        // 屏幕销毁时通知管理器
        event Action<IUIScreen> OnDestroyed;
    }

    
    /// <summary>
    /// 窗口专用接口
    /// </summary>
    public interface IWindow : IUIScreen
    {
        WindowPriority WindowPriority { get; }
        bool HideOnForegroundLost { get; }
        bool IsPopup { get; }
    }
    
    /// <summary>
    /// 面板专用接口
    /// </summary>
    public interface IPanel : IUIScreen
    {
        PanelPriority Priority { get; }
    }

    
}