using UnityEngine;

public class GiantControllerScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject target;
    [SerializeField] float movespeed;

    private float targetAngle = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //rb.rotation += (Mathf.PerlinNoise(Time.time,12345))-.5f;

        targetAngle = 180+(180/Mathf.PI)*(Mathf.Atan2(transform.position.y-target.transform.position.y, transform.position.x - target.transform.position.x));


        rb.rotation = Mathf.LerpAngle(rb.rotation,targetAngle,.01f);
        float angle = (rb.rotation/ Mathf.Rad2Deg);


        rb.linearVelocity = new Vector2(Mathf.Cos(angle)*3,Mathf.Sin(angle)*3)*(movespeed+(Mathf.PerlinNoise(Time.time,1235)));

    }
}
