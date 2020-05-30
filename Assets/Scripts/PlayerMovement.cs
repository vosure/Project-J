using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    //NOTE(vosure): Think about conventions - public vs private/[SerializeField]
    public float movementSpeed = 5.0f;

    private Vector3 velocity;

    //NOTE(vosure): Think about conventions - private/RequireComponent vs public and drag by hands
    private Rigidbody rigidbody;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }

    public void UpdateVelocity(Vector3 inputVelocity)
    {
        velocity = inputVelocity.normalized * movementSpeed;
    }

    public void LookAt(Vector3 point)
    {
        Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(heightCorrectedPoint);
    }
}
