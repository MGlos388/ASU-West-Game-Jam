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
    public TMP_Dropdown ResolutionDropdown;
    public RectTransform SceneTransitionImage;
    public TextMeshProUGUI VersionText;

    public void Credits_Link(string siteToOpen)
    {
        Application.OpenURL(siteToOpen);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetResolution()
    {
        int width = int.Parse(ResolutionDropdown.options[ResolutionDropdown.value].text.Substring(0, 4));
        int height = int.Parse(ResolutionDropdown.options[ResolutionDropdown.value].text.Substring(5));
        PlayerPrefs.SetInt("Resolution1", width);
        PlayerPrefs.SetInt("Resolution2", height);

        if (PlayerPrefs.GetInt("Fullscreen") == 0)
        {
            Screen.SetResolution(width, height, false);
        }
        else
        {
            Screen.SetResolution(width, height, true);
        }
    }
    public void SetScene(string SceneName)
    {
        StartCoroutine(SetScene_Coro(SceneName));
    }
    IEnumerator SetScene_Coro(string SceneToLoad)
    {
        Time.timeScale = 1;
        CurrentEventSystem.SetActive(false);
        Tween FadeOut = SceneTransitionImage.DOScale(1, 1.25f).SetAutoKill(false);
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
    public void Start()
    {
        VersionText.text = "v" + Application.version;
    }
}
