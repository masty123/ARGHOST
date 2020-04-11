using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{

    public float speed = 2.0f;
    public float destroyDistance = 100.000000f;
    GameObject camera;

    public List<ParticleSystem> particles;

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("BulletSpawnerPosition");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * speed * transform.forward;
        if(camera != null)
        {
            if (Vector3.Distance(transform.position, camera.transform.position) >= destroyDistance)
            {
                StopAllParticles();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyController temp = collision.gameObject.GetComponent<EnemyController>();
        if (temp != null)
        {
            temp.Defeat();
            StopAllParticles();
            Destroy(gameObject);
        }
        IndependentEnemyController temp2 = collision.gameObject.GetComponent<IndependentEnemyController>();
        if (temp2 != null)
        {
            temp2.isHit = true;
            StopAllParticles();
            Destroy(gameObject);
        }
    }

    private void StopAllParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.transform.SetParent(null);
            particle.enableEmission = false;
            particle.Stop();
        }
    }

}
