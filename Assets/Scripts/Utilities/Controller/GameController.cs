using AraxisTools;
using Facebook.Unity;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameController : Controller<GameController>
{
    public ActionsQueue MainQueue;

    public Texture2D DefaultCursorTexture;

    public Texture2D HandCursorTexture;

    public static string UserIdentifier
    {
        get
        {
            if (FB.IsLoggedIn)
            {
                return AccessToken.CurrentAccessToken.UserId;
            }
            else
            {
                return _userIdentifier;
            }
        }
        set { _userIdentifier = value; }
    }

    private static string _userIdentifier;

    public override void Start()
    {
        base.Start();

        MainQueue = new ActionsQueue();
        StartCoroutine(MainQueue.ProcessQueue());

        RestoreCursor();
    }

    protected override void ParseConfig()
    {
        JSONNode json = JSON.Parse(Config.text);

        _userIdentifier = json["user_name"] + json["user_name_index"];
    }

    public static void AddAction(Action action)
    {
        if (Instance != null)
        {
            Instance.MainQueue.AddAction(action);
        }
    }

    public static string GetGuestIdentifier()
    {
        return _userIdentifier;
    }

    public static void SetCursor()
    {
#if UNITY_WEBPLAYER || UNITY_WEBGL
        if (Instance != null)
        {
            Cursor.SetCursor(Instance.HandCursorTexture, new Vector2(17.0f, 0), CursorMode.ForceSoftware);
        }
#endif
    }

    public static void RestoreCursor()
    {
#if UNITY_WEBPLAYER || UNITY_WEBGL
        if (Instance != null)
        {
            Cursor.SetCursor(Instance.DefaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
#endif
    }
}
