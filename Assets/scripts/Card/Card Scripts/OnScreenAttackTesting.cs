using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenAttackTesting : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            AttackGhost atk = FindObjectOfType<AttackGhost>();
            if (atk != null)
            {
                atk.attack();
            }
        }
    }
}
