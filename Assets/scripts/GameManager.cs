using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    // Player position
    Transform player;
    string enemyTag = "Enemy";

    // Camera variable
    Camera cam;
    Plane[] planes;

    void Start() {
        player = PlayerManager.instance.player.transform;

        cam = Camera.main;
        Debug.Log("cam is "+cam);
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
    }

    void Update()
    {
        EnemyController e = GetNearestEnemy();
        if(e != null)
        {
            Debug.Log(e.transform.position);
        }
    }

    // NearestEnemy get nearest enemy to player
    // as a GameObject
    public EnemyController GetNearestEnemy() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if( enemies.Length == 0 ) { return null; }
        EnemyController nearest = enemies[0].GetComponentInChildren<EnemyController>();
        float nearestDistance = 0f;

        foreach(GameObject enemy in enemies)
        {
            EnemyController e = enemy.GetComponentInChildren<EnemyController>();
            Collider c = e.GetComponent<Collider>();
            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if((distance < nearestDistance || nearestDistance == 0f) && GeometryUtility.TestPlanesAABB(planes, c.bounds)) {
                nearest = e;
                nearestDistance = distance;
            }
        }
        return nearest;
    }
}
