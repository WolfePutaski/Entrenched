using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerUpperFunctions : MonoBehaviour
{
    SC_PlayerProperties playerProperties;

    private void Start()
    {
        playerProperties = FindObjectOfType<SC_PlayerProperties>();
    }

    void Detect_MeleeCollision()
    {
        playerProperties.Detect_MeleeCollision();
    }

    //Reload - Get the ammo to Load value (pull ammo to load from reserve ammo pool) then add it to the current magazine;

    void AmmoFullLoaded()
    {
        var ammoToLoad = Mathf.Clamp(playerProperties.currentGunInfo.magSize - playerProperties.ammoInMag, 0, playerProperties.reserveAmmo[playerProperties.currentWeaponSlot]);
        playerProperties.reserveAmmo[playerProperties.currentWeaponSlot] -= ammoToLoad;

        playerProperties.ammoInMag += ammoToLoad;
    }
    void AmmoFullLoadWithChanmber()
    {
        var ammoToLoad = Mathf.Clamp(playerProperties.currentGunInfo.magSize - playerProperties.ammoInMag + 1, 0, playerProperties.reserveAmmo[playerProperties.currentWeaponSlot]);

        playerProperties.reserveAmmo[playerProperties.currentWeaponSlot] -= ammoToLoad;

        playerProperties.ammoInMag += ammoToLoad;

    }

    void AmmoOneLoad()
    {
        playerProperties.reserveAmmo[playerProperties.currentWeaponSlot]--;

        playerProperties.ammoInMag++;
        if (playerProperties.ammoInMag < playerProperties.currentGunInfo.magSize)
        {
            playerProperties.gameObject.GetComponent<SC_PlayerShoot>().Reload();
        }
    }

    void PlaySound_Shoot()
    {
        playerProperties.GetComponent<AudioSource>().PlayOneShot(playerProperties.currentGunInfo.shootSound);
    }

    void PlayeSound_Bolt()
    {
        playerProperties.GetComponent<AudioSource>().PlayOneShot(playerProperties.currentGunInfo.boltSound);

    }




}
