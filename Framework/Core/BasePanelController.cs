using UnityEngine;

namespace SimpleUI
{
    /// <summary>
    /// 面板优先级：数值越大，渲染层级越靠前（视觉上越靠上）
    /// </summary>
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
        // 可以根据需要添加通用的面板参数字段
    }
    /// <summary>
    /// 面板基类（HUD、血条、小地图等）
    /// </summary>
    public abstract class BasePanelController<T> : BaseScreen<T>, IPanel where T : IPanelArgs
    {
        [SerializeField] protected PanelPriority priority = PanelPriority.Normal;
        
        /// <summary>
        /// 面板优先级（可在 Inspector 配置）
        /// </summary>
        public PanelPriority Priority => priority;

    }
    public abstract class BasePanelController : BasePanelController<PanelArgs>
    {
        
    }
}