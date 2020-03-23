using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Projectile : MonoBehaviour
{
    public float damage;
    public LayerMask whatIsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(BulletDespawn());

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bullet Hit!");
            collision.gameObject.GetComponent<SC_EnemyProperties>().TakeDamage(damage, 0);
        }

    }

    //private void (Collider2D collision)
    //{

    //}


    IEnumerator BulletDespawn()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
