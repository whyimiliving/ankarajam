using UnityEngine;

public class DontCrush : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("gg");
        }
    }
}
