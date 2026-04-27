using System.Collections.Generic;
using UnityEngine;

namespace SimpleUI
{
    /// <summary>
    /// 面板层：简单显示/隐藏，支持多个同时显示
    /// </summary>
    internal class PanelLayer : UILayer<IPanel>
    {
        [System.Serializable]
        public class PanelPriorityParent
        {
            [Tooltip("优先级类型")]
            public PanelPriority Priority;
            
            [Tooltip("该优先级面板的父节点（Transform）")]
            public Transform TargetParent;
        }

        [Tooltip("Layer配置")]
        [SerializeField] private List<PanelPriorityParent> priorityParents = new(); // 根据需要在Inspector中添加不同的 Layer
        private Dictionary<PanelPriority, Transform> priorityLookup; // 存储Priority枚举对应的Transform
        
        private void Awake()
        {
            BuildPriorityLookup();
        }

#region Override

        /// <summary>
        /// 面板放到到对应的 Layer下
        /// </summary>
        /// <param name="screen"></param>
        protected sealed override void ReparentScreen(IPanel screen)
        {
            if (screen is IPanel panel)
            {
                ReparentPanel(panel);
            }else
            {
                Debug.LogError($"Attempted to reparent a non-panel screen: {screen.ScreenId}");
            }
        }
        internal override void ShowScreen(IPanel screen)
        {
            screen.Show();
        }

        internal override void ShowScreen<TArgs>(IPanel screen, TArgs args)
        {
            screen.Show(args);
        }

        internal override void HideScreen(IPanel screen)
        {
            screen.Hide();
        }
#endregion


#region 私有方法        
        /// <summary>
        /// 将List中配置的各种Layer存储到字典中，
        /// 用于后续根据Priority枚举查找到对应Transform进行挂载
        /// </summary>
        private void BuildPriorityLookup()
        {
            priorityLookup = new Dictionary<PanelPriority, Transform>();
            foreach (var entry in priorityParents)
            {
                if (entry.TargetParent != null)
                {
                    priorityLookup[entry.Priority] = entry.TargetParent;
                }
            }
        }

        /// <summary>
        /// 第一次注册时调用，之后不允许修改
        /// 暂时不考虑动态修改Layer的情况
        /// </summary>
        /// <param name="panel"></param>        
        private void ReparentPanel(IPanel panel)
        {
            // if (panel == null) return;            
            var panelBehaviour = panel as MonoBehaviour;
            if (panelBehaviour == null) return;
            
            Transform targetParent = GetParentForPriority(panel.Priority);
            panelBehaviour.transform.SetParent(targetParent, false);
        }
        
        private Transform GetParentForPriority(PanelPriority priority)
        {
            if (priorityLookup != null && priorityLookup.TryGetValue(priority, out var parent))
            {
                return parent;
            }
            // 默认使用 PanelLayer 自身作为父节点
            return transform;
        }

#endregion

        
        // internal bool IsPanelVisible(string screenId)
        // {
        //     if (registeredScreens.TryGetValue(screenId, out var panel)) {
        //         return panel.IsVisible;
        //     }

        //     return false;
        // }

        

        
    }
}