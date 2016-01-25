using UnityEngine;
using System.Collections;
using AraxisTools;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadLevel(LevelType levelType)
    {
        AsyncOperation asyncOperation = null;

        switch (levelType)
        {
            case LevelType.MainMenu:
                asyncOperation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
                break;
            case LevelType.Game:
                asyncOperation = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
                break;
            case LevelType.GameOver:
                asyncOperation = SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
                break;
            case LevelType.Options:
                asyncOperation = SceneManager.LoadSceneAsync("Options", LoadSceneMode.Single);
                break;
            case LevelType.Leaderboard:
                asyncOperation = SceneManager.LoadSceneAsync("Leaderboard", LoadSceneMode.Single);
                break;
        }

        if (asyncOperation != null)
        {
            GameController.AddAction(new LoadLevelEvent(asyncOperation));
        }
        else
        {
            Debug.LogError("Scene of type " + levelType + " missed!");
        }
    }
}

public enum LevelType
{
    MainMenu, Game, GameOver, Options, Leaderboard
}

public class LoadLevelEvent : Action
{
    private AsyncOperation _levelLoading;

    public LoadLevelEvent(AsyncOperation levelLoading)
    {
        _levelLoading = levelLoading;
    }

    public override IEnumerable NextFrame()
    {
        OnStart();

        while (!_levelLoading.isDone)
        {
            yield return null;
        }

        OnFinish();
    }
}