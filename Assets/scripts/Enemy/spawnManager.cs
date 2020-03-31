using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.Common;
using GoogleARCore;

public class spawnManager : MonoBehaviour
{
    //State of SpawnManager
    public enum SpawnState {SPAWNING, WAITING, COUNTING, HALTING };

    [SerializeField] Transform cameraTransform;

    [System.Serializable]
    public class Wave
    {
        [Header("Types of enemies")]
        // public GameObject enemy;
        public int count;
        public float rate;
        public GameObject enemyPrefab;

        public void setCount(int newCount)
        {
            this.count = newCount;
        }

    }

    //Wave properties
    [SerializeField] public Wave[] waves;
    private int nextWave;
    [SerializeField] private float timeBetweenWaves = 5f;
    public float waveCountdown;

    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    public SpawnState state = SpawnState.HALTING;


    [Header("Range of spawning position")]
    [SerializeField]
    public float xPosition;
    public float yPosition;
    public float zPosition;

    // [Header("Radius away from the player")]
    // [SerializeField]
    // public float xRadius;
    // public float yRadius;
    // public float zRadius;

    // [Header("Previously randomized Position")]
    // [SerializeField]
    // public float xSpawn;
    // public float ySpawn;
    // public float zSpawn;

    [Header("Distance Radius")]
    [SerializeField]
    public float playerRadius;
    public float spawnRadius;
    public Vector3 preSpawn = Vector3.zero;

    [Header("Range of spawning time")]
    [SerializeField]
    public float minTime;
    public float maxTime;

    [Header("Live display of enemies in the scene")]
    public List<GameObject> enemies;

    //countdown of search time.
    private float searchCountdown = 1f;
    
    // Plane Generator, Parent Object of all detected plane.
    [SerializeField] DetectedPlaneGenerator planeGenerator;
    // Visualizer, Hold DetectedPlane inside, can be found as child of DetectedPlaneGenerater Object.
    DetectedPlaneVisualizer[] visualizer;

    //Randomly spawn enemies.
    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        // Unhalt();
    }

    //Remove an enemy from the list if one dies.
    private void Update()
    {
        // Check if manager is halting (waiting for arcore to detect plane)
        if ( state == SpawnState.HALTING )
        {
            return;
        }

        if (state == SpawnState.WAITING)
        {
            RemoveDestroyedEnemy();
            if (!isAlive())
            {
                //Begin a new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        //Check if all waves are done.
        if (waveCountdown <= 0)
        {
            if (state == SpawnState.COUNTING)
            {
                // get detected plane
                GetVisualizer();
                if(visualizer.Length > 0)
                {
                    Debug.Log("Plane Detected!!!");
                    //Start spawning wave
                    StartCoroutine(spawnWave(waves[nextWave]));
                }
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    public void Unhalt()
    {
        if ( state == SpawnState.HALTING )
        {
            state = SpawnState.COUNTING;
            waveCountdown = 0;
        }
    }

    private void GetVisualizer()
    {
        visualizer = planeGenerator.GetComponentsInChildren<DetectedPlaneVisualizer>();
        visualizer = FilterPlane(visualizer);
    }

    //Destroy enemies will be removed from the list.
    public void RemoveDestroyedEnemy()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                }
            }
        }
    }

    //Spawning next or reset the wave.
    public void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! start looping...");
        }
        else
        {
            nextWave++;
        }
    }

    //Spawn enemies intervally.
    IEnumerator spawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        //spawning
        for (int i = 0; i < _wave.count; i++)
        {
            spawnEnemy(_wave.enemyPrefab.GetComponent<FullGhostPrefab>());
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    //Spawn enemies.
    void spawnEnemy(FullGhostPrefab _enemyPrefab)
    {
        int planeIndex;
        Vector3 spawnPoint;
        Quaternion spawnRotation = Quaternion.identity;

        // random Plane from Detected Plane list
        planeIndex = UnityEngine.Random.Range(0, visualizer.Length);
        // get center position from selected plane
        spawnPoint = visualizer[planeIndex].m_DetectedPlane.CenterPose.position;
        // spawnPoint = checkSpawnPoint(spawnPoint);
        spawnRotation = visualizer[planeIndex].m_DetectedPlane.CenterPose.rotation;
        
        GameObject enemyPrefeb;
        switch(visualizer[planeIndex].m_DetectedPlane.PlaneType)
        {
            case DetectedPlaneType.Vertical:
                enemyPrefeb = _enemyPrefab.ghostPortalVertical;
                spawnRotation.y += 180;
                break;
            default:
                enemyPrefeb = _enemyPrefab.ghostPortalHorizontal;
                break;
        }

        //Spawn ghost into the map and add into live display.
        GameObject ghost = Instantiate(enemyPrefeb, spawnPoint, spawnRotation);
        preSpawn = spawnPoint;
        enemies.Add(ghost);
    }

    // filter plane if it is far enought
    private DetectedPlaneVisualizer[] FilterPlane(DetectedPlaneVisualizer[] visualizer)
    {
        List<DetectedPlaneVisualizer> v = new List<DetectedPlaneVisualizer>();
        foreach(DetectedPlaneVisualizer p in visualizer)
        {
            if( IsFarEnought(p.m_DetectedPlane.CenterPose.position) )
            // if( p.m_DetectedPlane.PlaneType == DetectedPlaneType.Vertical )
            {
                v.Add(p);
            }
        }
        return v.ToArray();
    }

    // check if spawn point is far enought from player(camera)
    private bool IsFarEnought(Vector3 spawnpoint)
    {
        if( Vector3.Distance(spawnpoint, cameraTransform.position) >= playerRadius )
        {
            return true;
        }
        return false;
    }

    //Check whether if enemies are alive.
    bool isAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if(enemies.Count == 0)
            {
                return false;
            }
        }
        return true;
    }

    //Check range of the spawn 
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position - new Vector3(xPosition, 0, 0), transform.position + new Vector3(xPosition, 0, 0));
        Gizmos.DrawLine(transform.position - new Vector3(0, 0, zPosition), transform.position + new Vector3(0, 0, zPosition));
    }
}
