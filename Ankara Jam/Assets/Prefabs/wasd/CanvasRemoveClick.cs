using UnityEngine;

public class CanvasRemoveClick : MonoBehaviour
{
    public int requiredPresses = 3;  // How many W presses needed
    private int currentPresses = 0;
    private bool actionDone = false;
    public KeyCode keyCode;

    void Start()
    {
        Time.timeScale = 0.05f;  // Start slow
    }

    void Update()
    {
        if (actionDone)
            return;

        if (Input.GetKeyDown(keyCode))
        {
            currentPresses++;

            if (currentPresses >= requiredPresses)
            {
                Time.timeScale = 1f; // Restore normal speed
                actionDone = true;
                Destroy(gameObject); // Destroy this GameObject
            }
        }
    }
}
