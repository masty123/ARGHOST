using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.Common;

public class spawnManager : MonoBehaviour
{
    //State of SpawnManager
    public enum SpawnState {SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        [Header("Types of enemies")]
        public GameObject enemy;
        public int count;
        public float rate;

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

    public SpawnState state = SpawnState.COUNTING;


    [Header("Range of spawning position")]
    [SerializeField]
    public float xPosition;
    public float yPosition;
    public float zPosition;

    [Header("Radius away from the player")]
    [SerializeField]
    public float xRadius;
    public float yRadius;
    public float zRadius;

    [Header("Previously randomized Position")]
    [SerializeField]
    public float xSpawn;
    public float ySpawn;
    public float zSpawn;


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
    }

    //Remove an enemy from the list if one dies.
    private void Update()
    {
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
                visualizer = planeGenerator.GetComponentsInChildren<DetectedPlaneVisualizer>(); 
                if(visualizer.Length > 0)
                {
                    Debug.Log("Plane Detected!!!");
                    //Start spawning wave
                    StartCoroutine(spawnWave(waves[nextWave]));
                }
                Debug.Log("Detected plane = " + visualizer.Length);
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
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
            spawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    //Spawn enemies.
    void spawnEnemy(GameObject _enemy)
    {
        // if (isAlive())
        {
            //Store 2 number of each axis.
            List<float> spawnAreaX = new List<float>();
            List<float> spawnAreaY = new List<float>();
            List<float> spawnAreaZ = new List<float>();

            //Random time to start spawning.
            // float spawnTime = Random.Range(minTime, maxTime);
            // timeBetweenWaves = spawnTime;
            // int randomRange = Random.Range(0, _enemy.Length);

            //Random X position and at it into a list
            float positiveX = Random.Range(xRadius, xPosition);
            spawnAreaX.Add(positiveX);

            //Random -X position and at it into a list
            float negativeX = Random.Range(-xPosition, -xRadius);
            spawnAreaX.Add(negativeX);

            //No underground spawning
            //Random Y position and at it into a list
            float positiveY = Random.Range(yRadius, yPosition);
            spawnAreaY.Add(positiveY);

            //Random Z position and at it into a list
            float positiveZ = Random.Range(zRadius, zPosition );
            spawnAreaZ.Add(positiveZ);

            //Random -Z position and at it into a list
            float negativeZ = Random.Range(-zPosition, -zRadius);
            spawnAreaZ.Add(negativeZ);

            //Get random X-axis number.
            int Xindex = Random.Range(0, spawnAreaX.Count);
            xSpawn = spawnAreaX[Xindex];

            //Get random Y-axis number.
            int Yindex = Random.Range(0, spawnAreaY.Count);
            ySpawn = spawnAreaY[Yindex];

            //Get random Z-axis number.
            int Zindex = Random.Range(0, spawnAreaZ.Count);
            zSpawn = spawnAreaZ[Zindex];

            // random Pose
            int planeIndex = Random.Range(0, visualizer.Length);
            Vector3 spawnPoint = visualizer[planeIndex].m_DetectedPlane.CenterPose.position;
            spawnPoint.x += Random.Range(-.1f, .1f);
            spawnPoint.z += Random.Range(-.1f, .1f);
            spawnPoint.y += 0.6f;

            //Spawn ghost into the map and add into live display.
            // GameObject ghost = Instantiate(_enemy[randomRange], new Vector3(xSpawn, ySpawn, zSpawn), Quaternion.identity);
            GameObject ghost = Instantiate(_enemy, spawnPoint, Quaternion.identity);
            enemies.Add(ghost);
        }
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
