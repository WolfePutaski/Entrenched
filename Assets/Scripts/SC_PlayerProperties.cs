using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public enum FireType
//{
//    Manual, Semi, Auto
//};

//public enum WeaponType
//{
//    OneHand, TwoHand
//};

//[System.Serializable]
//public struct WeaponInformation
//{
//    public float shtDamage;
//    public float shtRate;
//    public float bulSpeed;
//    public Sprite wpnSprite;
//    public FireType fireType;
//};
public class SC_PlayerProperties : MonoBehaviour
{
    Rigidbody2D playerPhysics;
    //SC_CameraController cameraController;
    Animator playerAnim;
    GameObject playerUpper;

    Animator upperAnim;
    //Animator gunAutoAnim;




    [Header("Health")]
    public float HP;
    public float MaxHP;
    //UI
    public Text healthText;


    [Header("Movement")]
    public bool canMove;
    public float defaultSpeed = 0;
    public float slowWalkSpeed = 0;
    public bool allowArmLookAt;


    [Header("Melee")]
    //public Transform meleePos;
    public Transform meleePosA;
    public Transform meleePosB;
    public bool canMelee;
    public float meleeRate;
    public Collider2D[] enemiesToDamage;
    public LayerMask whatIsEnemies;

    public float buttRange;
    public float meleeRadius;
    public float meleeDamage;
    public float meleePushForce;


    [Header("Shooting")]
    public int currentWeaponSlot;

    //public Vector3 defaultAimOffset;
    public bool isAiming;
    public bool canShoot;

    //WeaponStats for Alpha
    public WeaponInformation currentGunInfo;
    public int ammoInMag;

    public List<GameObject> demoGuns;
    public List<int> ammoInGuns;
    public List<int> reserveAmmo;


    //[System.Serializable]
    //public class WeaponStats
    //{
    //    public float shootDamage;
    //    public float shootRate;
    //    public int magSize;
    //    public float bulletSpeed;
    //    public float recoilKick;
    //    public FireType fireType;
    //}


    // Start is called before the first frame update
    void Start()
    {   

        playerPhysics = GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
        playerUpper = GameObject.Find("Player_Upper");
        upperAnim = playerUpper.GetComponent<Animator>();
        //gunAutoAnim = GameObject.Find("Gun").GetComponent<Animator>();

        for (int i = 0; i<demoGuns.Count; i++)
        {
            ammoInGuns.Add(0);
            if (demoGuns[i].GetComponent<SC_GunProperties>().wpnInformation.isInfiniteAmmo)
            {
                reserveAmmo.Add(99);
            }
            else
            {
                reserveAmmo.Add((demoGuns[i].GetComponent<SC_GunProperties>().wpnInformation.maxReserveAmmo + demoGuns[i].GetComponent<SC_GunProperties>().wpnInformation.magSize));

            }
        }
        HP = MaxHP;
        healthText = GameObject.Find("Health Text").GetComponent<Text>();


        //cameraController = FindObjectOfType<SC_CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        IsAim();
        UpdateUI();
        WeaponSelect();


        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void IsAim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            allowArmLookAt = false;
        }

