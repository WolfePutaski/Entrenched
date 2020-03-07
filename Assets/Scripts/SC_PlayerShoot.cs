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
        playerArms = GameObject.Find("Player_ArmR");
        muzzlePos = GameObject.Find("Muzzle").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanShoot();
        OnShoot();
    }

    public void OnShoot()
    {
        if (playerProperties.canShoot)
        {
            //Auto
            if (playerProperties.fireType == FireType.Auto)
            {
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("FIRED!");
                    shootRateCount = playerProperties.shootRate;
                    Shoot();
                    playerUpperAnim.SetTrigger("Shoot");
                }
            }

            if (playerProperties.fireType == FireType.Semi)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("FIRED!");
                    shootRateCount = playerProperties.shootRate;
                    Shoot();
                    playerUpperAnim.SetTrigger("Shoot");
                }
            }

            if (playerProperties.fireType == FireType.Manual)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("FIRED!");
                    shootRateCount = playerProperties.shootRate;
                    Shoot();
                    playerUpperAnim.SetTrigger("Shoot");
                    playerUpperAnim.SetTrigger("ManualLoad");
                }
            }



        }
    }

    public void CheckCanShoot()
    {
        //Depend on FireType
        //Manual



        //Automatic & Semi


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
        GameObject bullet = SC_ObjectPooler.SharedInstance.GetPooledObject("Bullet");
        if (bullet != null)
        {
            bullet.transform.position = muzzlePos.transform.position;
            bullet.transform.rotation = muzzlePos.transform.rotation;
            bullet.SetActive(true);

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * playerProperties.bulletSpeed);


        }
    }
}
