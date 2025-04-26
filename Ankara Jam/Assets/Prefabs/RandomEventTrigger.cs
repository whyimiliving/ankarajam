using UnityEngine;

public class RandomEventTrigger : MonoBehaviour, INextRoad
{
    public GameObject[] spawnParents;
    public GameObject spawnObj;
    private bool isDone;
    private bool isUi;

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
            spawnParents = SjGameManager.instance.spawnUiParents;
        }

        int index = Random.Range(0, spawnParents.Length);
        var spawnPoint = spawnParents[index];
        if (isUi)
        {
            var spawned = Instantiate(spawnObj, spawnPoint.transform);
        }
        else
        {
            var spawned = Instantiate(spawnObj, spawnPoint.transform);
        }

    }
}
