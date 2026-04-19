using UnityEngine;
using UnityEngine.UI;
using SimpleUI;
using System.Collections;
using System;

public class ScorePopupPanel : BasePanelController<ScorePopupArgs>
{
    [SerializeField] private Text messageText;
    [SerializeField] private float autoHideDelay = 1f;
    
    private Coroutine autoHideCoroutine;
    
    protected override void OnScreenArgsSet() // 使用带参数的Show方法时会调用该方法
    {
        messageText.text = CurrentArgs.Message;            
        
        // 自动隐藏
        if (autoHideCoroutine != null)
            StopCoroutine(autoHideCoroutine);
        autoHideCoroutine = StartCoroutine(AutoHide());
    }
    
    private IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(autoHideDelay);
        
        var uiManager = UIManager.Instance;
        if (uiManager != null)
        {
            uiManager.HidePanel(ScreenIds.SCORE_POPUP);
        }
    }
    
    protected override void OnHide()
    {
        if (autoHideCoroutine != null)
        {
            StopCoroutine(autoHideCoroutine);
            autoHideCoroutine = null;
        }
    }

    
}
[Serializable]
public class ScorePopupArgs : PanelArgs
{
    public string Message;
}