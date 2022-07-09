using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void LoadScene(string loadSceneName)
    {
        SceneManager.LoadScene(loadSceneName, LoadSceneMode.Single);
    }

    public void CallApplicationQuit()
    {
        Application.Quit();
    }
}
