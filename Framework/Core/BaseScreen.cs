using System;
using UnityEngine;

namespace SimpleUI
{
    public abstract class BaseScreen<TArgs> : MonoBehaviour, IUIScreen where TArgs : IScreenArgs
    {
        [SerializeField] protected string screenId;
    
        public string ScreenId 
        { 
            get => screenId;
            set => screenId = value;
        }        
        public bool IsVisible { get; private set; }
        protected TArgs CurrentArgs { get; private set; }
        public event Action<IUIScreen> OnDestroyed;
        public virtual void Show()
        {
            gameObject.SetActive(true);
            IsVisible = true;
        }
        
        public virtual void Show(IScreenArgs args)
        {
            // 检查参数类型是否匹配
            if(args is TArgs typeArgs)
            {
                CurrentArgs = typeArgs;
            }
            else
            {
                Debug.LogError($"Screen {screenId} requires {typeof(TArgs)} args, but got {args.GetType()}");
            }           

            gameObject.SetActive(true);
            IsVisible = true;
            OnScreenArgsSet(); // 有些操作比如开启协程需要gameObject处于激活状态，所以放在SetActive后调用            
        }

        public virtual void Hide(bool animate = true)
        {
            gameObject.SetActive(false);
            IsVisible = false;
            OnHide();
        }
        
        protected virtual void OnHide() { }

        protected virtual void OnScreenArgsSet() { }
        
        protected virtual void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

    }
}