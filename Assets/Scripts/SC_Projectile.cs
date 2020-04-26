using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Projectile : MonoBehaviour
{
    public float damage;
    public float criticalModifier;
    public int penetration;
    public float rayRadius;
    public LayerMask whatIsEnemy;
    Rigidbody2D bulletPhysics;
    Collider2D bulletCollider;

    Collider[] nmeHit;

    // Start is called before the first frame update
    void Start()
    {
        bulletPhysics = gameObject.GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //RaycastHit hit;

        //if (Physics.SphereCast(transform.position,1,Vector3.one,out hit))
        //{
        //    hit.collider.gameObject.GetComponent<SC_EnemyProperties>().TakeDamage(damage, 0);
        //}


        //nmeHit = Physics.OverlapSphere(transform.position, rayRadius, whatIsEnemy);
        //if (nmeHit != null)
        //{

        //    foreach (Collider nme in nmeHit)
        //    {
        //        nme.GetComponent<SC_EnemyProperties>().TakeDamage(damage, 0);
        //    }

        //    //Collider2D hitEnemy;
        //    //hitEnemy = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), rayRadius, whatIsEnemy);
        //    //nmeHit = null;
        //}

        //if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), rayRadius, whatIsEnemy))
        //{
        //    Collider2D hitEnemy;
        //    hitEnemy = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), rayRadius, whatIsEnemy);
        //    hitEnemy.GetComponent<SC_EnemyProperties>().TakeDamage(damage, 0);
        //    penetration--;
        //        if (penetration == 0)
        //    {
        //        gameObject.SetActive(false);
        //    }

        //}


    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(BulletDespawn());

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (penetration <= 0 || collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }


        if (collision.gameObject.CompareTag("Enemy"))
        //if (collision.gameObject.layer == whatIsEnemy)
        {
            penetration--;
            Debug.Log("Bullet Hit!");

            if (collision.name == "HeadCollider")
            {
                collision.gameObject.GetComponentInParent<SC_EnemyProperties>().TakeDamage(damage*criticalModifier, 0);

            }

            if (collision.name == "Weakpoint")
            {
                collision.gameObject.GetComponentInParent<SC_EnemyProperties>().TakeDamage(damage * criticalModifier, 0);
                gameObject.SetActive(false);
            }

            if (collision.name == "Enemy_BodyHitBox")
            {
                collision.gameObject.GetComponentInParent<SC_EnemyProperties>().TakeDamage(damage, 0);
            }
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    bulletCollider.isTrigger = false;

    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {

    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    bulletCollider.isTrigger = true;

    //    penetration -= 1;
    //    if (penetration == 0 || collision.gameObject.CompareTag("Wall"))
    //    {
    //        gameObject.SetActive(false);
    //    }

    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Bullet Hit!");
    //        collision.gameObject.GetComponent<SC_EnemyProperties>().TakeDamage(damage, 0);
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
                        //penetration--;

        }
    }



    //private void (Collider2D collision)
    //{

    //}

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rayRadius);
    }

    IEnumerator BulletDespawn()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
