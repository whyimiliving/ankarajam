using UnityEngine;

public class CanvasRemoveConditions : MonoBehaviour
{
    public float holdDuration = 2f;  // Time to hold any key
    private float holdTimer = 0f;
    private bool actionDone = false;
    public KeyCode[] keyCodes; // List of keys to check

    void Start()
    {
        Time.timeScale = 0.05f;  // Start slow
    }

    void Update()
    {
        if (actionDone)
            return;

        if (IsAnyKeyHeld())
        {
            holdTimer += Time.unscaledDeltaTime; // Use unscaled time because timeScale is very small
            if (holdTimer >= holdDuration)
            {
                Time.timeScale = 1f; // Restore normal speed
                actionDone = true;
                Destroy(gameObject); // Destroy this GameObject
            }
        }
        else
        {
            holdTimer = 0f; // Reset timer if none are pressed
        }
    }

    bool IsAnyKeyHeld()
    {
        foreach (KeyCode key in keyCodes)
        {
            if (Input.GetKey(key))
            {
                return true;
            }
        }
        return false;
    }
}
