using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct MapSelection
{
    public string sceneText;
    public string sceneName;
    public Color textColor;
}

public class MapSelectController : MonoBehaviour
{

    public List<MapSelection> maps;
    public List<GameObject> buildings;
    public Transform cameraTransform;
    public Transform cameraPoint;
    public float lerpTime = 2f;
    public Text levelText;

    List<Transform> selectableBuildings;
    int currentMap = 0;
    SimpleLookAt cameraLookAt;

    private void Start()
    {
        selectableBuildings = new List<Transform>();
        cameraLookAt = cameraTransform.GetComponent<SimpleLookAt>();
        foreach(GameObject building in buildings)
        {
            building.SetActive(false);
        }
        int i = 0;
        while (i < maps.Count)
        {
            int temp = Random.Range(0, buildings.Count);
            if (!selectableBuildings.Contains(buildings[temp].GetComponentInChildren<TextMeshPro>().transform))
            {
                buildings[temp].SetActive(true);
                /*
                buildings[temp].GetComponentInChildren<TextMeshPro>().text = maps[i].sceneText;
                buildings[temp].GetComponentInChildren<TextMeshPro>().color = maps[i].textColor;
                */
                selectableBuildings.Add(buildings[temp].transform);
                i++;
            }
        }
    }

    private void Update()
    {
        if(cameraTransform != null && cameraPoint != null)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraPoint.position, lerpTime * Time.deltaTime);
        }
        if(cameraLookAt != null)
        {
            cameraLookAt.targetObj = selectableBuildings[currentMap];
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
