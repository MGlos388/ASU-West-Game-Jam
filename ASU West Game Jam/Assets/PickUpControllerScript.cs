using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PickUpControllerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] UnityEvent pickup;
    [SerializeField] GameObject player;

    private bool collected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (collected) {
            GameObject.Destroy(gameObject);

        }

        if (Vector3.Distance(transform.position, player.transform.position) < 1 && !collected) {
            collected = true;
            pickup.Invoke();
            
        }
    }

}
