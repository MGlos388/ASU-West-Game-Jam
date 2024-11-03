using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject CurrentEventSystem;
    public Slider LoadingBar;
    public TextMeshProUGUI LoadingBar_Percentage;
    public RectTransform SceneTransitionImage;
    public void SetScene(string SceneName)
    {
        StartCoroutine(SetScene_Coro(SceneName));
    }
    IEnumerator SetScene_Coro(string SceneToLoad)
    {
        Time.timeScale = 1;
        CurrentEventSystem.SetActive(false);
        Tween FadeOut = SceneTransitionImage.DOScale(1, 2).SetAutoKill(false);
        yield return FadeOut.WaitForCompletion();
        LoadingBar.gameObject.SetActive(true);
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(SceneToLoad);
        while (!loadingScene.isDone)
        {
            LoadingBar_Percentage.enabled = true;
            float progress = Mathf.Clamp01(loadingScene.progress / 0.9f);
            LoadingBar_Percentage.text = Mathf.RoundToInt(progress * 100) + "%";
            LoadingBar.value = progress;
            yield return null;
        }
    }
}
