using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemyControllerScript : MonoBehaviour
{

    [SerializeField] List<GameObject> enemypool;
    List<GameObject> destructionlist;

    [SerializeField] int spawncap;
    [SerializeField] int spawndistance;
    [SerializeField] int despawndistance;

    List<GameObject> enemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (enemies.Count<spawncap) {
            GameObject newenemy = Instantiate(enemypool[Random.Range(0,enemypool.Count)]);
            newenemy.transform.position = transform.position;
            Vector2 offset = ((Random.insideUnitCircle).normalized)*spawndistance;

            newenemy.transform.position += new Vector3(offset.x,offset.y,0);

            enemies.Add(newenemy);
        
        }


        destructionlist = new List<GameObject>();
        foreach (GameObject enemy in enemies) {
            if (Vector3.Distance(transform.position, enemy.transform.position)>despawndistance) {
                destructionlist.Add(enemy); 
            
            }
        }
        while (destructionlist.Count > 0) {
            GameObject destroy = destructionlist[0];
            
            destructionlist.Remove(destroy);
            enemies.Remove(destroy);
            GameObject.Destroy(destroy);
        }


    }
}
