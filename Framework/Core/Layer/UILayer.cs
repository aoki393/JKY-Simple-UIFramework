using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleUI
{
    /// <summary>
    /// UI 层级基类
    /// </summary>
    public abstract class UILayer<TScreen> : MonoBehaviour where TScreen : IUIScreen
    {
        protected Dictionary<string, TScreen> registeredScreens = new();
        internal abstract void ShowScreen(TScreen screen);
        internal abstract void ShowScreen<TArgs>(TScreen screen, TArgs args) where TArgs : IScreenArgs;
        internal abstract void HideScreen(TScreen screen);
        protected abstract void ReparentScreen(TScreen screen);
        
        internal void RegisterScreen(string screenId, TScreen screen)
        {
            if (registeredScreens.ContainsKey(screenId))
            {
                Debug.LogError($"Screen {screenId} already registered!");
                return;
            }
            
            screen.ScreenId = screenId;
            screen.OnDestroyed += OnScreenDestroyed;
            registeredScreens.Add(screenId, screen);
            
            ReparentScreen(screen); // 设置父对象为对应Layer
        }
        
        

        internal void UnregisterScreen(string screenId)
        {
            if (registeredScreens.TryGetValue(screenId, out var screen))
            {
                screen.OnDestroyed -= OnScreenDestroyed;
                registeredScreens.Remove(screenId);
                // OnAfterUnregister(screen);
            }
        }
        
        // protected virtual void OnAfterRegister(IUIScreen screen) { }
        // protected virtual void OnAfterUnregister(IUIScreen screen) { }
        
        private void OnScreenDestroyed(IUIScreen screen)
        {
            if (!string.IsNullOrEmpty(screen.ScreenId) && registeredScreens.ContainsKey(screen.ScreenId))
            {
                UnregisterScreen(screen.ScreenId);
            }
        }

        internal bool IsScreenRegistered(string screenId) => registeredScreens.ContainsKey(screenId);

        /// <summary>
        /// 通过ID显示Screen，无参数版本
        /// </summary>
        /// <param name="screenId"></param>
        internal void ShowScreenById(string screenId)
        {
            if (registeredScreens.TryGetValue(screenId, out var screen))
            {
                ShowScreen(screen);
            }
            else
            {
                Debug.LogError($"Screen {screenId} not registered to this layer!");
            }
        }
        /// <summary>
        /// 传参到Screen的版本
        /// </summary>
        /// <typeparam name="TArgs"></typeparam>
        /// <param name="screenId"></param>
        /// <param name="args"></param>
        internal void ShowScreenById<TArgs>(string screenId, TArgs args) where TArgs : IScreenArgs
        {
            if (registeredScreens.TryGetValue(screenId, out var screen))
            {
                ShowScreen(screen, args);
            }
            else
            {
                Debug.LogError($"Screen {screenId} not registered to this layer!");
            }
        }
        internal void HideScreenById(string screenId)
        {
            if (registeredScreens.TryGetValue(screenId, out var screen))
            {
                HideScreen(screen);
            }else
            {
                Debug.LogError($"Screen {screenId} not registered to this layer!");
            }
        }

    }
}