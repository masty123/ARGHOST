using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Popup text script that will destroy the prefab within seconds
public class popUpText : MonoBehaviour
{
    [SerializeField] float seconds;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, seconds);
    }


}
