using UnityEngine;
using SimpleUI;
using UnityEngine.UI;

/// <summary>
/// 测试用，不作为实际UI继承框架使用
/// </summary>
public class TestButtonsPanel : MonoBehaviour
{
    public Button btnShowShop;
    public Button btnSendFriendRequest;
    public Button btnSendLetterNotify;
    void Start()
    {
        btnShowShop.onClick.AddListener(() => {
            UIManager.Instance.OpenWindow(ScreenIds.Shop);
        });
        btnSendFriendRequest.onClick.AddListener(() => {
            UIManager.Instance.OpenWindow(ScreenIds.FriendRequest);
        });
        btnSendLetterNotify.onClick.AddListener(() => {
            UIManager.Instance.OpenWindow(ScreenIds.LetterNotify);
        });
    }

}
