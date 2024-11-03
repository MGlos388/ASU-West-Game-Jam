using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    private Transform player;      // Reference to the player's position
    public float speed = 2f;      // Speed at which the enemy follows
    public float minimumFollowDistance = 1;
    public float maxmumFollowDistance = 8;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (player != null)
        {
            // Move towards the player
            if (Vector2.Distance(transform.position, player.position) <= maxmumFollowDistance && Vector2.Distance(transform.position, player.position) >= minimumFollowDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }
}
