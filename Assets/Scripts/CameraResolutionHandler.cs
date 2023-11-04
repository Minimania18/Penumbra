using UnityEngine;

[ExecuteInEditMode]
public class CameraResolutionHandler : MonoBehaviour
{
    public float targetWidth = 10f; 
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        AdjustCameraSize();
    }

    private void AdjustCameraSize()
    {
  
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }

        float currentAspect = (float)Screen.width / Screen.height;
        cam.orthographicSize = targetWidth / (2 * currentAspect);
    }

    private void OnValidate()
    {
        AdjustCameraSize();
    }
}