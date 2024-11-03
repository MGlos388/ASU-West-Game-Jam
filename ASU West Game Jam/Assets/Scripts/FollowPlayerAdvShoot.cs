//using System.Numerics;
using UnityEngine;

public class FollowPlayerAdvShoot : MonoBehaviour
{
    private Transform player;      // Reference to the player's position
    public float speed = 2f;      // Speed at which the enemy follows
    public float minimumFollowDistance = 1;
    public float maxmumFollowDistance = 8; 
    public float startingAngleOffset = 90f; // The angle offset to apply to the starting rotation


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

            // Move the enemy towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Calculate the angle to rotate towards the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the enemy sprite to face the player
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));            // Rotate the enemy sprite to face the player, adding the offset
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + startingAngleOffset));
        }
    } 
    // private void Update()
    // { 
    //     //ar rb 
    //     UnityEngine.Vector2 direction = (UnityEngine.Vector2) player.position - (UnityEngine.Vector2)transform.position;
    //     direction.Normalize();
    //     float rotateAmount = UnityEngine.Vector3.Cross(direction, transform.up).z;
    //     //rb.angularVelocity = -rotateAmount * RotateSpeed;

    //     if (player != null)
    //     {
    //         // Move towards the player
    //         if (UnityEngine.Vector2.Distance(transform.position, player.position) <= maxmumFollowDistance && UnityEngine.Vector2.Distance(transform.position, player.position) >= minimumFollowDistance)
    //         {
    //             UnityEngine.Vector2 direction = (player.position - transform.position).normalized;
    //             transform.position = UnityEngine.Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    //         }
    //     }
    // }

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
