using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public float distance = 4f;
    public float height = 2f;
    public float speed = 5f;
    public Vector2 pitchLimits = new Vector2(-30, 60);

    float yaw = 0f;
    float pitch = 10f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null) return;

        yaw += Input.GetAxis("Mouse X") * speed;
        pitch -= Input.GetAxis("Mouse Y") * speed;
        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rot * new Vector3(0, 0, -distance);

        transform.position = player.position + Vector3.up * height + offset;
        transform.rotation = rot;
    }
}
