//using System;
//using UnityEngine;
//using System.Collections;
//using ChartboostSDK;

//public class ChartboostController : Singleton<ChartboostController>
//{
//    public event Action<CBLocation, CBImpressionError> DidFailToLoadInterstitial;

//    public event Action<CBLocation> DidDismissInterstitial;

//    public event Action<CBLocation> DidCloseInterstitial;

//    public event Action<CBLocation> DidClickInterstitial;

//    public event Action<CBLocation> DidCacheInterstitial;

//    public event Action<CBLocation> ShouldDisplayInterstitial;

//    public event Action<CBLocation> DidDisplayInterstitial;

//    public override void Start()
//    {
//        base.Start();
//        SetCallbacksChartboost();
//    }

//    public static void ShowAd()
//    {
//        Chartboost.showInterstitial(CBLocation.Default);
//    }

//    private void AddLog(string message)
//    {
//        Debug.Log(message);
//    }

//    #region Chartboost callbacks

//    private void SetCallbacksChartboost()
//    {
//        Chartboost.didFailToLoadInterstitial += OnDidFailToLoadInterstitial;
//        Chartboost.didDismissInterstitial += OnDidDismissInterstitial;
//        Chartboost.didCloseInterstitial += OnDidCloseInterstitial;
//        Chartboost.didClickInterstitial += OnDidClickInterstitial;
//        Chartboost.didCacheInterstitial += OnDidCacheInterstitial;
//        Chartboost.shouldDisplayInterstitial += OnShouldDisplayInterstitial;
//        Chartboost.didDisplayInterstitial += OnDidDisplayInterstitial;
//    }

//    private void OnDidFailToLoadInterstitial(CBLocation location, CBImpressionError error)
//    {
//        AddLog(string.Format("DidFailToLoadInterstitial: {0} at location {1}", error, location));

//        if (DidFailToLoadInterstitial != null)
//        {
//            DidFailToLoadInterstitial(location, error);
//        }
//    }

//    private void OnDidDisplayInterstitial(CBLocation location)
//    {
//        AddLog(string.Format("DidDisplayInterstitial: at location {0}", location));

//        if (DidDisplayInterstitial != null)
//        {
//            DidDisplayInterstitial(location);
//        }
//    }

//    private bool OnShouldDisplayInterstitial(CBLocation location)
//    {
//        AddLog(string.Format("ShouldDisplayInterstitial: at location {0}", location));

//        if (ShouldDisplayInterstitial != null)
//        {
//            ShouldDisplayInterstitial(location);
//        }

//        return true;
//    }

//    private void OnDidCacheInterstitial(CBLocation location)
//    {
//        AddLog(string.Format("DidCacheInterstitial at location {0}", location));

//        if (DidCacheInterstitial != null)
//        {
//            DidCacheInterstitial(location);
//        }
//    }

//    private void OnDidClickInterstitial(CBLocation location)
//    {
//        AddLog(string.Format("DidClickInterstitial: at location {0}", location));

//        if (DidClickInterstitial != null)
//        {
//            DidClickInterstitial(location);
//        }
//    }

//    private void OnDidCloseInterstitial(CBLocation location)
//    {
//        AddLog(string.Format("DidCloseInterstitial: at location {0}", location));

//        if (DidCloseInterstitial != null)
//        {
//            DidCloseInterstitial(location);
//        }
//    }

//    private void OnDidDismissInterstitial(CBLocation location)
//    {
//        AddLog(string.Format("DidDismissInterstitial: at location {0}", location));

//        if (DidDismissInterstitial != null)
//        {
//            DidDismissInterstitial(location);
//        }
//    }

//    #endregion
//}

//public class ShowChartboostAd : AraxisTools.Action
//{
//    private bool _interstitialLoaded;

//    private bool _interstitialLoadingFailed;

//    private bool _interstitialClosed;

//    public override IEnumerable NextFrame()
//    {
//        OnStart();
//#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR 
//        AddHandlers();
//        ChartboostController.ShowAd();

//        while (_interstitialLoaded == false && _interstitialLoadingFailed == false)
//        {
//            yield return null;
//        }

//        if (_interstitialLoaded)
//        {
//            while (_interstitialClosed == false)
//            {
//                yield return null;
//            }
//        }
//        RemoveHandlers();
//#else
//        yield return null;
//#endif
//        OnFinish();
//    }

//    private void AddHandlers()
//    {
//        ChartboostController.Instance.DidFailToLoadInterstitial += InterstitialLoadingFailed;
//        ChartboostController.Instance.DidCacheInterstitial += InterstitialLoaded;
//        ChartboostController.Instance.DidCloseInterstitial += InterstitialCloase;
//    }

//    private void RemoveHandlers()
//    {
//        ChartboostController.Instance.DidFailToLoadInterstitial -= InterstitialLoadingFailed;
//        ChartboostController.Instance.DidCacheInterstitial -= InterstitialLoaded;
//        ChartboostController.Instance.DidCloseInterstitial -= InterstitialCloase;
//    }

//    private void InterstitialLoadingFailed(CBLocation cbLocation, CBImpressionError cbImpressionError)
//    {
//        _interstitialLoadingFailed = true;
//    }

//    private void InterstitialLoaded(CBLocation cbLocation)
//    {
//        _interstitialLoaded = true;
//    }

//    private void InterstitialCloase(CBLocation cbLocation)
//    {
//        _interstitialClosed = true;
//    }
//}