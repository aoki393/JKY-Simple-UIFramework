using UnityEngine;
using UnityEngine.UI;
using SimpleUI;

public class ShopWindow : BaseWindowController
{
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnBuy;

    void Start()
    {
        btnClose.onClick.AddListener(() => {
            UIManager.Instance.CloseWindow(screenId);
        });
        btnBuy.onClick.AddListener(() => {
            UIManager.Instance.OpenWindow(ScreenIds.PopupConfirm, new PopupConfirmArgs { 
                Message = "Are you sure you want to buy this item?" 
                , OnYes = () => {
                    Debug.Log("Item bought");
                }
                , OnNo = () => {
                    Debug.Log("Item buy canceled");
                }
            }); 
        });
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        btnClose.onClick.RemoveAllListeners();
        btnBuy.onClick.RemoveAllListeners();
    }

}
