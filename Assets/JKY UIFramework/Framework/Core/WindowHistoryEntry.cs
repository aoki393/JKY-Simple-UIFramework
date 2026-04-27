namespace SimpleUI
{
    /// <summary>
    /// 历史栈条目：保存窗口及其数据
    /// </summary>
    public struct WindowHistoryEntry
    {
        public IWindow Window { get; }
        public IWindowArgs WindowArgs { get; }
        
        public WindowHistoryEntry(IWindow window, IWindowArgs windowArgs)
        {
            Window = window;
            WindowArgs = windowArgs;
        }
    }
}