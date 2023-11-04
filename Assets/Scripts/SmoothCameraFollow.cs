using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; 
    public Vector3 offset = new Vector3(0, 1, -10); 
    public float lookAheadFactor = 3.0f; 
    public float yDampeningThreshold = 0.05f; 

    private Vector3 lastTargetPosition;

    private void LateUpdate()
    {
        if (target == null) return;
  
        Vector3 lookAhead = lookAheadFactor * (target.position - lastTargetPosition);

        if (Mathf.Abs(target.position.y - lastTargetPosition.y) < yDampeningThreshold)
        {
            lookAhead.y *= 0.5f;
        }

        lastTargetPosition = target.position;
        Vector3 desiredPosition = target.position + offset + lookAhead;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}