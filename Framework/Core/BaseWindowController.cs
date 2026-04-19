namespace SimpleUI
{
    /// <summary>
    /// 窗口基类（弹窗、设置界面等）
    /// </summary>
    public abstract class BaseWindowController<TArgs> : BaseScreen<TArgs>, IWindow where TArgs : IWindowArgs
    {
        // private object lastShowData;
        
        // public object LastShowData => lastShowData;
        
    }
    public abstract class BaseWindowController : BaseWindowController<IWindowArgs>
    {
    }
}