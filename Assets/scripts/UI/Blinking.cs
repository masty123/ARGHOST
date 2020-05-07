using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blinking : MonoBehaviour
{
    public Image image;
    [SerializeField] private float Timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        image = this.transform.GetComponent<Image>();
        StartCoroutine(blinkingIcon());
    }

    // Update is called once per frame
    IEnumerator blinkingIcon()
    {
        while(true)
        {
            yield return new WaitForSeconds(Timer);
            image.enabled = !image.enabled;
        }
    }

 
}
