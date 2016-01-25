using System.Runtime.Remoting.Channels;
using AraxisTools;
using TMPro;
using UnityEngine.iOS;
using UnityEngine.UI;

public class GameGUI : GUI<GameGUI>
{
    public Button PauseButton;

    public Button LeftButton;

    public Button RightButton;

    public TextMeshProUGUI Score;

    public TextMeshProUGUI BestScore;

    public override void InitGUI()
    {
        base.InitGUI();
    }

    public override void AddHandlers()
    {
        base.AddHandlers();
        PauseButton.onClick.AddListener(PauseButtonClick);
        LeftButton.onClick.AddListener(LeftButtonClick);
        RightButton.onClick.AddListener(RightButtonClick);
    }

    public override void AddEvents()
    {
        base.AddEvents();
    }

    #region callbacks

    public void PauseButtonClick()
    {
        SceneLoader.LoadLevel(LevelType.MainMenu);
    }

    public void LeftButtonClick()
    {
        Swiper.Instance.ShiftLeft();
    }

    public void RightButtonClick()
    {
        Swiper.Instance.ShiftRight();
    }

    #endregion

    #region events

    public void SetScore()
    {
        
    }

    public void SetBestScore()
    {
        
    }

    #endregion
}