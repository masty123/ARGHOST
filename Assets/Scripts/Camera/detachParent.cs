using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detachParent : MonoBehaviour
{
    [SerializeField] float seconds;
    private bool detachChild;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DetachParent());
    }

    IEnumerator DetachParent()
    {
        yield return new WaitForSeconds(seconds);
        transform.parent = null;
        
    }
}
