using UnityEngine;

public class RemoveMemory : MonoBehaviour
{
    public int[] myOrder;
    [SerializeField] private Animator myAnimator;

    public void DoAnim(bool isRight)
    {
        if (isRight)
        {
            myAnimator.SetTrigger("right");
        }
        else
        {
            myAnimator.SetTrigger("left");
        }
    }

    public void RemoveChilds()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
