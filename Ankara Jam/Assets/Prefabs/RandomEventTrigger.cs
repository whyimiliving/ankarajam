using System.Linq;
using UnityEngine;

public class RandomEventTrigger : MonoBehaviour, INextRoad
{
    public GameObject[] spawnParents;
    public GameObject spawnObj;
    private bool isDone;
    public bool isUi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isDone)
        {
            isDone = true;
            SpawnEventGulucuk();
        }
    }

    public void SpawnEventGulucuk()
    {   
        if (isUi)
        {
            CreateRandomUiEvent();
            return;
        }

        if (!spawnObj)
        {
            return;
        }
        int index = Random.Range(0, spawnParents.Length);
        var spawnPoint = spawnParents[index];
        var spawned = Instantiate(spawnObj, spawnPoint.transform);
    }

    public void CreateRandomUiEvent()
    {
        var index = Random.Range(0, SjGameManager.instance.spawnUiParents.Length);
        Debug.Log("hafizanin geldigi index: " + index);
        if (SjGameManager.instance.spawnUiParents[index].transform.childCount > 0)
        {
            for (int i = 0; i < SjGameManager.instance.spawnUiParents.Length; i++)
            {
                var item = SjGameManager.instance.spawnUiParents[i];
                if (item.transform.childCount <= 0)
                {
                    SpawnGulucuk(i);
                }
            }
        }
        else
        {
            SpawnGulucuk(index);
        }
    }

    public void SpawnGulucuk(int index)
    {
        var objIndex = Random.Range(0, SjGameManager.instance.UiCutScenes.Count);
        var spawned = Instantiate(SjGameManager.instance.UiCutScenes[objIndex], SjGameManager.instance.spawnUiParents[index].transform);
        //SjGameManager.instance.UiCutScenes.RemoveAt(objIndex);
    }
}
