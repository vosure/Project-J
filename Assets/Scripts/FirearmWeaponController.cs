using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmWeaponController : MonoBehaviour
{
    public Transform weaponHold;
    public FirearmWeapon[] allGuns;
    FirearmWeapon equippedGun;

    void Start()
    {
    }

    private void Update()
    {
        //NOTE(vosure): Just for testing, delete later
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipGun(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipGun(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            EquipGun(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            EquipGun(4);
    }

    public void EquipGun(FirearmWeapon gunToEquip)
    {
        if (equippedGun != null)
            Destroy(equippedGun.gameObject);

        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as FirearmWeapon;
        equippedGun.transform.parent = weaponHold;
    }

    public void EquipGun(int weaponIndex)
    {
        EquipGun(allGuns[weaponIndex]);
    }

    public void Aim(Vector3 aimPoint)
    {
        if (equippedGun != null)
            equippedGun.Aim(aimPoint);
    }

    public void Reload()
    {
        if (equippedGun != null)
            equippedGun.Reload();
    }

    public void OnTriggerHold()
    {
        if (equippedGun != null)
            equippedGun.OnTriggerHold();
    }

    public void OnTriggerRelease()
    {
        if (equippedGun != null)
            equippedGun.OnTriggerRelease();
    }
}
