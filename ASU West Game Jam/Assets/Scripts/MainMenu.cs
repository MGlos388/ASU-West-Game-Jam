using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject CurrentEventSystem;
    public TextMeshProUGUI FrameRateNumber;
    public Slider FrameRateSlider;
    public Toggle FullscreenToggle;
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
    public void Menu_Open(RectTransform menuToOpen)
    {
        menuToOpen.DOScale(1, 0.25f).SetAutoKill(true).SetUpdate(UpdateType.Normal, true);
    }
    public void Menu_Close(RectTransform menuToclose)
    {
        menuToclose.DOScale(0, 0.25f).SetAutoKill(true).SetUpdate(UpdateType.Normal, true);
    }
    public void SetFrameRate(Slider frameRateSlider)
    {
        Application.targetFrameRate = Mathf.RoundToInt(frameRateSlider.value);
        FrameRateNumber.text = frameRateSlider.value.ToString();
    }
    public void SetResolution()
    {
        ToggleFullscreen();
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
        if (!PlayerPrefs.HasKey("BootedBefore"))
        {
            PlayerPrefs.SetInt("Resolution1", 1920);
            PlayerPrefs.SetInt("Resolution2", 1080);
            PlayerPrefs.SetInt("Fulllscreen", 1);
            PlayerPrefs.SetInt("FrameRate", 60);
        }
        VersionText.text = "v" + Application.version;
        if (PlayerPrefs.HasKey("Resolution1"))
        {
            if (PlayerPrefs.GetInt("Fullscreen") == 0)
            {
                Screen.SetResolution(PlayerPrefs.GetInt("Resolution1"), PlayerPrefs.GetInt("Resolution2"), true);
            }
            else
            {
                Screen.SetResolution(PlayerPrefs.GetInt("Resolution1"), PlayerPrefs.GetInt("Resolution2"), false);
            }
        }
        else
        {
            Screen.SetResolution(1920, 1080, true);
        }
        
        FrameRateNumber.text = PlayerPrefs.GetInt("FrameRate").ToString();
        FrameRateSlider.value = PlayerPrefs.GetInt("FrameRate");
    }
    public void ToggleFullscreen()
    {
        int width = int.Parse(ResolutionDropdown.options[ResolutionDropdown.value].text.Substring(0, 4));
        int height = int.Parse(ResolutionDropdown.options[ResolutionDropdown.value].text.Substring(5));
        PlayerPrefs.SetInt("Resolution1", width);
        PlayerPrefs.SetInt("Resolution2", height);

        if (FullscreenToggle.isOn)
        {
            Screen.SetResolution(width, height, true);
        }
        else
        {
            Screen.SetResolution(width, height, false);
        }
    }
}
