using UnityEngine;
using System.Collections; // Required for IEnumerator

public class ZigZagToPlayer : MonoBehaviour
{
    private Transform player;                  // Reference to the player's Transform
    public GameObject arrowPrefab;            // Arrow prefab to show direction

    public float minSpeed = 2f;               // Minimum movement speed
    public float maxSpeed = 5f;               // Maximum movement speed
    public float minDelayTime = 1f;           // Minimum delay time between zigzag moves
    public float maxDelayTime = 3f;           // Maximum delay time between zigzag moves
    public float zigzagAngle = 30f;           // Max angle deviation for zigzag

    private GameObject currentArrow;          // Instance of the arrow
    private float speed;                      // Current random speed
    private float delayTime;                  // Current random delay time    
    private Animator animator;
    private void Start()
    {     
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Transform childTransform = transform.Find("SBMSEnemy1_ZigZagVariation");
        if (childTransform != null)
        {
            animator = childTransform.GetComponent<Animator>();           
        }

        StartCoroutine(ZigZagMovement());
    }

    private IEnumerator ZigZagMovement()
    {
        while (true)
        {
            if (player != null)
            {

                // Get the direction to the player
                Vector2 directionToPlayer = (player.position - transform.position).normalized;

                /////////////////////// added for med var enemy, to rotate sprite towards player
                var startingAngleOffset =  90;
                // Calculate the angle to rotate towards the player
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                // Rotate the enemy sprite to face the player
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + startingAngleOffset));
                ///////////////////////

                // Generate a random angle within the zigzag range
                float randomAngle = Random.Range(-zigzagAngle / 2, zigzagAngle / 2);
                
                // Rotate the direction by the random angle
                Vector2 zigzagDirection = Quaternion.Euler(0, 0, randomAngle) * directionToPlayer;

                // Randomize speed and delay time for this zigzag
                speed = Random.Range(minSpeed, maxSpeed);
                delayTime = Random.Range(minDelayTime, maxDelayTime);

                // Draw or update the arrow in the zigzag direction
                //not working atm //DrawArrow(zigzagDirection);

                // Move in the zigzag direction for a short duration
                float moveDuration = delayTime;
                float elapsedTime = 0f;
 
                if (animator != null)
                    animator.enabled = true; // disable animation when moving
                while (elapsedTime < moveDuration)
                {
                    transform.position += (Vector3)(zigzagDirection * speed * Time.deltaTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }

            if (animator != null)
                animator.enabled = false; // Enable animation when moving
            // Wait for the random delay time before the next zigzag movement
            yield return new WaitForSeconds(delayTime);
        }
    }
  
    // not working atm
    private void DrawArrow(Vector2 direction)
    {
        // if unassigned, don't draw
        if (arrowPrefab == null)
            return;

        // If an arrow already exists, destroy it to create a new one
        if (currentArrow != null)
        {
            Destroy(currentArrow);
        }

        // Create a new arrow instance
        currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

        // Set the arrow's rotation to match the zigzag direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        
        // Set the arrow position just in front of the enemy
        currentArrow.transform.position = transform.position + (Vector3)direction * 0.5f;
    }
}