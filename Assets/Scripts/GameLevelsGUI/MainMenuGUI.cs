using UnityEngine.UI;

public class MainMenuGUI : GUI<MainMenuGUI>
{
    public Button PlayButton;

    public Button LeaderboardButton;

    public Button OptionsButton;

    public override void AddHandlers()
    {
        base.AddHandlers();
        PlayButton.onClick.AddListener(PlayButtonClick);
        LeaderboardButton.onClick.AddListener(LeaderboardButtonClick);
        OptionsButton.onClick.AddListener(OptionsButtonClick);
    }

    #region callbacks

    public void PlayButtonClick()
    {
        SceneLoader.LoadLevel(LevelType.Game);
    }

    public void LeaderboardButtonClick()
    {
        SceneLoader.LoadLevel(LevelType.Leaderboard);
    }

    public void OptionsButtonClick()
    {
        SceneLoader.LoadLevel(LevelType.Options);
    }

    #endregion
}
