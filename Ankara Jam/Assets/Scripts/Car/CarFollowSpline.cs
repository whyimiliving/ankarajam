using System.Collections.Generic;
using UnityEngine;

public class CarSplineFollower : MonoBehaviour
{
    public List<Transform> splinePoints; // Takip edilecek spline noktaları
    public float speed = 5f;              // Arabanın hızı
    public float turnSpeed = 5f;           // Dönme hızı
    private int currentPointIndex = 0;     // Şu anki hedef nokta

    void Update()
    {
        if (splinePoints == null || splinePoints.Count == 0)
            return;

        if (currentPointIndex == splinePoints.Count - 1)
        {
            return;
        }

        // Hedef nokta
        Transform targetPoint = splinePoints[currentPointIndex];

        // Hedefe doğru yönelme
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // İleri hareket
        transform.position += transform.forward * speed * Time.deltaTime;

        // Hedefe çok yaklaştıysa bir sonrakine geç
        if (Vector3.Distance(transform.position, targetPoint.position) < 1f)
        {
            currentPointIndex++;
        }
    }
}