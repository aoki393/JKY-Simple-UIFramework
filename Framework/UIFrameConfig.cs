using System.Collections.Generic;
using UnityEngine;

namespace SimpleUI
{
    [CreateAssetMenu(fileName = "UIFrameConfig", menuName = "Simple UI/UI Config")]
    public class UIFrameConfig : ScriptableObject
    {
        [Header("窗口配置")]
        [Tooltip("需要预先注册的窗口预制体")]
        public List<WindowConfigEntry> windowConfigs = new();
        
        [Header("面板配置")]
        [Tooltip("需要预先注册的面板预制体")]
        public List<PanelConfigEntry> panelConfigs = new();
        
        
        [Header("行为设置")]
        [Tooltip("注册后是否自动隐藏屏幕（推荐 true）")]
        public bool deactivateOnRegister = true;
        
    }
    /// <summary>
    /// 窗口配置条目
    /// </summary>
    [System.Serializable]
    public class WindowConfigEntry
    {
        [Tooltip("窗口唯一标识符（建议与预制体同名）")]
        public string windowId;
        
        [Tooltip("窗口预制体")]
        public GameObject prefab;

    }
    
    /// <summary>
    /// 面板配置条目
    /// </summary>
    [System.Serializable]
    public class PanelConfigEntry
    {
        [Tooltip("面板唯一标识符（建议与预制体同名）")]
        public string panelId;
        
        [Tooltip("面板预制体")]
        public GameObject prefab;

        [Tooltip("面板优先级(决定挂载到哪个Layer)")]
        public PanelPriority priority = PanelPriority.Normal;
    }
}