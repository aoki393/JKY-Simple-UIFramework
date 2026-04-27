using UnityEngine;

namespace SimpleUI
{
    public enum PanelPriority
    {
        Background = 0,   // 背景层（如地图、特效）
        Normal = 1,       // 普通层（HUD、血条）
        Foreground = 2,   // 前景层（弹窗提示、悬浮窗）
        Tutorial = 3,     // 教程层（引导高亮）
        Blocker = 4,      // 阻断层（全屏遮罩）
    }

    [System.Serializable] 
    public class PanelArgs : IPanelArgs
    {
        protected PanelPriority panelPriority;

        public PanelPriority PanelPriority
        {
            get => panelPriority;
            set => panelPriority = value;
        }
    }

    public abstract class BasePanelController<T> : BaseScreen<T>, IPanel where T : IPanelArgs
    {
        [SerializeField] protected PanelPriority priority = PanelPriority.Normal;
        public PanelPriority Priority => priority;

    }
    public abstract class BasePanelController : BasePanelController<PanelArgs>
    {
        
    }
}