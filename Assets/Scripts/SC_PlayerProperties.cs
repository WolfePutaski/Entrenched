using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FireType
{
    Manual, Semi, Auto
};

[System.Serializable]
public struct WeaponInformation
{
    public float shtDamage;
    public float shtRate;
    public float bulSpeed;
    public Sprite wpnSprite;
    public FireType fireType;
};
public class SC_PlayerProperties : MonoBehaviour
{
    public static SC_PlayerProperties SharedInstance;

    Rigidbody2D playerPhysics;
    //SC_CameraController cameraController;
    Animator playerAnim;
    Animator upperAnim;


    [Header("Health")]
    public float HP;
    public float MaxHP;
    //UI
    public Text healthText;


    [Header("Movement")]
    public bool canMove;
    public float defaultSpeed = 0;
    public float slowWalkSpeed = 0;
    [HideInInspector] public bool allowLookAt;


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
    public Vector3 defaultAimOffset;
    public Vector3 aimOffset;
    public bool isAiming;
    public bool canShoot;

    //WeaponStats for Alpha
    public float shootDamage;
    public float shootRate;
    public float recoilKick;
    public float bulletSpeed;
    public FireType fireType;
    public int magSize;
    public int ammoInMag;

    public List<WeaponStats> demoGuns;

    [System.Serializable]
    public class WeaponStats
    {
        public float shootDamage;
        public float shootRate;
        public int magSize;
        public float bulletSpeed;
        public float recoilKick;
        public FireType fireType;
    }


    // Start is called before the first frame update
    void Start()
    {
        SharedInstance = this;
        playerPhysics = GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
        upperAnim = GameObject.Find("Player_Upper").GetComponent<Animator>();

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
        }

        if (isAiming)
        {
            allowLookAt = true;

            upperAnim.SetBool("Aim",true);
        }
        else
        {
            allowLookAt = false;

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
        playerPhysics.AddForce(Vector2.right * push);
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
                enemy.GetComponent<SC_EnemyProperties>().TakeDamage(meleeDamage, meleePushForce * gameObject.transform.localScale.x); //getcomponent and takedamage
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
        int i = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            i = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            i = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            i = 2;
        }

        shootDamage = demoGuns[i].shootDamage;
        shootRate = demoGuns[i].shootRate;
        magSize = demoGuns[i].magSize;
        fireType = demoGuns[i].fireType;
        bulletSpeed = demoGuns[i].bulletSpeed;
        recoilKick = demoGuns[i].recoilKick;
        
    }

    void UpdateUI()
    {
        if (HP > 0)
        {
            healthText.text = string.Format("HP: {0}\nGun Damage: {1}\nFire Rate: {2}\nFire Mode: {3}", HP, shootDamage,shootRate,fireType);

        }
        if (HP <= 0)
        {
            healthText.text = "DEAD";
        }
    }
}
