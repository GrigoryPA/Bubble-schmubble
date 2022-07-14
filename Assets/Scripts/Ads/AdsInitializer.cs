using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameID = "4840328";
    [SerializeField] private string iOSGameID = "4840329";
    [SerializeField] private bool testMod = true;
    private string gameID;

    private void Awake()
    {
        InitializeAds();
    }

    private void InitializeAds()
    {
        gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSGameID : androidGameID;
        Advertisement.Initialize(gameID, testMod, true, this);
    }

    public void OnInitializationComplete()
    {
        //throw new System.NotImplementedException();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //throw new System.NotImplementedException();
    }
}
