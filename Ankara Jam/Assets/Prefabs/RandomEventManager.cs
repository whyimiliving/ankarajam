using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public List<Transform> myOnRoadPositions;
    public List<Transform> myNextRoadPositions;

    private void Start()
    {
        var count = Random.Range(1, 4);
        for (int i = 0; i < count; i++)
        {
            CreateRandom();
        }
    }

    public void CreateRandom()
    {
        var spawnObj = SjGameManager.instance.RandomEvents[Random.Range(0, SjGameManager.instance.RandomEvents.Length)];
        var components = spawnObj.GetComponents<Component>();

        foreach (var component in components)
        {
            if (component is INextRoad)
            {
                CreateNextRoadRandomEvent(spawnObj);
                break;
            }
            if (component is IInRoad)
            {
                //IInRoad testComponent = (IInRoad)component;
                CreateOnRoadRandomEvent(spawnObj);
                break;
            }
        }
    }

    public void CreateOnRoadRandomEvent(GameObject spawnObj)
    {
        var index = Random.Range(0, myOnRoadPositions.Count);
        List<Transform> children = new List<Transform>();
        foreach (Transform child in myOnRoadPositions[index])
        {
            children.Add(child.gameObject.transform);
        }

        var spawned = Instantiate(spawnObj, myOnRoadPositions[index]);
        spawned.transform.rotation = myOnRoadPositions[index].transform.rotation;
        spawned.GetComponent<IInRoad>().SetPath(children);
        myOnRoadPositions.RemoveAt(index);
    }

    public void CreateNextRoadRandomEvent(GameObject spawnObj)
    {
        var index = Random.Range(0, myNextRoadPositions.Count);
        var spawned = Instantiate(spawnObj, myNextRoadPositions[index]);
        myNextRoadPositions.RemoveAt(index);
    }

}
