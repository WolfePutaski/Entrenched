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
    GameObject playerArms;
    // Start is called before the first frame update
    void Start()
    {
        playerProperties = gameObject.GetComponent<SC_PlayerProperties>();
        playerUpperAnim = GameObject.Find("Player_Upper").GetComponent<Animator>();
        playerAim = GetComponent<SC_PlayerAim>();
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
        playerUpperAnim.SetTrigger("Reload");
    }

    public void FinishReload()
    {
        playerProperties.ammoInMag = playerProperties.magSize;
    }


    public void OnShoot()
    {
        if (playerProperties.canShoot && playerProperties.ammoInMag > 0)
        {
            //Auto
            if (playerProperties.fireType == FireType.Auto)
            {
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
            }

            if (playerProperties.fireType == FireType.Semi)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    shootRateCount = playerProperties.shootRate;
                    Shoot();
                }
            }

            if (playerProperties.fireType == FireType.Manual)
            {
                if (Input.GetMouseButtonDown(0))
                { 
                    Shoot();
                    playerUpperAnim.SetTrigger("ManualLoad");
                }
            }



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
            GameObject bullet = SC_ObjectPooler.SharedInstance.GetPooledObject("Bullet");
            shootRateCount = playerProperties.shootRate;
            playerUpperAnim.SetTrigger("Shoot");
            playerAim.aimKick();
            playerProperties.ammoInMag--;
            //gunkick
            if (bullet != null)
            {
                bullet.transform.position = muzzlePos.transform.position;
                bullet.transform.rotation = muzzlePos.transform.rotation;
                bullet.SetActive(true);

                bullet.GetComponent<SC_Projectile>().damage = playerProperties.shootDamage;
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * playerProperties.bulletSpeed);
            }
        
        
    }
}
