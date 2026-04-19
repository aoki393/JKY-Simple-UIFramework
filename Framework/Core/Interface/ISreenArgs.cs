namespace SimpleUI
{
    /// <summary>
    /// UI 参数的基接口（空标记接口，用于类型约束）
    /// </summary>
    public interface IScreenArgs { }
    
    /// <summary>
    /// 窗口参数的基接口
    /// </summary>
    public interface IWindowArgs : IScreenArgs { }
    
    /// <summary>
    /// 面板参数的基接口
    /// </summary>
    public interface IPanelArgs : IScreenArgs { }
}
