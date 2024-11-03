using System.Collections;
using UnityEngine;

public class PickUpControllerScript : MonoBehaviour
{
    [SerializeField] public GameObject player;

    private bool collected = false;
    public float CollectionRange;
    private float timer = 0;
    void Start()
    {
        //Fade In
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < CollectionRange && !collected) {
            collected = true;
        }
        if (collected) {
            timer += Time.deltaTime;
            float distance = Vector3.Distance(transform.position, player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime*(distance+2+(timer*3)));
            if (distance <= .01f)
            {
                
                GameObject.Destroy(gameObject, 0.25f);
            }
        }
    }
}
