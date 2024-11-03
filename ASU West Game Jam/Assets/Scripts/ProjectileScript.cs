using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float damage = 2f;           // Speed of the projectile
    public float speed = 5f;           // Speed of the projectile
    public float lifetime = 3.5f;        // How long the projectile lasts before disappearing

    private void Start()
    {
        // Destroy the projectile after a set time
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the projectile forward
        //was .right, but with the 90degree offset, use instead vector2.up //transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collisions with enemies or obstacles here
        // Destroy projectile on collision
        Destroy(gameObject);
    }
}
