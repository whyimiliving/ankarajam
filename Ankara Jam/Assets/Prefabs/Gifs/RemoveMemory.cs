using UnityEngine;

public class RemoveMemory : MonoBehaviour
{
    public int[] myOrder;
    [SerializeField] private Animator myAnimator;

    public void DoAnim(bool isRight)
    {

    }

    public void RemoveChilds()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
