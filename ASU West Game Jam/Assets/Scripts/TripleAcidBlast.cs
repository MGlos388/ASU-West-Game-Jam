using UnityEngine;

public class TripleAcidBlast : MonoBehaviour
{
    public GameObject acidProjectilePrefab;  // The acid projectile prefab
    public float spreadAngle = 15f;          // Angle between each projectile
    public float shootCooldown = 1.5f;       // Cooldown between shots

    private float lastShotTime;

    void Update()
    {
        // Check if enough time has passed to shoot again
        if (Time.time > lastShotTime + shootCooldown)
        {
            ShootTripleBlast();
            lastShotTime = Time.time;
        }
    }

    void ShootTripleBlast()
    {
        // Fire three projectiles with different angles
        for (int i = -1; i <= 1; i++)
        {
            float angle = i * spreadAngle;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject projectile = Instantiate(acidProjectilePrefab, transform.position, rotation);

            // Optionally, adjust the color to green if you want to ensure it appears as an acid projectile
            projectile.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
