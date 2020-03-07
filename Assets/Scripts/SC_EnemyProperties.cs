using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyProperties : MonoBehaviour
{
    [Header("Health")]
    public float defaultHP;
    float HP;

    //HP REgen
    public float regenDelay;
    float regenDelayCount = 0;
    float regenRateCount = 0;
    public float regenPerSec;

    [Header("Movement")]
    public bool CanMove = true;
    public float DefaultSpeed;


    [Header("Attack")]
    public float damage;
    public float attackPush;
    public bool OnScanning = true;
    public float startTimeBtwAttack;
    public float attackRadius;

    public float attackDash;
    public Transform attackPos;


    Rigidbody2D enemyPhysics;
    Animator enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        enemyPhysics = gameObject.GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        HP = defaultHP;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (HP<= 0)
        {
            enemyAnim.SetTrigger("Die");
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }

    public void TakeDamage(float damage, float push)
    {
        //regenDelayCount = regenDelay;
        HP -= damage;

        enemyPhysics.velocity = new Vector2(0, enemyPhysics.velocity.y);
        enemyPhysics.AddForce(Vector2.right * push);

        //enemyAnim.SetTrigger("Hurt");
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

    public void Die()
    {
       Destroy(gameObject);
    }
}
