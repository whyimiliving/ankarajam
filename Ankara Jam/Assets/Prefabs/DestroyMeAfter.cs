using UnityEngine;

public class DestroyMeAfter : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
}
