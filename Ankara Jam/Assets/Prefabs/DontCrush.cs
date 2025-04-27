using UnityEngine;

public class DontCrush : MonoBehaviour
{
    [SerializeField] private GameObject sfx;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHp.instance.TakeDmg();
        }
    }
}
