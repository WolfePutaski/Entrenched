using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SC_PlayerProperties))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SC_PlayerAim))]

[System.Serializable]
public class GunInfo
{
    public Sprite gunSprite; 
}

public class SC_PlayerShoot : MonoBehaviour
{
    SC_PlayerProperties playerProperties;
    SC_PlayerAim playerAim;
    GameObject gun;
    Transform muzzlePos;
    GameObject bullet;
    float shootRateCount;


    Animator playerUpperAnim;
    Animator gunAutoAnim;
    GameObject playerArms;
    // Start is called before the first frame update
    void Start()
    {
        playerProperties = gameObject.GetComponent<SC_PlayerProperties>();
        playerUpperAnim = GameObject.Find("Player_Upper").GetComponent<Animator>();
        playerAim = GetComponent<SC_PlayerAim>();
        gunAutoAnim = GameObject.Find("Gun").GetComponent<Animator>();
        playerArms = GameObject.Find("Player_ArmR");
        muzzlePos = GameObject.Find("Muzzle").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanShoot();
        OnShoot();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (playerProperties.reserveAmmo[playerProperties.currentWeaponSlot] > 0)
        {
            if (playerProperties.currentGunInfo.relaodType == WeaponInformation.RelaodType.Clip)
            {

                playerUpperAnim.SetTrigger("ReloadEmpty");
            }

            if (playerProperties.currentGunInfo.relaodType == WeaponInformation.RelaodType.MagazineCloseBolt)
            {
                if (playerProperties.ammoInMag <= playerProperties.currentGunInfo.magSize && playerProperties.ammoInMag > 0)
                {
                    playerUpperAnim.SetTrigger("ReloadPartial");

                }
                if (playerProperties.ammoInMag == 0)
                {
                    playerUpperAnim.SetTrigger("ReloadEmpty");
                }
            }

            if (playerProperties.currentGunInfo.relaodType == WeaponInformation.RelaodType.MagazineOpenBolt)
            {
                if (playerProperties.ammoInMag < playerProperties.currentGunInfo.magSize && playerProperties.ammoInMag > 0)
                {
                    playerUpperAnim.SetTrigger("ReloadPartial");

                }
                if (playerProperties.ammoInMag == 0)
                {
                    playerUpperAnim.SetTrigger("ReloadEmpty");
                }
            }


            if (playerProperties.currentGunInfo.relaodType == WeaponInformation.RelaodType.SingleLoad)
            {
                if (playerProperties.ammoInMag < playerProperties.currentGunInfo.magSize && playerProperties.ammoInMag > 0)
                {
                    playerUpperAnim.SetTrigger("ReloadPartial");

                }
                if (playerProperties.ammoInMag == 0)
                {
                    playerUpperAnim.SetTrigger("ReloadEmpty");
                }

            }
        }
        
    }


    public void OnShoot()
    {
        if (playerProperties.canShoot && playerProperties.ammoInMag > 0)
        {
            //Auto
            if (playerProperties.currentGunInfo.fireType == WeaponInformation.FireType.Auto)
            {
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
            }

            if (playerProperties.currentGunInfo.fireType == WeaponInformation.FireType.Semi)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    shootRateCount = playerProperties.currentGunInfo.shootRate;
                    Shoot();
                }
            }

            if (playerProperties.currentGunInfo.fireType == WeaponInformation.FireType.Manual)
            {
                if (Input.GetMouseButtonDown(0))
                { 
                    Shoot();
                    if (playerProperties.ammoInMag != 0)
                    {
                        playerUpperAnim.SetTrigger("ManualLoad");
                    }
                }
            }



        }
        if (playerProperties.ammoInMag <= 0 && playerProperties.currentGunInfo.fireType != WeaponInformation.FireType.Manual)
        {
            playerUpperAnim.SetFloat("isEmpty",1);
        }
        else
        {
            playerUpperAnim.SetFloat("isEmpty", 0);

        }


    }

    public void CheckCanShoot()
    {
        if (shootRateCount > 0)
        {
            shootRateCount -= Time.deltaTime;
        }
        if (shootRateCount <= 0)
        {
            shootRateCount = 0;
        }

        if (playerProperties.isAiming)
        {
            if (shootRateCount > 0)
            {
                playerProperties.canShoot = false;
            }
            if (shootRateCount <= 0)
            {
                playerProperties.canShoot = true;
            }
        }
        else
        {
            playerProperties.canShoot = false;
        }
        
    }

    public void Shoot()
    {
        
        Debug.Log("FIRED!");
        shootRateCount = playerProperties.currentGunInfo.shootRate;
        playerUpperAnim.SetTrigger("Shoot");
        playerAim.StartCoroutine("aimKick");
        playerProperties.ammoInMag--;
        if (playerProperties.currentGunInfo.shotType == WeaponInformation.ShotType.Pellets)
        {
            for (int i = 0; i < playerProperties.currentGunInfo.shotshellProperties.pelletCount; i++)
            {
                SpawnBullet(true);
            }
        }
        if (playerProperties.currentGunInfo.shotType == WeaponInformation.ShotType.Bullet)
        {
            SpawnBullet(false);
        }

        void SpawnBullet(bool isPellet)
        {
            GameObject bullet = SC_ObjectPooler.SharedInstance.GetPooledObject("Bullet");
            if (bullet != null)
            {
                var bulletProperties = bullet.GetComponent<SC_Projectile>();

                bullet.transform.position = muzzlePos.transform.position;
                bullet.transform.rotation = muzzlePos.transform.rotation;
                bullet.SetActive(true);
                if (isPellet == true)
                {
                    bullet.transform.Rotate(0, 0, Random.Range(-playerProperties.currentGunInfo.shotshellProperties.pelletSpread, playerProperties.currentGunInfo.shotshellProperties.pelletSpread));
                    bulletProperties.damage = playerProperties.currentGunInfo.shootDamage / playerProperties.currentGunInfo.shotshellProperties.pelletCount;

                }

                else
                {
                    bulletProperties.damage = playerProperties.currentGunInfo.shootDamage;

                }


                bulletProperties.penetration = playerProperties.currentGunInfo.penetration;
                bulletProperties.criticalModifier = playerProperties.currentGunInfo.criticalModifier;
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * playerProperties.currentGunInfo.bulletSpeed);
            }
        }
        
        
    }
}
