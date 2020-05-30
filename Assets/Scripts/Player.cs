using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(FirearmWeaponController))]
public class Player : MonoBehaviour
{
    public Crosshair crosshair;

    private PlayerMovement movementController;
    private FirearmWeaponController firearmWeaponController;

    private Camera mainCamera;

    private void Start()
    {
        // TODO(vosure): base.start() later, inherit from living entity
    }

    // Update is called once per frame
    private void Awake()
    {
        movementController = GetComponent<PlayerMovement>();
        firearmWeaponController = GetComponent<FirearmWeaponController>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 moveVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        movementController.UpdateVelocity(moveVelocity);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * 1.551f);

        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            movementController.LookAt(point);

            crosshair.transform.position = point;
            crosshair.DetectTarget(ray);
        }
    }


}
