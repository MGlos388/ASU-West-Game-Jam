using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PickUpControllerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] UnityEvent pickup;
    [SerializeField] GameObject player;

    private bool collected = false;
    private float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (collected) {
            timer += Time.deltaTime;
            float distance = Vector3.Distance(transform.position, player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position,Time.deltaTime*(distance+2+(timer*3)));
            if (distance < .01f)
            {
                GameObject.Destroy(gameObject);

                pickup.Invoke();
            }

        }

        if (Vector3.Distance(transform.position, player.transform.position) < 1 && !collected) {
            collected = true;
            
        }
    }

}
