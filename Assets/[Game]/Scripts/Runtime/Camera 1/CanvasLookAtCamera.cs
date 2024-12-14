using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;  // Ana kamerayı bul
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            // Canvas'ı tamamen kameraya döndür
            Vector3 direction = mainCamera.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(-direction); // Canvas'ın doğru tarafa bakmasını sağlar
        }
    }
}
