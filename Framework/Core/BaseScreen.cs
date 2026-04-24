using System;
using UnityEngine;

namespace SimpleUI
{
    public abstract class BaseScreen<TArgs> : MonoBehaviour, IUIScreen where TArgs : IScreenArgs
    {
        [SerializeField] protected string screenId;
        [Header("Screen Args Settings")]
        [SerializeField] private TArgs screenArgs; // 用于在Inspector中显示
        // [SerializeField] private TransitionComponent animIn; // 动画组件
        // [SerializeField] private TransitionComponent animOut; // 动画组件
        public Action<IUIScreen> InTransitionFinished { get; set; }
        public Action<IUIScreen> OutTransitionFinished { get; set; }
    
        public string ScreenId 
        { 
            get => screenId;
            set => screenId = value;
        }        
        public bool IsVisible { get; private set; }
        protected TArgs ScreenArgs 
        { 
            get { return screenArgs;} 
            set { screenArgs = value;} 
        }
        public event Action<IUIScreen> OnDestroyed;
        public virtual void Show()
        {
            Show(null);
        }
        
        public virtual void Show(IScreenArgs args)
        {
            if (args != null)
            {
                if(args is TArgs typeArgs)
                {
                    ScreenArgs = typeArgs;
                }
                else
                {
                    Debug.LogError($"Screen {screenId} requires {typeof(TArgs)} args, but got {args.GetType()}");
                }
            }

            OnScreenArgsSet(); // 具体子类在这里使用参数进行界面更新等操作

            if (!gameObject.activeSelf)
            {
                DoActiveSet(OnTransitionInFinished, true);
            }
            else
            {
                // 窗口恢复但HideOnForeground配置不勾选时会这样
                // Debug.LogWarning($"Screen {screenId} is already active. Updating args without transition!");
                InTransitionFinished?.Invoke(this); // 直接调用完成回调
            }     
        }

        public virtual void Hide(bool animate = true)
        {
            DoActiveSet(OnTransitionOutFinished, false);
            OnHide();
        }
        
        protected virtual void OnHide() { }

        /// <summary>
        /// 子类重写该方法使用传入的参数进行界面更新操作
        /// </summary>
        protected virtual void OnScreenArgsSet() { }
        
        protected virtual void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        private void DoActiveSet(Action callWhenFinished, bool isVisible)
        {
            gameObject.SetActive(isVisible);
            callWhenFinished?.Invoke();
        }

        private void OnTransitionInFinished()
        {
            IsVisible = true;
            InTransitionFinished?.Invoke(this);
        }

        private void OnTransitionOutFinished()
        {
            IsVisible = false;
            gameObject.SetActive(false);
            OutTransitionFinished?.Invoke(this);
        }

        // private void DoAnimation(TransitionComponent caller, Action callWhenFinished, bool isVisible)
        // {
        //     if (caller == null)
        //     {
        //         gameObject.SetActive(isVisible);
        //         if (callWhenFinished != null)
        //         {
        //             callWhenFinished();
        //         }
        //     }
        //     else
        //     {
        //         if (isVisible && !gameObject.activeSelf)
        //         {
        //             gameObject.SetActive(true);
        //         }

        //         caller.Animate(transform, callWhenFinished);
        //     }
        // }


    }
}