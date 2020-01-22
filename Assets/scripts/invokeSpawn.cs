using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invokeSpawn : MonoBehaviour
{
     public GameObject enemy;
     public int xPos;
     public int zPos;
     public int xSpawn;
     public int zSpawn;
     public float minT, maxT;
     public List<GameObject> enemies;

    private float searchCountdown = 1f;

    private void Start()
    {
        Invoke("spawnEnemy", 0.5f);
    }

    private void Update()
    {   
        if(isAlive())
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i] == null)
                {
                    enemies.Remove(enemies[i]);
                }
            }
       
        }
    }

    bool isAlive()
    {
       
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
                if (enemies.Count == 0)
                {
                    return false;
                }
            }
            return true;
     
    }


    void spawnEnemy()
    {
        if (isAlive())
        {
            float spawnTime = Random.Range(minT, maxT);
            Invoke("spawnEnemy", spawnTime);
            xSpawn = Random.Range(0, xPos-2);
            zSpawn = Random.Range(0, zPos-2);
            GameObject ghost = Instantiate(enemy, new Vector3(xSpawn, 0.26f, zSpawn), Quaternion.identity);
            Debug.Log("Spawned");
            enemies.Add(ghost.gameObject);
        }
        else
        {   
            Debug.Log("No enemies spawn.");
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position - new Vector3(xPos, 0, 0), transform.position + new Vector3(xPos, 0, 0));
        Gizmos.DrawLine(transform.position - new Vector3(0, 0, zPos), transform.position + new Vector3(0, 0, zPos));

    }
}
