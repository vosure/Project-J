﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    float speed = 15.0f;
    float damage = 1.0f;
    float lifeTime = 3;
    float skinWidth = 0.1f;

    public GameObject hitEffectPrefab;

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position, new Vector3(0.0f, 0.0f, 0.0f));
        }
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask | LayerMask.GetMask("Obstacle"), QueryTriggerInteraction.Collide))
            OnHitObject(hit.collider, hit.point, hit.normal);
    }

    void OnHitObject(Collider collider, Vector3 hitPoint, Vector3 normal)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
        }
        GameObject.Destroy(gameObject);

        //TODO(vosure): Different hit empacts for different materials (soft body, sand, concrete, wood, etc)
        if (collider.CompareTag("Environment"))
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, collider.gameObject.transform.position, Quaternion.LookRotation(normal)) as GameObject;
            Destroy(hitEffect, 0.5f);
        }
        //TODO(vosure) Piercing shots
    }
}
