using UnityEngine;
using SimpleUI;
using UnityEngine.UI;
public class FriendRequestWindow : BaseWindowController
{
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button rejectButton;
    void Start()
    {
        acceptButton.onClick.AddListener(AcceptRequest);
        rejectButton.onClick.AddListener(RejectRequest);
    }

    private void AcceptRequest()
    {
        UIManager.Instance.CloseWindow(ScreenIds.FriendRequest);
         // 这里可以添加接受好友请求的逻辑
         // 比如：更新好友列表，显示通知等
    }

    private void RejectRequest()
    {
        UIManager.Instance.CloseWindow(ScreenIds.FriendRequest);
    }
}
