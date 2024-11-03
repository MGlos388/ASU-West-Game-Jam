using UnityEngine;

public class BodyPieceController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject target;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 2)
        {
            rb.linearVelocity = (target.transform.position - transform.position).normalized*(Vector3.Distance(transform.position,target.transform.position)*2);
            transform.LookAt(target.transform.position);
        }
        else {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
