using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiantControllerScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject target;
    [SerializeField] GameObject food;
    [SerializeField] float movespeed;
    [SerializeField] float lerpamount;

    List<GameObject> enemies;

    private float foodtimer = 0;

    private float targetAngle = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        foodtimer -= Time.deltaTime;    
        //rb.rotation += (Mathf.PerlinNoise(Time.time,12345))-.5f;

        targetAngle = 180+(180/Mathf.PI)*(Mathf.Atan2(transform.position.y-target.transform.position.y, transform.position.x - target.transform.position.x));


        //rb.rotation = Mathf.LerpAngle(rb.rotation,targetAngle,.01f);
        //float angle = (rb.rotation/ Mathf.Rad2Deg);


        //rb.linearVelocity = new Vector2(Mathf.Cos(angle)*3,Mathf.Sin(angle)*3)*(movespeed+(Mathf.PerlinNoise(Time.time,1235)));

        transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.LerpAngle(transform.rotation.z, targetAngle,lerpamount)));
        transform.Rotate(new Vector3(0,0, (90.0f * (Mathf.PerlinNoise(Time.time/5, 123) - .5f))));
        transform.Translate(Vector3.right*Time.deltaTime*movespeed);


        if (foodtimer < 0) {
            foodtimer = 5;
            GameObject gb = Instantiate(food);
            gb.tag = "Material";
            gb.GetComponent<PickUpControllerScript>().player = GameObject.Find("Player");
            gb.transform.position = transform.position;
        }


        enemies.Clear();
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();
        foreach (GameObject gb in enemies)
        {
            if (Vector3.Distance(gb.transform.position, transform.position) < 10) {
                gb.transform.position += (gb.transform.position - transform.position) / 100;
            }
        }
    }
}
