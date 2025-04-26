using UnityEngine;

public class RandomEventTrigger : MonoBehaviour
{
    public GameObject spawnParent;
    public GameObject spawnObj;
    private bool isDone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isDone)
        {
            var spawned = Instantiate(spawnObj, spawnParent.transform);
            isDone = true;
        }
    }
}
