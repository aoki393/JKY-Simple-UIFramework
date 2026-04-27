using UnityEngine;
using SimpleUI;
using System;
using TMPro;
using UnityEngine.UI;
[Serializable]
public class PopupConfirmArgs : IWindowArgs
{
    public string Message;
    public Action OnYes;
    public Action OnNo;
}
public class PopupConfirmWindow : BaseWindowController<PopupConfirmArgs>
{
    public TextMeshProUGUI MessageText;
    public Button btnConfirm;
    public Button btnCancel;
    void Start()
    {
        btnConfirm.onClick.AddListener(OnYes);
        btnCancel.onClick.AddListener(OnNo);
    }
    protected override void OnScreenArgsSet()
    {
        MessageText.text = ScreenArgs.Message;
    }
    void OnYes()
    {
        ScreenArgs.OnYes?.Invoke();
        UIManager.Instance.CloseCurrentWindow();
    }
    void OnNo()
    {
        ScreenArgs.OnNo?.Invoke();
        UIManager.Instance.CloseCurrentWindow();
    }

}
