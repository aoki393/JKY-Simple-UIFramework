namespace SimpleUI
{
    /// <summary>
    /// 历史栈条目：保存窗口及其数据
    /// </summary>
    public struct WindowHistoryEntry
    {
        public IWindow Window { get; }
        public object Data { get; }
        
        public WindowHistoryEntry(IWindow window, object data)
        {
            Window = window;
            Data = data;
        }
    }
}