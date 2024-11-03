//using System.Numerics;
using UnityEngine;

public class FollowPlayerAdvShoot : MonoBehaviour
{ 
    private Transform player;      // Reference to the player's position
    public float speed = 2f;      // Speed at which the enemy follows
    public float minimumFollowDistance = 0.6f;
    public float maxmumFollowDistance = 12; 
    public float startingAngleOffset = 90f; // The angle offset to apply to the starting rotation

    // Acid Blast Variables
    public GameObject acidProjectilePrefab;  // Acid projectile prefab
    public float spreadAngle = 15f;          // Angle between each projectile in the triple shot
    public float shootCooldown = 5.5f;         // Cooldown time between each shot

    private float lastShotTime = 0f;         // Track the time since the last shot

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    } 
    private void Update()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector2 direction = (player.position - transform.position).normalized;
 
            // Move towards the player
            if (Vector2.Distance(transform.position, player.position) <= maxmumFollowDistance && Vector2.Distance(transform.position, player.position) >= minimumFollowDistance)
            { 
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            } 

            // Calculate the angle to rotate towards the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the enemy sprite to face the player
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + startingAngleOffset));

			// Shooting logic
			if (Time.time > lastShotTime + shootCooldown)
			{
                Debug.Log("triple blast from adv enemy move");
				ShootTripleBlast();
				lastShotTime = Time.time;
			}
		}
    }

	void ShootTripleBlast()
    {
        // Fire three projectiles in a spread pattern
        for (int i = -1; i <= 1; i++)
        {
            float angle = i * spreadAngle;
            Quaternion rotation = Quaternion.Euler(0, 0, angle) * Quaternion.LookRotation(Vector3.forward, player.position - transform.position);
            GameObject projectile = Instantiate(acidProjectilePrefab, transform.position, rotation);

            // Set the projectile color to green if desired
            projectile.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Adv Enemy collided with Player!");
            // Here you can add more behavior, such as dealing damage, etc.
        }
    }
}
