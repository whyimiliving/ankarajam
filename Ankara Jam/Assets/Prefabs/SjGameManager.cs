using UnityEngine;

public class SjGameManager : MonoBehaviour
{
    public static SjGameManager instance;
    public GameObject[] RandomEvents;
    public GameObject[] RandomRoadTiles;
    public GameObject[] RandomOtherTiles;

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
