using UnityEngine;

public class arrowController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject worm;
    [SerializeField] GameObject arrow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, worm.transform.position) > 15)
        {
            arrow.SetActive(true);
            arrow.transform.LookAt(worm.transform.position);
        }
        else {
            arrow.SetActive(false);
        }
    }
}
