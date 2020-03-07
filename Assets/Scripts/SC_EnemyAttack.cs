using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyAttack : MonoBehaviour
{
    Animator enemyAnim;

    float timeBtwAttack;

    Rigidbody2D enemyPhysics;
    GameObject Target;
    LayerMask whatIsPlayer;

    Collider2D attackTarget;

    SC_EnemyProperties enemyProperties;
    SC_CameraController cameraController;
    SC_EnemyMovement enemyMovement;

    



    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        enemyPhysics = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<SC_EnemyMovement>();
        enemyProperties = GetComponent<SC_EnemyProperties>();
        enemyAnim = GetComponent<Animator>();
        whatIsPlayer = LayerMask.GetMask("Player");
        cameraController = FindObjectOfType<SC_CameraController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeBtwAttack <= 0)
        {
            enemyProperties.OnScanning = true;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (enemyProperties.OnScanning && enemyMovement.IsEngaging)
        {
            ScanForAttack();
        }


    }

    public void ScanForAttack()
    {
        if (timeBtwAttack <= 0 && enemyProperties.OnScanning && Mathf.Abs(enemyMovement.distanceFromTarget) < enemyMovement.closeDistance)
        {
            timeBtwAttack = enemyProperties.startTimeBtwAttack;
            enemyProperties.OnScanning = false;
            enemyMovement.CancelAttack();
            enemyAnim.SetTrigger("Attack");
            Debug.Log("Enemy Attack Triggered");

        }
    }

    public void Attack()
    {
        enemyPhysics.velocity = new Vector2(0, enemyPhysics.velocity.y);
        enemyPhysics.AddForce(Vector2.right * gameObject.transform.localScale.x * enemyProperties.attackDash);

        enemyMovement.CancelAttack(); 



    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyProperties.attackPos.position, enemyProperties.attackRadius);
    }
    


    void AllowtoAttack()
    {
        enemyProperties.OnScanning = true;
    }

    void NotAllowtoAttack()
    {
        enemyProperties.OnScanning = false;
    }
}



