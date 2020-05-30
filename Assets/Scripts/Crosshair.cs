using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    //NOTE(vosure): layer for enemies
    public LayerMask targetMask;

    public SpriteRenderer sprite;

    //NOTE(vosure): change crosshair color 
    public Color highlightColor;
    Color originalColor;

    private void Start()
    {
        originalColor = sprite.color;
    }

    void Update()
    {
        Cursor.visible = false;
        transform.Rotate(Vector3.forward * 40 * Time.deltaTime);
    }

    public void DetectTarget(Ray ray)
    {
        if (Physics.Raycast(ray, 100, targetMask))
        {
            sprite.color = highlightColor;
        }
        else
        {
            sprite.color = originalColor;
        }
    }

    //TODO(vosure): dynamic crosshair if enemy hit
}
