using System.Collections.Generic;
using UnityEngine;

namespace SimpleUI
{
    /// <summary>
    /// 模态弹窗层：专门处理模态弹窗，支持半透明背景遮罩
    /// </summary>
    internal class PopupWindowLayer : MonoBehaviour
    {
        public GameObject darkBackground; // 用于模态效果的半透明背景
        private List<GameObject> containedScreens = new List<GameObject>();

        public void AddScreen(Transform screenRectTransform)
        {
            screenRectTransform.SetParent(transform, false);
            containedScreens.Add(screenRectTransform.gameObject);
        }
        public void RefreshDarken() {
            for (int i = 0; i < containedScreens.Count; i++) {
                if (containedScreens[i] != null) {
                    if (containedScreens[i].activeSelf) {
                        darkBackground.SetActive(true);
                        return;
                    }
                }
            }

            darkBackground.SetActive(false);
        }

        public void ShowDarkBG()
        {
            if (darkBackground != null)
                darkBackground.SetActive(true);
        }
    }
}