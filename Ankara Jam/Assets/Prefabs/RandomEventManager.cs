using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public List<Transform> myPositions;

    private void Start()
    {
        var index = Random.Range(0, myPositions.Count);
        CreateRandomEvent(index);
        var index2 = Random.Range(0, myPositions.Count);
        if (index != index2)
        {
            CreateRandomEvent(index2);
        }
    }

    public void CreateRandomEvent(int index)
    {
        var spawnObj = SjGameManager.instance.RandomEvents[Random.Range(0, SjGameManager.instance.RandomEvents.Length)];
        var spawned = Instantiate(spawnObj, myPositions[index]);
    }
}
