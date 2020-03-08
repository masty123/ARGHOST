﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGhost : MonoBehaviour
{

    Camera camera;
    public Bullet bulletPrefab;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    public void attack()
    {
        GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    /*
    public void attack()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.gameObject.GetComponent<EnemyController>())
            {
                hit.transform.gameObject.GetComponent<EnemyController>().Defeat();
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
    */
}
