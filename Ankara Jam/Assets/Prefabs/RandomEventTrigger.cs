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
        if (SjGameManager.instance.spawnUiParents[index].transform.childCount > 0)
        {
            CreateRandomUiEvent();
            return;
        }
        var objIndex = Random.Range(0, SjGameManager.instance.UiCutScenes.Count);
        var spawned = Instantiate(SjGameManager.instance.UiCutScenes[objIndex], SjGameManager.instance.spawnUiParents[index].transform);
        //SjGameManager.instance.UiCutScenes.RemoveAt(objIndex);
    }
}
