using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Movement")]
    [HideInInspector] public bool canMove;
    public float defaultSpeed = 0;
    public float slowWalkSpeed = 0;
    [HideInInspector] public bool allowLookAt;


    [Header("Melee")]
    public Transform meleePos;
    public bool canMelee;
    public float meleeRate;
    public Collider2D[] enemiesToDamage;
    public LayerMask whatIsEnemies;

    public float meleeRadius;
    public float meleeDamage;
    public float meleePushForce;


    [Header("Shooting")]
    public bool isAiming;
    public bool canShoot;

    //WeaponStats for Alpha
    public float shootDamage;
    public float shootRate;
    public float bulletSpeed;
    public FireType fireType;

    [Header("AttackRequest")]

    public int MaxMeleeAttackers = 1;
    public List<GameObject> MeleeAttackers;

    public int MaxRangedAttackers = 1;
    public List<GameObject> RangedAttackers;

    // Start is called before the first frame update
    void Start()
    {
        SharedInstance = this;
        HP = MaxHP;
        playerPhysics = GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
        upperAnim = GameObject.Find("Player_Upper").GetComponent<Animator>();
        //cameraController = FindObjectOfType<SC_CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        IsAim();
    }


    public void GetAttackRequest(GameObject requestor)
    {
        MeleeAttackers.RemoveAll(item => item == null);
        if (MeleeAttackers.Count < MaxMeleeAttackers)
        {
            if (!MeleeAttackers.Contains(requestor))
            {
                requestor.SendMessage("AllowtoAttack");
                MeleeAttackers.Add(requestor);
                Debug.Log("Attack Allowing");
            }
        }
        else { }
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


    public void CancelAttacker(GameObject requestor)
    {
        MeleeAttackers.Remove(requestor);
    }



    public void Attacked(float damage, float push)
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
        //    //playerPhysics.velocity = new Vector2(0, playerPhysics.velocity.y);
        //    playerPhysics.AddForce(Vector2.right* push);
        //    playerAnim.SetTrigger("IsHurt");
        //    HP -= damage;
        //}
    }

    //Movement
    void NotAllowMove()
    {
        canMove = false;
    }

    void AllowMove()
    {
        canMove = true;
    }

    void Detect_MeleeCollision()
    {
        enemiesToDamage = null;

        //Debug.Log("P");
        //playerPhysics.velocity = new Vector2(0, playerPhysics.velocity.y);
        //playerPhysics.AddForce(Vector2.right * gameObject.transform.localScale.x * attackDashForce);

        //Hitbox
        enemiesToDamage = Physics2D.OverlapCircleAll(meleePos.position, meleeRadius, whatIsEnemies);
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleePos.position, meleeRadius);
    }
}
