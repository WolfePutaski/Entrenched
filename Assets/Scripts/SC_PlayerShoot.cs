using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SC_PlayerAim))]
[System.Serializable]
public class GunInfo
{
    public Sprite gunSprite; 
}

public class SC_PlayerShoot : MonoBehaviour
{
    GameObject gun;
    Transform muzzlePos;
    GameObject bullet;
    public bool canShoot;
    public float shootRate;
    float shootRateCount;

    public float bulletSpeed;

    Animator playerUpperAnim;
    GameObject playerArms;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetMouseButton(0) && canShoot)
        {
            Debug.Log("FIRED!");
            shootRateCount = shootRate;
            Shoot();
            playerUpperAnim.SetTrigger("Shoot");
        }
    }

    public void CheckCanShoot()
    {
        if (SC_PlayerProperties.SharedInstance.isAiming)
        {
            if (shootRateCount > 0)
            {
                canShoot = false;
                shootRateCount -= Time.deltaTime;
            }
            if (shootRateCount <= 0)
            {
                canShoot = true;
            }
        }
        else
        {
            canShoot = false;
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

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletSpeed);


        }
    }
}
