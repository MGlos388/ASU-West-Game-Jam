using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject wood;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Worm") {



            GameObject gb = Instantiate(wood,transform.position,Quaternion.identity);
            gb.transform.rotation = Quaternion.Euler(new Vector3(0,0,Random.Range(0,360)));
            gb.tag = "Wood";
            gb.GetComponent<PickUpControllerScript>().player = GameObject.Find("Player");

            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            StartCoroutine(regrow());
        
        }
    }


    IEnumerator regrow() {


        yield return new WaitForSeconds(120);

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

    }
}
