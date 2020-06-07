using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //TODO(vosure): Choose which one is better / CameraController!?
    //NOTE(vosure): Don't use it, but probably we will need it later
    public enum CameraFollowType
    {
        Static,
        Dynamic
    };

    public CameraFollowType followType;
    public Transform player;

    //TODO(vosure): Tune this later
    public float height = 10.0f;
    public float distance = 20.0f;
    public float angle = 45.0f;
    public float smoothSpeed = 0.5f;

    public float smoothing = 1.0f;
    private Vector3 offset;

    private Vector3 velocity;

    void Start()
    {
        if (followType == CameraFollowType.Dynamic)
        {
            DynamicCameraFollow();
        }
        else
        {
            offset = transform.position - player.position;
            StaticCameraFollow();
        }
    }

    void FixedUpdate()
    {
        if (followType == CameraFollowType.Dynamic)
        {
            DynamicCameraFollow();
        }
        else
        {
            StaticCameraFollow();
        }
    }

    private void StaticCameraFollow()
    {
        Vector3 targetCamPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.fixedDeltaTime);
    }

    private void DynamicCameraFollow()
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
