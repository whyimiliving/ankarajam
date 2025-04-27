using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float lookAmount = 10f; // Kaç derece sağa sola baksın
    public float smoothSpeed = 5f; // Geçişin yumuşaklığı

    private float targetYRotation = 0f;
    private float currentYRotation = 0f;

    void Update()
    {
        // Tuşlara basılınca hedef açıyı ayarla
        if (Input.GetKey(KeyCode.A))
        {
            targetYRotation = -lookAmount;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetYRotation = lookAmount;
        }
        else
        {
            targetYRotation = 0f; // Hiçbir tuşa basılmıyorsa ortala
        }

        // Yumuşak bir şekilde hedef açıya yaklaş
        currentYRotation = Mathf.Lerp(currentYRotation, targetYRotation, Time.deltaTime * smoothSpeed);

        // Kamerayı döndür
        transform.localRotation = Quaternion.Euler(0f, currentYRotation, 0f);
    }
}