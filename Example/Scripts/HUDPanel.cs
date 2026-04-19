using UnityEngine.UI;
using SimpleUI;
using UnityEngine;
using System;

public class HUDPanel : BasePanelController<ScoreArgs>
{
    [SerializeField] private Text scoreText;
    private int currentScore;
    
    protected override void OnScreenArgsSet()
    {
        if(CurrentArgs != null)
        {
            currentScore = CurrentArgs.InitialScore;            
        }
        UpdateScoreUI();
    }
    
    public void AddScore(int value)
    {
        currentScore += value;
        UpdateScoreUI();

        // 显示飘字
        UIManager.Instance.ShowPanel(ScreenIds.SCORE_POPUP, new ScorePopupArgs { Message = $"Score +{value}" });
    }
    
    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {currentScore}";
    }    
}
[Serializable]
public class ScoreArgs : PanelArgs
{
    public int InitialScore;
}