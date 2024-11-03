using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject CurrentEventSystem;
    public TextMeshProUGUI LoadingBar_Percentage;
    IEnumerator SetScene_Coro(string sceneName)
    {
        Time.timeScale = 1;
        CurrentEventSystem.SetActive(false);
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneName);
        while (!loadingScene.isDone)
        {
            float progress = Mathf.Clamp01(loadingScene.progress / 0.9f);
            LoadingBar_Percentage.text = Mathf.RoundToInt(progress * 100) + "%";
            yield return null;
        }
    }
    public void SetScene(string sceneName)
    {
        StartCoroutine(SetScene_Coro(sceneName));
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
