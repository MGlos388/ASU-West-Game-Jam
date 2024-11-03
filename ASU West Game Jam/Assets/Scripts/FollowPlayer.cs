using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public Transform player;      // Reference to the player's position
    public float speed = 2f;      // Speed at which the enemy follows

    private void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with Player!");
            // Here you can add more behavior, such as dealing damage, etc.
        }
    }
}
