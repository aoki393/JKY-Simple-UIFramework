using UnityEngine.UI;
using SimpleUI;
using UnityEngine;

public class SettingsWindow : BaseWindowController
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button closeButton;
    
    private void Start()
    {
        closeButton.onClick.AddListener(Close);
    }
    
    private void Close()
    {
        UIManager.Instance.CloseCurrentWindow();
    }
    
    protected override void OnScreenArgsSet()
    {
        // 恢复音量设置
        // if (CurrentArgs is float savedVolume)
        // {
        //     volumeSlider.value = savedVolume;
        // }
    }
    
    protected override void OnHide()
    {
        // 保存设置
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}