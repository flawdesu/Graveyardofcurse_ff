using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                  // Player
    public SpriteRenderer backgroundRenderer; // ลาก BG มาวางตรงนี้

    private float camHalfWidth;
    private float camHalfHeight;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        if (backgroundRenderer == null)
        {
            Debug.LogError("Background SpriteRenderer not assigned!");
            return;
        }

        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;

        // คำนวณขอบเขตจาก BG
        Bounds bounds = backgroundRenderer.bounds;

        minX = bounds.min.x + camHalfWidth;
        maxX = bounds.max.x - camHalfWidth;
        minY = bounds.min.y + camHalfHeight;
        maxY = bounds.max.y - camHalfHeight;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
