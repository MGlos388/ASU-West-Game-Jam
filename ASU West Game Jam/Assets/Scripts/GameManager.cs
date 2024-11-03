using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider HealthBar;
    public TextMeshProUGUI HealthBar_Text;
    public TextMeshProUGUI WoodValue_Text;
    PlayerControllerScript player;
    public AudioSource CantPickUpWood_SoundFX;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
        UpdateWood();
    }
    public void UpdateHealth()
    {
        HealthBar.maxValue = player.maxhealth;
        HealthBar.DOValue((int)player.health, 0.33f);
        HealthBar_Text.text = ((int)player.health).ToString() + "/" + ((int)player.maxhealth).ToString();
    }

    public void UpdateWood() {
        WoodValue_Text.text = player.woodcount.ToString();
    }
}
