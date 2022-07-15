using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdID = "Interstitial_Android";
    [SerializeField] private string iOSAdID = "Interstitial_iOS";
    private string adID;

    [Space]
    [Space]
    public bool isShowingOnStart = false;
    public UnityEvent onShowCompleted;

    private void Awake()
    {
        adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSAdID : androidAdID;
    }

    private void OnEnable()
    {
        LoadAd();
        if (isShowingOnStart)
        {
            ShowAd();
        }
    }

    public void LoadAd()
    {
        Advertisement.Load(adID, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(adID, this);
    }




    //IUnityAdsLoadListener

    public void OnUnityAdsAdLoaded(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        //throw new System.NotImplementedException();
    }



    //IUnityAdsShowListener

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        onShowCompleted.Invoke();
        LoadAd();
    }
}
