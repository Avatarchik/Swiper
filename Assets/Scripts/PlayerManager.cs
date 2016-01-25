using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AraxisTools.Json;
using Facebook.Unity;
using Parse;
using SimpleJSON;
using Action = System.Action;

public static class PlayerManager
{
    #region public fields

    public static bool LoginComplete = false;

    public static bool Music
    {
        get
        {
            return GetMusic();
        }
        set
        {
            SetMusic(value);
            OnMusicChange();
        }
    }

    public static bool Sound
    {
        get
        {
            return GetSound();
        }
        set
        {
            SetSound(value);
            OnSoundChange();
        }
    }

    public static LoginType LoginType
    {
        get
        {
            return _loginType;
        }
        set
        {
            _loginType = value;
            OnLoginTypeChange();
        }
    }

    public static Gender Gender
    {
        get
        {
            return _gender;
        }
        set
        {
            _gender = value;
            OnGenderChange();
        }
    }

    public static Language Language
    {
        get
        {
            return _language;
        }
        set
        {
            _language = value;
            OnLanguageChange();
        }
    }

    public static string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
            OnNameChange();
        }
    }

    public static string Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
            OnIdChange();
        }
    }

    public static int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            OnScoreChange();
        }
    }

    public static int MaxScore
    {
        get
        {
            return _maxScore;
        }
        set
        {
            _maxScore = value;
            OnMaxScoreChange();
        }
    }

    #endregion

    #region private fields

    private static string _saveName = "PlayerData";

    private static LoginType _loginType;

    private static Gender _gender;

    private static Language _language;

    private static string _name;

    private static string _id;

    private static int _score;

    private static int _maxScore;

    #endregion

    #region events

    public static event Action MusicChange;

    public static event Action SoundChange;

    public static event Action LoginTypeChange;

    public static event Action GenderChange;

    public static event Action LanguageChange;

    public static event Action NameChange;

    public static event Action IdChange;

    public static event Action ScoreChange;

    public static event Action MaxScoreChange;

    public static event Action<bool> DataLoaded;

    public static event Action DataSaved;

    #endregion

    #region events invocators

    private static void OnMusicChange()
    {
        var handler = MusicChange;
        if (handler != null)
        {
            handler();
        }

        if (Music)
        {
            MusicController.EnableMusic();
        }
        else
        {
            MusicController.DisableMusic();
        }
    }

    private static void OnSoundChange()
    {
        var handler = SoundChange;
        if (handler != null)
        {
            handler();
        }

        if (Sound)
        {
            SoundController.EnableSound();
        }
        else
        {
            SoundController.DisableSound();
        }
    }

    private static void OnLoginTypeChange()
    {
        var handler = LoginTypeChange;
        if (handler != null)
        {
            handler();
        }
    }

    private static void OnGenderChange()
    {
        var handler = GenderChange;
        if (handler != null)
        {
            handler();
        }
    }

    private static void OnLanguageChange()
    {
        var handler = LanguageChange;
        if (handler != null)
        {
            handler();
        }
    }

    private static void OnNameChange()
    {
        var handler = NameChange;
        if (handler != null)
        {
            handler();
        }
    }

    private static void OnIdChange()
    {
        var handler = IdChange;
        if (handler != null)
        {
            handler();
        }
    }


    private static void OnScoreChange()
    {
        var handler = ScoreChange;
        if (handler != null)
        {
            handler();
        }
    }

    private static void OnMaxScoreChange()
    {
        var handler = MaxScoreChange;
        if (handler != null)
        {
            handler();
        }
    }

    private static void OnDataLoaded(bool loaded)
    {
        var handler = DataLoaded;
        if (handler != null)
        {
            handler(loaded);
        }
    }

    private static void OnDataSaved()
    {
        var handler = DataSaved;
        if (handler != null)
        {
            handler();
        }
    }

    #endregion

    public static void SaveData(string userIdentifier)
    {
        var root = new JsonRoot();
        root.AddObjects(new JsonValue("MaxScore", MaxScore));

        string saveString = root.ToString();
        PlayerPrefs.SetString("login_type", _loginType.ToString());
        PlayerPrefs.SetString(_saveName + "_" + userIdentifier, saveString);
        PlayerPrefs.SetString(GameController.UserIdentifier, GameController.UserIdentifier);

        OnDataSaved();
    }

    public static bool LoadData(string userIdentifier)
    {
        string loadedData;
        if (PlayerPrefs.HasKey(_saveName + "_" + userIdentifier))
        {
            loadedData = PlayerPrefs.GetString(_saveName + "_" + userIdentifier);
        }
        else
        {
            OnDataLoaded(false);
            return false;
        }

        LoginType = (LoginType)Enum.Parse(typeof(LoginType), PlayerPrefs.GetString("login_type").Replace("\"", ""));

        JSONNode json = JSON.Parse(loadedData);
        _maxScore = json["MaxScore"].AsInt;
        Debug.LogError(loadedData);
        OnDataLoaded(true);
        return true;
    }

    public static void WriteParseUser(ParseUser user)
    {
        if (FB.IsLoggedIn)
        {
            user["Created"] = true;
            user["MaxScore"] = MaxScore;
            user["Name"] = Name;
            user["Id"] = Id;
        }
        else
        {
            Debug.LogError("FB.IsLoggedIn = " + FB.IsLoggedIn);
        }
    }

    public static bool ReadParseUser(ParseUser user)
    {
        if (user.ContainsKey("Created"))
        {
            if (user.ContainsKey("MaxScore"))
            {
                var maxscore = (long)user["MaxScore"];
                _maxScore = (int)maxscore;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ClearUser()
    {
        Music = true;
        Sound = true;
        Gender = Gender.None;
        Language = Language.English;
        LoginType = LoginType.Guest;
        Score = 0;
        MaxScore = 0;
    }

    public static void CopyData(string userSource, string userTarget)
    {
        LoadData(userSource);
        SaveData(userTarget);
    }

    public static void SetSync(bool flag)
    {
        if (flag)
        {
            PlayerPrefs.SetString("sync", "true");
        }
        else
        {
            PlayerPrefs.SetString("sync", "false");
        }
    }

    public static bool GetSync()
    {
        if (PlayerPrefs.HasKey("sync"))
        {
            if (PlayerPrefs.GetString("sync") == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static void SetMusic(bool flag)
    {
        if (flag)
        {
            PlayerPrefs.SetString("music", "true");
        }
        else
        {
            PlayerPrefs.SetString("music", "false");
        }
    }

    public static bool GetMusic()
    {
        if (PlayerPrefs.HasKey("music"))
        {
            if (PlayerPrefs.GetString("music") == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static void SetSound(bool flag)
    {
        if (flag)
        {
            PlayerPrefs.SetString("sound", "true");
        }
        else
        {
            PlayerPrefs.SetString("sound", "false");
        }
    }

    public static bool GetSound()
    {
        if (PlayerPrefs.HasKey("sound"))
        {
            if (PlayerPrefs.GetString("sound") == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static void SetLoginType(LoginType loginType)
    {
        PlayerPrefs.SetString("login_type", loginType.ToString());
    }

    public static LoginType GetLoginType()
    {
        if (PlayerPrefs.HasKey("login_type"))
        {
            return (LoginType)Enum.Parse(typeof(LoginType), PlayerPrefs.GetString("login_type").Replace("\"", ""));
        }
        else
        {
            return LoginType.Guest;
        }
    }

    public static bool CheckUser()
    {
        return PlayerPrefs.HasKey(GameController.UserIdentifier);
    }

    public static void SaveUser()
    {
        PlayerPrefs.SetString(GameController.UserIdentifier, GameController.UserIdentifier);
    }
}

public enum LoginType
{
    Guest,
    Facebook
}

public enum Gender
{
    None,
    Male,
    Female,
}

public enum Language
{
    English,
    German,
    French,
    Spanish,
    Portugal,
    Italian,
    Russian
}