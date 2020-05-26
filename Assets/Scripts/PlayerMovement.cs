using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //NOTE(vosure): Think about conventions - public vs private/[SerializeField]
    public float movementSpeed = 5.0f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    //NOTE(vosure): Think about conventions - private/RequireComponent vs public and drag by hands
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //TODO(vosure): Handle input for jumps if needed
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            controller.Move(direction * movementSpeed * Time.deltaTime);
        }
        
    }
}
