using System.Collections.Generic;
using UnityEngine;

public class SjGameManager : MonoBehaviour
{
    public static SjGameManager instance;
    public GameObject[] RandomEvents;
    public GameObject[] RandomRoadTiles;
    public GameObject[] RandomOtherTiles;
    public GameObject[] RandomCars;
    public GameObject[] spawnUiParents;
    public List<GameObject> UiCutScenes;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        { 
            Destroy(this);
        }
    }
}
