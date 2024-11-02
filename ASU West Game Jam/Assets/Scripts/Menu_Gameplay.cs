using UnityEngine;
using UnityEngine.UI;

public class Menu_Gameplay : MonoBehaviour
{
    public Slider HealthBar;
    Player_Test player;
    
    void Start()
    {
        player = GetComponent<Player_Test>();
    }
    public void UpdateHealth()
    {
        HealthBar.maxValue = player.MaxHealth;
        HealthBar.value = player.Health;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateHealth();
        }
    }
}
