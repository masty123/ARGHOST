using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectGhost : MonoBehaviour
{
    public GameObject closeEnemy;
    public GlitchEffect cameraEffect;
    public float distance = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraEffect = GetComponent<GlitchEffect>();
    }

    // Update is called once per frame
    void Update()
    {
       if(closeEnemy == null)
       {
            cameraEffect.intensity = 0f;
            cameraEffect.colorIntensity = 0f;
            closeEnemy = GameObject.FindGameObjectWithTag("Enemy");
       }
       else if(closeEnemy != null)
       {
            gitchIntensity();
       }
        //Debug.Log("cannot detect any ghost");
    }

    public void gitchIntensity()
    {
            float temp = distance / Vector3.Distance(transform.position, closeEnemy.transform.position);
            cameraEffect.intensity = 0.25f * temp;
            cameraEffect.flipIntensity = 0.05f * temp;
            cameraEffect.colorIntensity = 0.15f * temp;
    }

    public virtual void OnDrawGizmos()
    {


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);

    }




}
