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
        uiManager.ShowPanel(ScreenIds.HUD, new ScoreArgs { InitialScore = 5 });  // 初始分数为 0
    }
    
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     uiManager.OpenWindow(ScreenIds.SETTINGS, PlayerPrefs.GetFloat("Volume", 0.5f));
        // }

        // 按空格增加分数（演示 HUD 和提示面板）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 实际项目中应该通过事件系统，这里简单演示
            AddScoreToHUD(10);
        }
    }
    private void AddScoreToHUD(int value)
    {
        Debug.Log($"[GameStarter] AddScoreToHUD");
        // 通过 UI 事件系统会更合适，这里简化处理
        var hud = FindFirstObjectByType <HUDPanel>();
        if (hud != null)
        {
            Debug.Log($"[GameStarter] Adding {value} score to HUD");
            hud.AddScore(value);
        }
        // var hudGO = GameObject.Find(ScreenIds.HUD);
        // if (hudGO != null)
        // {
        //     Debug.Log($"[GameStarter] Adding {value} score to HUD");
        //     var hud = hudGO.GetComponent<HUDPanel>();
        //     if (hud != null)
        //     {
        //         hud.AddScore(value);
        //     }
        // }
    }
}
