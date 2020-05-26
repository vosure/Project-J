using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;

    //TODO(vosure): Tune this later
    public float height = 10.0f;
    public float distance = 20.0f;
    public float angle = 45.0f;

    public float smoothSpeed = 0.5f;

    private Vector3 velocity;

    void Start()
    {
        CameraFollow();
    }

    void Update()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        Vector3 position = (Vector3.forward * -distance) + (Vector3.up * height);
        Vector3 rotation = Quaternion.AngleAxis(angle, Vector3.up) * position;

        Vector3 playerPosition = player.position;
        playerPosition.y = 0.0f;

        Vector3 cameraPosition = playerPosition + rotation;
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref velocity, smoothSpeed);
        transform.LookAt(playerPosition);
    }
}
