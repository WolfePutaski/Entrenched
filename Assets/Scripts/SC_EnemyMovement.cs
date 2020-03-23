using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyMovement : MonoBehaviour
{
    Animator enemyAnim;

    [Header("Movement")]
    //public float farDistanceMin;
    //public float farDistanceMax;
    //private float farDistance;
    //public float closeDistance;
    private float defaultScaleX;


    SC_EnemyProperties enemyProperties;
    Rigidbody2D enemyPhysics;

    // Start is called before the first frame update
    void Start()
    {
        enemyProperties = GetComponent<SC_EnemyProperties>();
        enemyPhysics = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        defaultScaleX = transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {

        if (enemyProperties.CanMove)
        {
            FollowPlayer();

        }

    }


    void FollowPlayer()
    {
        if (enemyProperties.distanceFromTarget < 0)
        {
            transform.localScale = new Vector2(defaultScaleX, transform.localScale.y);
        }
        if (enemyProperties.distanceFromTarget > 0)
        {
            transform.localScale = new Vector2(-defaultScaleX, transform.localScale.y);
        }

        if (Mathf.Abs(enemyProperties.distanceFromTarget) > enemyProperties.attackDistance) //walk towards
        {
            enemyPhysics.velocity = new Vector2(transform.localScale.normalized.x * enemyProperties.DefaultSpeed, enemyPhysics.velocity.y);
            enemyAnim.SetFloat("MoveDir", 1);
        }
        //if (Mathf.Abs(distanceFromTarget) < enemyProperties.closeDistance -0.2f) //walk back
        //{
        //    enemyPhysics.velocity = new Vector2(-transform.localScale.normalized.x * enemyProperties.DefaultSpeed, enemyPhysics.velocity.y);
        //    enemyAnim.SetFloat("MoveDir", -1);
        //}

        if (enemyPhysics.velocity.x == 0)
        {
            enemyAnim.SetFloat("MoveDir", 0);
        }

    }
}