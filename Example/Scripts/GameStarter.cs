using UnityEngine;
using SimpleUI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private UIFrameConfig uiConfig;
    
    void Start()
    {
        // 通过配置文件注册Screen
        if(uiConfig != null)
        {
            uiManager.IniteUIFrameConfig(uiConfig);
        }        
        
        uiManager.ShowPanel(ScreenIds.TestButtonPanel);
    }
    
}
