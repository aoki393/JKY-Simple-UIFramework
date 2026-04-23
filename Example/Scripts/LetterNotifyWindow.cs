using UnityEngine;
using SimpleUI;
using UnityEngine.UI;
public class LetterNotifyWindow : BaseWindowController<WindowArgs>
{
    [SerializeField] private Button closeButton;
    void Start()
    {
        closeButton.onClick.AddListener(() => {
            UIManager.Instance.CloseWindow(screenId);
        });
    }
}
