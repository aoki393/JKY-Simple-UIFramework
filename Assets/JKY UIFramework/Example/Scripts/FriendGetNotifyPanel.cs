using UnityEngine;
using SimpleUI;
using System.Collections;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;

[Serializable]
public class FriendGetNotifyData : PanelArgs
{
    public Sprite friendImage;
    public string friendName;
}
public class FriendGetNotifyPanel : BasePanelController<FriendGetNotifyData>
{
    [SerializeField] private RectTransform toastRect = null;
    [SerializeField] private Image FriendImage;
    [SerializeField] private TextMeshProUGUI NotifyText;
    [SerializeField] private string textContent = " Become Your Friend !";
    [SerializeField] private float toastDuration = 0.5f;
    [SerializeField] private float toastPause = 2f;
    [SerializeField] private Ease toastEase = Ease.Linear;
    private bool isToasting;

    protected override void OnScreenArgsSet()
    {
        FriendImage.sprite = ScreenArgs.friendImage;
        NotifyText.text = "<color=red>"+ScreenArgs.friendName+"</color>" + textContent;
    }
    void Awake() {
        var friendRequest = FindFirstObjectByType<FriendRequestWindow>(FindObjectsInactive.Include);
        friendRequest.OnFriendRequestAccepted.AddListener(OnFriendAdd);
    }

    private void OnFriendAdd() {
        if (isToasting) {
            return;
        }
        StartCoroutine(YieldForDOTween());
    }


    private IEnumerator YieldForDOTween() {
       yield return null;
       
       isToasting = true;
       Sequence seq = DOTween.Sequence();
       seq.Append(toastRect.DOAnchorPosY(0f, toastDuration).SetEase(toastEase));
       seq.AppendInterval(toastPause);
       seq.Append(toastRect.DOAnchorPosY(toastRect.rect.height, toastDuration).SetEase(toastEase));
       seq.OnComplete(() => isToasting = false);

       seq.Play();
    }
}
