using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    // Player position
    Transform player;
    string enemyTag = "Enemy";

    void Start() {
        player = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        
    }

    // NearestEnemy get nearest enemy to player
    // as a GameObject
    public GameObject GetNearestEnemy() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if( enemies.Length == 0 ) { return null; }
        GameObject nearest = enemies[0];
        float nearestDistance = Vector3.Distance(player.position, nearest.transform.position);

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if(distance < nearestDistance) {
                nearest = enemy;
            }
        }
        return nearest;
    }
}
