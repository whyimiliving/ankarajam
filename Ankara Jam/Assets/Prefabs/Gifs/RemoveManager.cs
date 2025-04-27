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
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("Right");
        }
    }

    public void CheckAndRemove(bool isRight)
    {
        // TODO: el anim isRight oynat
        if (removeMemories[1].gameObject.transform.childCount > 0)
        {
            if (!isRight)
            {
                // TODO: m-l
            }
            else 
            { 
                // TODO: m-r
            }
            return;
        }
        if (!isRight && removeMemories[0].gameObject.transform.childCount > 0)
        {

            // TODO: l-l
            return;
        }
        if (isRight && removeMemories[2].gameObject.transform.childCount > 0)
        {

            // TODO: r-r
            return;
        }
    }
}
