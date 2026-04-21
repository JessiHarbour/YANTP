using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float pixelsPerUnit = 16f;

    void LateUpdate()
    {
        if (target == null) return;

        float unit = 1f / pixelsPerUnit;

        Vector3 targetPos = target.position + offset;

        
        targetPos.x = Mathf.Round(targetPos.x / unit) * unit;
        targetPos.y = Mathf.Round(targetPos.y / unit) * unit;

        transform.position = new Vector3(
            targetPos.x,
            targetPos.y,
            transform.position.z
        );
    }
}