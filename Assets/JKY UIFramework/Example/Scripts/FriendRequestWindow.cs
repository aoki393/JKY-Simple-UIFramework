using UnityEngine;
using SimpleUI;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Events;


[Serializable]
public class FriendRequestArgs : IWindowArgs
{
    public string friendName;
    public Sprite friendImage;
}
public class FriendRequestWindow : BaseWindowController<FriendRequestArgs>
{
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button rejectButton;
    [SerializeField] private TextMeshProUGUI friendNameText;
    [SerializeField] private string context = " Want to Be your Friend...";
    public UnityEvent OnFriendRequestAccepted;
    protected override void OnScreenArgsSet()
    {
        friendNameText.text = "<color=#FF0000>"+ScreenArgs.friendName+"</color>" + context;
    }
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
        UIManager.Instance.ShowPanel(ScreenIds.FriendGetNotify, new FriendGetNotifyData{
            friendName = ScreenArgs.friendName,
            friendImage = ScreenArgs.friendImage
        });
        OnFriendRequestAccepted?.Invoke();
    }

    private void RejectRequest()
    {
        UIManager.Instance.CloseWindow(ScreenIds.FriendRequest);
    }

    
}
