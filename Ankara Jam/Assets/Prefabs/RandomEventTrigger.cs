using System.Linq;
using UnityEngine;

public class RandomEventTrigger : MonoBehaviour, INextRoad
{
    public GameObject[] spawnParents;
    public GameObject spawnObj;
    private bool isDone;
    public bool isUi;
    public bool isForcedRightUi;
    public bool isForcedLeftUi;
    public bool isForcedADUi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isDone)
        {
            Debug.Log("1aaa1");

            isDone = true;
            if (isForcedLeftUi)
            {
                SpawnEventLeftUi();
            }
            else if (isForcedRightUi)
            {
                SpawnEventRightUi();
            }
            else if (isForcedADUi)
            {
                SpawnEventADUi();
            }
            else
            {
                SpawnEventGulucuk();
            }
        }
    }

    public void SpawnEventADUi() // bunu okuyan kisi ozur dilerim zaman yoktu hardcoded event ekledim
    {
        Debug.Log("11");
        var objIndex = Random.Range(0, SjGameManager.instance.UiCutScenes.Count);
        var spawned = Instantiate(SjGameManager.instance.UiCutScenes[objIndex], SjGameManager.instance.spawnUiParents[2].transform);
        var spawnedCanvas = Instantiate(SjGameManager.instance.tutorials[1]);
    }

    public void SpawnEventRightUi()
    {
        Debug.Log("21");

        var objIndex = Random.Range(0, SjGameManager.instance.UiCutScenes.Count);
        var spawned = Instantiate(SjGameManager.instance.UiCutScenes[objIndex], SjGameManager.instance.spawnUiParents[2].transform);
        var spawnedCanvas = Instantiate(SjGameManager.instance.tutorials[2]);
    }

    public void SpawnEventLeftUi()
    {
        var objIndex = Random.Range(0, SjGameManager.instance.UiCutScenes.Count);
        var spawned = Instantiate(SjGameManager.instance.UiCutScenes[objIndex], SjGameManager.instance.spawnUiParents[0].transform);
        var spawnedCanvas = Instantiate(SjGameManager.instance.tutorials[3]);
    }

    public void SpawnEventGulucuk()
    {   
        if (isUi)
        {
            CreateRandomUiEvent();
        }
        else
        {
            if (!spawnObj)
            {
                return;
            }
            int index = Random.Range(0, spawnParents.Length);
            var spawnPoint = spawnParents[index];
            var spawned = Instantiate(spawnObj, spawnPoint.transform);
        }
    }

    public void CreateRandomUiEvent()
    {
        var index = Random.Range(0, SjGameManager.instance.spawnUiParents.Length);
        if (SjGameManager.instance.spawnUiParents[index].transform.childCount > 0)
        {
            for (int i = 0; i < SjGameManager.instance.spawnUiParents.Length; i++)
            {
                var item = SjGameManager.instance.spawnUiParents[i];
                if (item.transform.childCount <= 0)
                {
                    SpawnGulucuk(i);
                    return;
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
