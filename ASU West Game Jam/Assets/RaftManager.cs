using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaftManager : MonoBehaviour
{
    [SerializeField] GameObject displaypanel;
    [SerializeField] GameObject woodcountdisplay;

    GameObject player;

    Vector3 targetScale;

    private int woodreq;

    private bool raftbuilt = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                    Debug.Log("you win");
                    SceneManager.LoadScene("EndScene");

                }
                else {
                    Debug.Log("not enough wood");
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
}
