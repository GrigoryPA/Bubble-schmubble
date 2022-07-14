using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button button;
    [SerializeField] private string androidAdID = "Rewarded_Android";
    [SerializeField] private string iOSAdID = "Rewarded_iOS";

    [Space]
    [Space]
    public int countShows = 0;
    public UnityEvent OnShowCompleted;

    private string adID;

    private void Awake()
    {
        adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSAdID : androidAdID;
        button.interactable = false;
        button.onClick.AddListener(ShowAd);
    }

    private void Start()
    {
        LoadAd();
    }

    private void LoadAd()
    {
        //если кол-во просмотров не превышено
        if (countShows >= 0)
        {
            Advertisement.Load(adID, this);
        }
    }

    private void ShowAd()
    {
        button.interactable = false;
        Advertisement.Show(adID, this);
    }

    //-----------------------------------------------------------------------------------------------
    //IUnityAdsLoadListener

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(adID))
        {
            button.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        //print("OnUnityAdsFailedToLoad");
        //throw new System.NotImplementedException();
    }


    //-----------------------------------------------------------------------------------------------
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
        print("reward show completed");
        if (placementId.Equals(adID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            countShows--; //уменьшаем кол-во просмотров
            //Здесь даем награду за рекламу

            //Запускаем событие окончания просмотра рекламы
            OnShowCompleted.Invoke();

            //сразу же пытаемся подгрузить на всякий случай следующую рекламу
            LoadAd();
        }
    }
}
