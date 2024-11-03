using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Gameplay : MonoBehaviour
{
    [Header("Incorporate Into Gameplay")]
    public Slider HealthBar;
    public TextMeshProUGUI HealthBar_Text;
    PlayerControllerScript player;

    [Header("Menu Stuff")]
    public bool Paused;
    public bool CanPressPause;
    public bool CanOpenAMenu;
    public Dropdown ResolutionDropdown;
    
    public void CloseMenu(RectTransform MenuToClose)
    {
        MenuToClose.DOScale(0, 0.2f).SetAutoKill(true).SetUpdate(UpdateType.Normal, true);
    }
    public void OpenMenu(RectTransform NewMenuToOpen)
    {
        if (CanOpenAMenu)
        {
            NewMenuToOpen.DOScale(1, 0.2f).SetAutoKill(true).SetUpdate(UpdateType.Normal, true);
        }
    }
    public void SetResolution()
    {
        int width = int.Parse(ResolutionDropdown.options[ResolutionDropdown.value].text.Substring(0, 4));
        int height = int.Parse(ResolutionDropdown.options[ResolutionDropdown.value].text.Substring(5));
        Screen.SetResolution(width, height, true);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
        CanOpenAMenu = true;
    }
    public void UpdateHealth()
    {
        HealthBar.maxValue = player.maxhealth;
        HealthBar.DOValue(player.health, 0.33f);
        HealthBar_Text.text = player.health.ToString();
    }
}
