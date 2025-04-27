using System.Collections.Generic;
using UnityEngine;

public class RandomRoadTile : MonoBehaviour
{
    bool isOnce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isOnce)
        {
            isOnce = true;
            CreateRandomTile();
        }
    }

    public void CreateRandomTile()
    {
        var spawnObj = SjGameManager.instance.RandomRoadTiles[Random.Range(0, SjGameManager.instance.RandomRoadTiles.Length)];
        var spawned = Instantiate(spawnObj);
        spawned.transform.position = new Vector3
        (
            this.gameObject.transform.position.x,
            this.gameObject.transform.position.y,
            this.gameObject.transform.position.z - 360
        );
        Destroy(gameObject);
    }
}
