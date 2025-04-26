using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public float rotationSpeed = 200f; // saniyede dönüş hızı
    public float returnSpeed = 300f;   // merkezlenme hızı
    public float maxRotation = 45f;    // maksimum sağ-sol açı sınırı

    private float currentRotation = 0f;

    void Update()
    {
        float input = 0f;

        if (Input.GetKey(KeyCode.A))
            input = -1f;
        else if (Input.GetKey(KeyCode.D))
            input = 1f;

        if (input != 0f)
        {
            // A veya D basılıyken normal döndür
            currentRotation += input * rotationSpeed * Time.deltaTime;
            currentRotation = Mathf.Clamp(currentRotation, -maxRotation, maxRotation);
        }
        else
        {
            // Hiç basılmıyorsa direksiyon yavaşça sıfıra dönsün
            currentRotation = Mathf.MoveTowards(currentRotation, 0f, returnSpeed * Time.deltaTime);
        }

        transform.localRotation = Quaternion.Euler(-14.500f, 180, currentRotation);
    }
}