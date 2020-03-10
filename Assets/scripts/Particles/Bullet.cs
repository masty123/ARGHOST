using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{

    public float speed = 2.0f;
    public float destroyDistance = 100.0f;
    Camera camera;

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * speed * transform.forward;
        if(Vector3.Distance(transform.position, camera.transform.position) >= destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyController temp = collision.gameObject.GetComponent<EnemyController>();
        if (temp != null)
        {
            temp.Defeat();
            Destroy(gameObject);
        }
    }

}
