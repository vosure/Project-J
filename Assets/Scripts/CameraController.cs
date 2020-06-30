using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public Transform cameraHolder;

    Vector3 target, mousePos, refVel;
    public float cameraDist = 1.5f;
    float smoothTime = 0.2f, zStart;

    Vector3 offset;

    //TODO(vosure): Clean it up
    void Start()
    {
        target = player.position; 
        offset = cameraHolder.position - player.position;
        zStart = cameraHolder.position.y;
    }
    void Update()
    {
        mousePos = CaptureMousePos(); 
        target = UpdateTargetPos() + offset; 
        UpdateCameraPosition(); 
    }
    Vector3 CaptureMousePos()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition); 
        ret *= 2;
        ret -= Vector2.one; 
        float max = 0.9f;
        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized; 
        }
        return new Vector3(ret.x, 0.0f, ret.y);
    }
    Vector3 UpdateTargetPos()
    {
        Vector3 mouseOffset = mousePos * cameraDist; 
        Vector3 ret = player.position + mouseOffset; 
        ret.y = zStart; 
        return ret;
    }
    void UpdateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(cameraHolder.position, target, ref refVel, smoothTime); 
        cameraHolder.position = tempPos; 
    }
}
