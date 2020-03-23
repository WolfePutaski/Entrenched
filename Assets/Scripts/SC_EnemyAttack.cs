using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SC_EnemyProperties))]
public class SC_EnemyAttack : MonoBehaviour
{
    Animator enemyAnim;


    Rigidbody2D enemyPhysics;
    LayerMask whatIsPlayer;

    Collider2D attackTarget;

    SC_EnemyProperties enemyProperties;
    SC_CameraController cameraController;

    



    // Start is called before the first frame update
    void Start()
    {
        enemyPhysics = GetComponent<Rigidbody2D>();
        enemyProperties = GetComponent<SC_EnemyProperties>();
        whatIsPlayer = LayerMask.GetMask("Player");

        cameraController = FindObjectOfType<SC_CameraController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyProperties.canAttack)
        {
            if (enemyProperties.timeBtwAttack <= 0)
            {
                enemyProperties.OnScanning = true;
            }
            else
            {
                enemyProperties.timeBtwAttack -= Time.deltaTime;
            }
        }
        else
        {
            enemyProperties.OnScanning = false;
        }

        if (enemyProperties.OnScanning)
        {
            ScanForAttack();
        }


    }

    void ScanForAttack()
    {
        if (enemyProperties.timeBtwAttack <= 0 && enemyProperties.OnScanning && Mathf.Abs(enemyProperties.distanceFromTarget) < enemyProperties.attackDistance)
        {
            enemyProperties.timeBtwAttack = enemyProperties.startTimeBtwAttack;
            enemyProperties.OnScanning = false;
            enemyProperties.CanMove = false;
            enemyProperties.enemyAnim.SetTrigger("Attack");
            Debug.Log("Enemy Attack Triggered");

        }
    }

}



