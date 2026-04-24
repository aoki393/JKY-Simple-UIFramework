using UnityEngine;
using UnityEngine.UI;
using SimpleUI;

public class ShopWindow : BaseWindowController
{
    [SerializeField] private Button btnClose;

    void Start()
    {
        btnClose.onClick.AddListener(() => {
            UIManager.Instance.CloseWindow(screenId);
        });
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        btnClose.onClick.RemoveAllListeners();
    }

}
