using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleUI
{
    /// <summary>
    /// UI 管理器 - 唯一的对外 API 入口
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Layers")]
        [SerializeField] private PanelLayer panelLayer;
        [SerializeField] private WindowLayer windowLayer;
        [Header("SO Config")]
        [SerializeField] private UIFrameConfig uiConfig;
        [SerializeField] private bool loadConfigOnAwake = true;
        [Header("For Debug")]
        [SerializeField] private List<WindowConfigEntry> registeredWindows;
        [SerializeField] private List<PanelConfigEntry> registeredPanels;
        public static UIManager Instance {get;private set;} // 单例模式
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            if(loadConfigOnAwake && uiConfig != null) // 当指定配置文件且开启Awake加载时才执行初始加载
            {
                LoadConfig();
            }            
        }

        #region 配置加载

        /// <summary>
        /// 暂时只使用一个配置文件，要么调用该方法加载指定配置，要么loadConfigOnAwake
        /// 如果考虑扩展为多个配置文件管理，这里就需要修改
        /// </summary>
        /// <param name="config"></param>
        public void IniteUIFrameConfig(UIFrameConfig config)
        {
            if (!loadConfigOnAwake) // ，后续可以扩展为多个配置文件管理
            {
                uiConfig = config;
                LoadConfig();
                return;
            }
            Debug.LogWarning($"[UIManager] Setting config 已在Awake时加载！");
        }

        /// <summary>
        /// 加载配置文件
        /// 按照配置设计分别读取窗口和面板的预制体进行注册
        /// 暂不处理自动显示，统一由外部手动初始化显示
        /// </summary>
        public void LoadConfig()
        {
            if (uiConfig == null)
            {
                Debug.LogWarning("[UIManager] No config assigned!");
                return;
            }
            
            Debug.Log($"[UIManager] Loading config: {uiConfig.name}");
            
            // 注册所有窗口Window
            foreach (var entry in uiConfig.windowConfigs)
            {
                if (entry.prefab == null)
                {
                    Debug.LogError($"Window prefab for '{entry.windowId}' is missing!");
                    continue;
                }
                
                RegisterWindowFromConfig(entry);
            }
            
            // 注册所有面板Panel
            foreach (var entry in uiConfig.panelConfigs)
            {
                if (entry.prefab == null)
                {
                    Debug.LogError($"Panel prefab for '{entry.panelId}' is missing!");
                    continue;
                }
                
                RegisterPanelFromConfig(entry);
            }
        }
        #endregion
        
        #region Screen 注册
        /// <summary>
        /// 从配置注册窗口
        /// </summary>
        public void RegisterWindowFromConfig(WindowConfigEntry entry)
        {
            // 实例化窗口预制体
            var instance = Instantiate(entry.prefab, windowLayer.transform);
            var window = instance.GetComponent<IWindow>();
            
            if (window == null)
            {
                Debug.LogError($"Prefab '{entry.windowId}' has no WindowController component!");
                Destroy(instance);
                return;
            }

            windowLayer.RegisterScreen(entry.windowId, window); // 注册到所属Layer，包含Layer的父物体设置
            
            // 注册后隐藏
            if (uiConfig.deactivateOnRegister && instance.activeSelf)
            {
                instance.SetActive(false);
            }
            
            Debug.Log($"[UIManager] Registered window: {entry.windowId}");
            registeredWindows.Add(entry);
        }
        public void RegisterPanelFromConfig(PanelConfigEntry entry)
        {
            var instance = Instantiate(entry.prefab);
            var panel = instance.GetComponent<IPanel>();
            
            if (panel == null)
            {
                Debug.LogError($"Prefab '{entry.panelId}' has no PanelController component!");
                Destroy(instance);
                return;
            }
            
            panelLayer.RegisterScreen(entry.panelId, panel);
            
            if (uiConfig.deactivateOnRegister && instance.activeSelf)
            {
                instance.SetActive(false);
            }
            
            Debug.Log($"[UIManager] Registered panel: {entry.panelId}");
            registeredPanels.Add(entry);
        }

        // public void RegisterPanel(string screenId, IPanel panel)
        // {
        //     panelLayer.RegisterScreen(screenId, panel, panel.transform);
        // }
        
        // public void RegisterWindow(string screenId, BaseWindowController window)
        // {
        //     windowLayer.RegisterScreen(screenId, window, window.transform);
        // }
        #endregion
        

        #region 显隐 API

        // public void ShowSreen(string screenId)
        // {
        //     // 检查是否注册再显示
        //     if(IsScreenRegistered(screenId, out Type type)) {
        //         if(type == typeof(IPanel)) {
        //             ShowPanel(screenId);
        //         } else if(type == typeof(IWindow)) {
        //             OpenWindow(screenId);
        //         }
        //     } else {
        //         Debug.LogError($"Panel or Window {screenId} not registered!");
        //     }
        // }

        public void ShowPanel(string screenId)
        {
            panelLayer.ShowScreenById(screenId); 
        }
        public void ShowPanel<T>(string screenId, T args)where T : IPanelArgs
        {
            panelLayer.ShowScreenById<T>(screenId, args);
        }
        
        public void HidePanel(string screenId)
        {
            panelLayer.HideScreenById(screenId);
        }
        

        public void OpenWindow(string screenId)
        {
            windowLayer.ShowScreenById(screenId);
        }
        public void OpenWindow<T>(string screenId, T args) where T : IWindowArgs
        {
            windowLayer.ShowScreenById<T>(screenId, args);
        }
        
        public void CloseCurrentWindow()
        {
            if(windowLayer.CurrentWindow != null) {
                CloseWindow(windowLayer.CurrentWindow.ScreenId);
            }
        }
        
        public void CloseWindow(string screenId)
        {
            windowLayer.HideScreenById(screenId);
        }

        // public bool IsWindowOpen(string screenId)
        // {
        //     return windowLayer.IsWindowOpen(screenId);
        // }
        #endregion
        

        /// <summary>
        /// 检查是否已从配置加载（用于调试）
        /// </summary>
        public bool IsConfigLoaded { get; private set; }

        public bool IsScreenRegistered(string screenId, out Type type) {
            if (windowLayer.IsScreenRegistered(screenId)) {
                type = typeof(BaseWindowController<WindowArgs>);
                return true;
            }

            if (panelLayer.IsScreenRegistered(screenId)) {
                type = typeof(BasePanelController);
                return true;
            }

            type = null;
            return false;
        }
    }
}