using UnityEngine;

public class RemoveManager : MonoBehaviour
{
    public RemoveMemory[] removeMemories;

    void Update()
    {
        // TODO: oyunda eli algilamazsa diye 12 ile de tetiklenecek
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("Left");
            CheckAndRemove(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("Right");
            CheckAndRemove(true);
        }
    }

    public void CheckAndRemove(bool isRight)
    {
        // TODO: el anim isRight oynat
        if (removeMemories[1].gameObject.transform.childCount > 0)
        {
            if (!isRight)
            {
                removeMemories[1].DoAnim(false);
                Debug.Log("1");

            }
            else 
            {
                Debug.Log("2");

                removeMemories[1].DoAnim(true);
            }
            return;
        }
        if (!isRight && removeMemories[0].gameObject.transform.childCount > 0)
        {
            Debug.Log("3");

            removeMemories[0].DoAnim(false);
            return;
        }
        if (isRight && removeMemories[2].gameObject.transform.childCount > 0)
        {
                Debug.Log("4");
            removeMemories[2].DoAnim(true);
            return;
        }
    }
}
