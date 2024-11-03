using UnityEngine;

public class raftspawner : MonoBehaviour
{
    [SerializeField] GameObject raft;
    [SerializeField] int raftcount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < raftcount; i++) {
            GameObject newraft = Instantiate(raft);
            newraft.transform.position = Vector3.zero;
            float angle = (Mathf.PI*2)/raftcount;
            angle += Random.Range(0,angle/5);

            newraft.transform.position += new Vector3(Mathf.Sin(angle*i)*60, Mathf.Cos(angle * i)*60, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
