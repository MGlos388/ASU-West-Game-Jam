using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Menu_Gameplay : MonoBehaviour
{
    [Header("Incorporate Into Gameplay")]
    public Slider HealthBar;
    Player_Test player;

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
        player = GetComponent<Player_Test>();
        CanOpenAMenu = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateHealth();
        }
    }
    public void UpdateHealth()
    {
        HealthBar.maxValue = player.MaxHealth;
        HealthBar.DOValue(player.Health, 0.33f);
    }
}
