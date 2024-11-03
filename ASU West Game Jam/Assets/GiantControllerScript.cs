using UnityEngine;

public class GiantControllerScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rb.rotation += (Mathf.PerlinNoise(Time.time,124987)*2)-1f;


        float angle = (rb.rotation/ Mathf.Rad2Deg);


        rb.linearVelocity = new Vector2(Mathf.Cos(angle)*3,Mathf.Sin(angle)*3);

    }
}
