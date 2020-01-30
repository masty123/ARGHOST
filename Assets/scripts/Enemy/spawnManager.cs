using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    //State of SpawnManager
    public enum SpawnState {SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        [Header("Types of enemies")]
        public GameObject[] enemy;
        public int count;
        public float rate;

        public void setCount(int newCount)
        {
            this.count = newCount;
        }

    }

    //Wave properties
    public Wave[] waves;
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
    public int xPosition;
    public int zPosition;

    [Header("Previously randomized Position")]
    [SerializeField]
    public int xSpawn;
    public int zSpawn;


    [Header("Range of spawning time")]
    [SerializeField]
    public float minTime;
    public float maxTime;

    [Header("Live display of enemies in the scene")]
    public List<GameObject> enemies;

    //countdown of search time.
    private float searchCountdown = 1f;

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

        if (waveCountdown <= 0)
        {
            if (state == SpawnState.COUNTING)
            {
                //Start spawning wave
                StartCoroutine(spawnWave(waves[nextWave]));

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
    void spawnEnemy(GameObject[] _enemy)
    {
        if (isAlive())
        {
            float spawnTime = Random.Range(minTime, maxTime);
            timeBetweenWaves = spawnTime;
            int randomRange = Random.Range(0, _enemy.Length);
            xSpawn = Random.Range(-xPosition+1, xPosition-1);
            zSpawn = Random.Range(-zPosition+1, zPosition-1);
            GameObject ghost = Instantiate(_enemy[randomRange], new Vector3(xSpawn, 0.26f, zSpawn), Quaternion.identity);
            Debug.Log(ghost.name+"Spawned");
            enemies.Add(ghost.gameObject);
        }
    }

    //Check whether if enemies are alive.
    bool isAlive()
    {

        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            //if (GameObject.FindGameObjectWithTag("Enemy") == null)
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
