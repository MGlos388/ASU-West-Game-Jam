using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class RaftManager : MonoBehaviour
{
    [SerializeField] GameObject displaypanel;
    [SerializeField] GameObject woodcountdisplay;
    public GameManager gameManager;

    GameObject player;

    Vector3 targetScale;

    private int woodreq;

    private bool raftbuilt = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        targetScale = displaypanel.transform.localScale;
        player = GameObject.Find("Player");
        woodreq = Random.Range(10,20);
        woodreq *= 10;

        woodcountdisplay.GetComponent<TextMeshPro>().text = "X"+woodreq.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        woodcountdisplay.GetComponent<TextMeshPro>().color = new Color(Mathf.Lerp(
            woodcountdisplay.GetComponent<TextMeshPro>().color.r,1,.01f), 
            Mathf.Lerp(woodcountdisplay.GetComponent<TextMeshPro>().color.g, 1, .01f),
            Mathf.Lerp(woodcountdisplay.GetComponent<TextMeshPro>().color.b, 1, .01f));


        if (raftbuilt) {
            if (Vector3.Distance(transform.position, player.transform.position) < 2) {
                if (GameObject.Find("outeredge")) {
                    StartCoroutine(endfunction());
                    GameObject.Find("outeredge").SetActive(false);
                }
                player.GetComponent<EnemyControllerScript>().stop();
                transform.position *= 1.0001f;
                player.transform.Translate((transform.position-player.transform.position)/40);
                player.GetComponentInChildren<Light2D>().intensity -= Time.deltaTime/10;
            }
        
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 4 && !raftbuilt)
        {
            displaypanel.transform.localScale = new Vector3(Mathf.Lerp(displaypanel.transform.localScale.x,targetScale.x,.1f),
                Mathf.Lerp(displaypanel.transform.localScale.y, targetScale.y, .1f),
                Mathf.Lerp(displaypanel.transform.localScale.z, targetScale.z, .1f));

            if (Input.GetKeyDown(KeyCode.E)) {
                if (player.GetComponent<PlayerControllerScript>().woodcount >= woodreq)
                {
                    player.GetComponent<PlayerControllerScript>().woodcount-=woodreq;
                    raftbuilt = true;
                    GetComponentInChildren<BoxCollider2D>().enabled = false;
                    Debug.Log("you win");
                    //SceneManager.LoadScene("EndScene");

                }
                else {
                    Debug.Log("not enough wood");
                    gameManager.CantPickUpWood_SoundFX.Play();
                    woodcountdisplay.GetComponent<TextMeshPro>().color = Color.red;
                    displaypanel.transform.localScale += new Vector3(-.1f,.1f,0);


                }
            
            }

        }
        else {
            displaypanel.transform.localScale = new Vector3(Mathf.Lerp(displaypanel.transform.localScale.x, 0, .1f),
                    Mathf.Lerp(displaypanel.transform.localScale.y, targetScale.y, .1f),
                    Mathf.Lerp(displaypanel.transform.localScale.z, targetScale.z, .1f));

        }
    }

    IEnumerator endfunction() {
        yield return new WaitForSeconds(12);
        SceneManager.LoadScene("EndScene");
    }
}
