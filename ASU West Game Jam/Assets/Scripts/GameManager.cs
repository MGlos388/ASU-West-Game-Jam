using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider HealthBar;
    public TextMeshProUGUI HealthBar_Text;
    PlayerControllerScript player;

    public void ExitGame()
    {
        Application.Quit();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
    }
    public void UpdateHealth()
    {
        HealthBar.maxValue = player.maxhealth;
        HealthBar.DOValue(player.health, 0.33f);
        HealthBar_Text.text = player.health.ToString();
    }
}
