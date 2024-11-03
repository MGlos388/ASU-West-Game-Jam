using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyPieceController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject target;
    Rigidbody2D rb;
    List<GameObject> enemies;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        /**
        if (Vector3.Distance(transform.position, target.transform.position) > 2)
        {
            rb.linearVelocity = (target.transform.position - transform.position).normalized*(Vector3.Distance(transform.position,target.transform.position)*2);
            transform.LookAt(target.transform.position);
        }
        else {
            rb.linearVelocity = Vector3.zero;
        }
        **/

        if (Vector3.Distance(transform.position, target.transform.position) > 2)
        {
            
            transform.LookAt(target.transform.position);

            transform.Translate(Vector3.forward* Vector3.Distance(transform.position, target.transform.position));
            transform.Translate(Vector3.forward* -2);

        }
        else
        {
            
        }

        enemies.Clear();
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();
        foreach (GameObject gb in enemies)
        {
            if (Vector3.Distance(gb.transform.position, transform.position) < 10)
            {
                gb.transform.position += (gb.transform.position - transform.position) / 100;
            }
        }
    }
}
