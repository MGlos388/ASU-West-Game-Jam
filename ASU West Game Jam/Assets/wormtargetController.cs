using System.Collections;
using UnityEngine;

public class wormtargetController : MonoBehaviour
{
    [SerializeField] GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Vector3.Distance(transform.position, target.transform.position) < 5) {
            transform.position = Random.insideUnitCircle * 50;
        }


    }

 
}
