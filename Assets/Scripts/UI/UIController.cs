using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private InterstitialAds iAds;
    private bool iAdsCompleted = false;

    public InterstitialAds IAds { set => iAds = value; }

    public void LoadScene(string loadSceneName)
    {
        SceneManager.LoadScene(loadSceneName, LoadSceneMode.Single);
    }

    public void LoadSceneWithAds(string loadSceneName)
    {
        iAds.LoadAd();
        iAdsCompleted = false;
        iAds.onShowCompleted.AddListener(SetIAdsCompleted);
        iAds.ShowAd();

        StartCoroutine(AsyncLoadSceneWithAds(loadSceneName));
    }

    public void SetIAdsCompleted() => iAdsCompleted = true;

    public void CallApplicationQuit()
    {
        Application.Quit();
    }

    //----------------------------------------------------------------------------------
    IEnumerator AsyncLoadSceneWithAds(string loadSceneName)
    {
        //��������� ������������ �������� �����
        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Single);
        
        //���� ���� ����� �� ����������� � ������� �� ����������
        while (!async.isDone && !iAdsCompleted)
        {
            yield return null;
        }
    }
}