        if (isAiming)
        {
            allowArmLookAt = true;

            upperAnim.SetBool("Aim",true);
        }
        else
        {
            upperAnim.SetBool("Aim", false);
        }
    }

    //public void AimOffsetUpdate()
    //{

    //}


    public void TakeDamage(float damage, float push)
    {
        //if (playerBlock.isBlocking)
        //{
        //    playerAnim.SetTrigger("AttackBlocked");
        //    //playerPhysics.velocity = new Vector2(0, playerPhysics.velocity.y);
        //    playerPhysics.AddForce(Vector2.right* push);

        //}
        //else
        //{

        //    cameraController.Shake();
        canMove = false;
        playerPhysics.velocity = new Vector2(0, playerPhysics.velocity.y);
        playerPhysics.AddForce(Vector2.right * push,ForceMode2D.Impulse);
        playerAnim.SetTrigger("Hurt");
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        HP -= damage;
        //}
    }

    //Movement
    void Deactive_Movement()
    {
        canMove = false;
    }

    void Active_Movement()
    {
        canMove = true;
    }

    public void Detect_MeleeCollision()
    {
        enemiesToDamage = null;

        //Debug.Log("P");
        //playerPhysics.velocity = new Vector2(0, playerPhysics.velocity.y);
        //playerPhysics.AddForce(Vector2.right * gameObject.transform.localScale.x * attackDashForce);

        //Hitbox
        //enemiesToDamage = Physics2D.OverlapCircleAll(meleePos.position, meleeRadius, whatIsEnemies);
        enemiesToDamage = Physics2D.OverlapAreaAll(meleePosA.position, meleePosB.position, whatIsEnemies);
        if (enemiesToDamage.Length > 0)
        {
            //cameraController.Shake();

            foreach (Collider2D enemy in enemiesToDamage)
            {
                Debug.Log("We hit " + enemy.name);
                //enemy.transform.position = new Vector2(attackPos.position.x, enemy.transform.position.y);
                enemy.GetComponentInParent<SC_EnemyProperties>().TakeDamage(meleeDamage, meleePushForce * gameObject.transform.localScale.x);

                //enemy.GetComponent<SC_EnemyProperties>().TakeDamage(meleeDamage, meleePushForce * gameObject.transform.localScale.x); //getcomponent and takedamage
            }
        }
    }

    void Return_Layer()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(meleePos.position, meleeRadius);
    }

    void WeaponSelect()
    {
        int toGun = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            toGun = 0;
            WeaponSelect2(toGun);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            toGun = 1;
            WeaponSelect2(toGun);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            toGun = 2;
            WeaponSelect2(toGun);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            toGun = 3;
            WeaponSelect2(toGun);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            toGun = 4;
            WeaponSelect2(toGun);
        }

        var currentGun = demoGuns[currentWeaponSlot].GetComponent<SC_GunProperties>().wpnInformation;

        currentGunInfo = currentGun;
        ammoInGuns[currentWeaponSlot] = ammoInMag;

        for (int i = 0; i < demoGuns.Count; i++)
        {
            if (demoGuns[i].GetComponent<SC_GunProperties>().wpnInformation.isInfiniteAmmo)
            {
                reserveAmmo[i] = 99;
            }
        }
        upperAnim.runtimeAnimatorController = currentGun.weaponAnim;





    }

    void WeaponSelect2(int x)
    {
        upperAnim.Play("PlayerUpper_Idle");

        ammoInGuns[currentWeaponSlot] = ammoInMag;

        currentWeaponSlot = x;
        ammoInMag = ammoInGuns[currentWeaponSlot];
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("AmmoPickup"))
        {
            var ammoProp = collision.GetComponent<SC_AmmoPickupProperties>();

            if (ammoProp.isRefillAll)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < demoGuns.Capacity; i++)
                    {
                        reserveAmmo[i] = demoGuns[i].GetComponent<SC_GunProperties>().wpnInformation.maxReserveAmmo + (demoGuns[i].GetComponent<SC_GunProperties>().wpnInformation.magSize - ammoInGuns[i]);
                    }

                    ammoProp.CrateCooldown();
                }

            }
        }
    }

    void UpdateUI()
    {
        if (HP > 0)
        {
            healthText.text = string.Format("HP: {0}\nGun Damage: {1}\nFire Rate: {2}\nFire Mode: {3}\nAmmo: {4}/{5}", HP, currentGunInfo.shootDamage, currentGunInfo.shootRate, currentGunInfo.fireType,ammoInMag, reserveAmmo[currentWeaponSlot]);

        }
        if (HP <= 0)
        {
            healthText.text = "DEAD";
        }
    }
}
