using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct MapSelection
{
    public GameObject building;
    public string sceneName;
}

public class MapSelectController : MonoBehaviour
{

    public List<MapSelection> maps;
    public Transform cameraTransform;
    public Transform cameraPoint;
    public float lerpTime = 2f;

    int currentMap = 0;
    SimpleLookAt cameraLookAt;

    private void Start()
    {
        cameraLookAt = cameraTransform.GetComponent<SimpleLookAt>();
    }

    private void Update()
    {
        if(cameraTransform != null && cameraPoint != null)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraPoint.position, lerpTime * Time.deltaTime);
        }
        if(cameraLookAt != null)
        {
            cameraLookAt.targetObj = maps[currentMap].building.transform;
        }
    }

    public void NextBuilding()
    {
        if (currentMap + 1 >= maps.Count)
        {
            currentMap = 0;
        }
        else
        {
            currentMap++;
        }
    }

    public void PreviousBuilding()
    {
        if (currentMap - 1 < 0)
        {
            currentMap = maps.Count - 1;
        }
        else
        {
            currentMap--;
        }
    }

    public void LoadMap()
    {
        SceneManager.LoadScene(maps[currentMap].sceneName);
    }

}
