using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Gracz do �ledzenia
    public Vector3 offset = new Vector3(0, 5, -10); // Offset kamery wzgl�dem gracza
    public float smoothSpeed = 0.125f; // Szybko�� �ledzenia

    private void LateUpdate()
    {
        if (target != null)
        {
            // Oblicz now� pozycj� kamery
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Ustaw now� pozycj� kamery
            transform.position = smoothedPosition;
        }
    }
}