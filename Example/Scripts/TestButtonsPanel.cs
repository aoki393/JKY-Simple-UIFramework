using UnityEngine;
using SimpleUI;
using UnityEngine.UI;

public class TestButtonsPanel : BasePanelController
{
    public Button btnShowShop;
    public Button btnSendFriendRequest;
    public Button btnSendLetterNotify;
    public FriendData friendData;
    private int index;
    void Start()
    {
        btnShowShop.onClick.AddListener(() => {
            UIManager.Instance.OpenWindow(ScreenIds.Shop);
        });

        btnSendFriendRequest.onClick.AddListener(SendTestFriendRequest);

        btnSendLetterNotify.onClick.AddListener(() => {
            UIManager.Instance.OpenWindow(ScreenIds.LetterNotify);
        });
    }
    private void SendTestFriendRequest()
    {
        if(index >= friendData.friends.Length)
        {
            index = 0;
        }
        UIManager.Instance.OpenWindow(ScreenIds.FriendRequest,
            new FriendRequestArgs{
                friendName=friendData.friends[index].FriendName,
                friendImage=friendData.friends[index].headImage
        });
        index++;
    }

}
