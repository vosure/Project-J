using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzleflash : MonoBehaviour
{
    public GameObject muzzleflashHolder;
    public ParticleSystem muzzleflashEffect;

    public float flashTime;

    void Start()
    {
        Deactivate();
    }

    public void Activate()
    {
        muzzleflashHolder.SetActive(true);
        muzzleflashEffect.Play();

        Invoke("Deactivate", flashTime);
    }

    private void Deactivate()
    {
        muzzleflashHolder.SetActive(false);
    }
}
