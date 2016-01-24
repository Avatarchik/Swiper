using System;
using UnityEngine;
using System.Collections;

public class VungleController : Singleton<VungleController>
{
#if UNITY_IOS || UNITY_ANDROID
//    public event Action AdStarted;

//    public event Action<AdFinishedEventArgs> AdFinished;

//    public event Action<bool> AdPlayable;

//    public event Action<string> Log;

//    public override void Start()
//    {
//        base.Start();
//        //Vungle.init("Jumper-Android", "Jumper-iOS");
//        Vungle.init("com.prime31.Vungle", "vungleTest", "vungleTest");
//        Debug.Log("Vungle version = " + Vungle.VersionInfo);
//        SetCallbacks();
//    }

//    void OnApplicationPause(bool pauseStatus)
//    {
//        if (pauseStatus)
//        {
//            Vungle.onPause();
//        }
//        else
//        {
//            Vungle.onResume();
//        }
//    }

//    public void ShowAd()
//    {
//        Vungle.playAd();
//    }

//    private void AddLog(string message)
//    {
//        Debug.Log(message);
//    }

//    #region Vungle callbacks

//    private void SetCallbacks()
//    {
//        Vungle.onAdStartedEvent += OnAdStarted;
//        Vungle.onAdFinishedEvent += OnAdFinished;
//        Vungle.adPlayableEvent += OnAdPlayable;
//        Vungle.onLogEvent += OnLog;
//    }


//    public void OnAdStarted()
//    {
//        AddLog("Ad event is starting!  Pause your game  animation or sound here.");
//        if (AdStarted != null)
//        {
//            AdStarted();
//        }
//    }

//    private void OnAdFinished(AdFinishedEventArgs args)
//    {
//        AddLog("Ad finished - watched time:" + args.TimeWatched + ", total duration:" + args.TotalDuration
//                  + ", was call to action clicked:" + args.WasCallToActionClicked + ", is completed view:"
//                  + args.IsCompletedView);
//        if (AdFinished != null)
//        {
//            AdFinished(args);
//        }
//    }

//    public void OnAdPlayable(bool adPlayable)
//    {
//        AddLog("Ad's playable state has been changed! Now: " + adPlayable);
//        if (AdPlayable != null)
//        {
//            AdPlayable(adPlayable);
//        }
//    }

//    public void OnLog(string log)
//    {
//        AddLog("Log: " + log);
//        if (Log != null)
//        {
//            Log(log);
//        }
//    }

//    #endregion
//}

//public class ShowVungleAd : AraxisTools.Action
//{
//    private bool _adFinished;

//    private int _coins;

//    public override IEnumerable NextFrame()
//    {
//        OnStart();
//#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR

//        if (Vungle.isAdvertAvailable())
//        {
//            AddHandlers();
//            VungleController.Instance.ShowAd();
//            while (_adFinished == false)
//            {
//                yield return null;
//            }
//            RemoveHandlers();

//            PlayerManager.Coins += _coins;
//        }
//#else

//        yield return null;
//#endif
//        OnFinish();
//    }

//    private void AddHandlers()
//    {
//        VungleController.Instance.AdFinished += InterstitialLoadingFailed;
//    }

//    private void RemoveHandlers()
//    {
//        VungleController.Instance.AdFinished -= InterstitialLoadingFailed;
//    }

//    private void InterstitialLoadingFailed(AdFinishedEventArgs adFinishedEventArgs)
//    {
//        if (adFinishedEventArgs.IsCompletedView)
//        {
//            _coins = 10;
//        }
//        else
//        {
//            _coins = 0;
//        }
//        _adFinished = true;
//    }
#endif
}