using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider HealthBar;
    public TextMeshProUGUI HealthBar_Text;
    public TextMeshProUGUI WoodValue_Text;
    PlayerControllerScript player;
    public RectTransform MainMenu;

    public void SetupGame()
    {
        StartCoroutine(SetupGame_Coro());
    }
    IEnumerator SetupGame_Coro()
    {
        MainMenu.DOScale(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        MainMenu.gameObject.SetActive(false);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
        UpdateWood();
    }
    public void UpdateHealth()
    {
        HealthBar.maxValue = player.maxhealth;
        HealthBar.DOValue(player.health, 0.33f);
        HealthBar_Text.text = player.health.ToString() + "/" + player.maxhealth.ToString();
    }

    public void UpdateWood() {
        WoodValue_Text.text = player.woodcount.ToString();
    
    }
}
