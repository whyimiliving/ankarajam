using UnityEngine;
using UnityEngine.UI;

public class ChangeRawImageTexture : MonoBehaviour
{
    public RawImage rawImage;          // Assign your RawImage here
    public Texture[] textures;         // Assign textures you want to cycle through
    public float changeInterval = 1f;  // Time (seconds) between changes

    private int currentIndex = 0;
    private float timer = 0f;

    void Update()
    {
        if (textures.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= changeInterval)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % textures.Length;
            rawImage.texture = textures[currentIndex];
        }
    }
}
