using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyProperties : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D enemyPhysics;
    [HideInInspector] public Animator enemyAnim;

    [Header("Health")]
    public float defaultHP;
    float HP;

    //HP REgen
    public float regenDelay;
    //float regenDelayCount = 0;
    float regenRateCount = 0;
    public float regenPerSec;

    [Header("Movement")]
    public bool CanMove = true;
    public float distanceFromTarget;
    public float DefaultSpeed;
    public GameObject Target;



    [Header("Attack")]
    public bool canAttack = true;
    public bool OnScanning = true;
    public float attackDistance;
    public float attackDamage;
    public float attackDash;
    public float attackPush;
    public float startTimeBtwAttack;
    public float attackRadius;
    [HideInInspector] public float timeBtwAttack;
    public LayerMask whatIsPlayer;
    public Transform attackPos;
    public Collider2D attackTarget;



    // Start is called before the first frame update
    void Start()
    {
        enemyPhysics = gameObject.GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        HP = defaultHP;
        Target = GameObject.Find("Player");


    }

    // Update is called once per frame
    void Update()
    {
        distanceFromTarget = transform.position.x - Target.transform.position.x;

        //if (regenDelayCount <= 0)
        //{
        //    if (HP > 0 && HP < defaultHP)
        //    {
        //        HealthRegen();
        //    }
        //}
        //else
        //{
        //    regenDelayCount -= Time.deltaTime;
        //}

        if (HP <= 0)
        {
            CanMove = false;
            canAttack = false;
            enemyAnim.SetTrigger("Die");
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            //gameObject.tag = "Untagged";
        }


    }

    public void TakeDamage(float damage, float push)
    {
        //regenDelayCount = regenDelay;
        if (HP > 0)
        {
            HP -= damage;

            CanMove = false;
            enemyPhysics.velocity = new Vector2(0, enemyPhysics.velocity.y);
            enemyPhysics.AddForce(Vector2.right * push);
            enemyAnim.SetTrigger("Hurt");
        }



        //if (HP <= 0)
        //{
        //    CanMove = false;
        //    canAttack = false;
        //    enemyAnim.SetTrigger("Die");
        //    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //}
    }

    public void HealthRegen()
    {
        if (regenRateCount <= 0)
        {
            HP += regenPerSec;
            regenRateCount = 1;
        }
        else
        {
            regenRateCount -= Time.deltaTime;
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);
    }

    public void AllowMoving()
    {
        CanMove = true;
    }

    public void NotAllowMoving()
    {
        CanMove = false;
    }

    public void Die()
    {
       Destroy(gameObject);
    }

    public void Attack()
    {
        enemyPhysics.velocity = new Vector2(0, enemyPhysics.velocity.y);
        enemyPhysics.AddForce(Vector2.right * gameObject.transform.localScale.x *attackDash);
    }

    //Attack Function

    public void Detect_MeleeCollision()
    {
        enemyPhysics.AddForce(Vector2.right * gameObject.transform.localScale.x * attackDash);
        Debug.Log("MELEE DETECTION!");


        //Hitbox
        attackTarget = null;
        attackTarget = Physics2D.OverlapCircle(attackPos.position, attackRadius, whatIsPlayer);
        if (attackTarget != null)
        {
            //cameraController.Shake();

            
                Debug.Log("Player is Hit");
                //enemy.transform.position = new Vector2(attackPos.position.x, enemy.transform.position.y);
                attackTarget.gameObject.GetComponent<SC_PlayerProperties>().TakeDamage(attackDamage, attackPush * gameObject.transform.localScale.x); //getcomponent and takedamage
            
        }
    }

    void Active_ScanforAttack()
    {
        OnScanning = true;
    }

    void Deactive_ScanforAttack()
    {
        OnScanning = false;
    }
}
