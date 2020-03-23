using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SC_PlayerProperties))]
public class SC_PlayerMovement : MonoBehaviour
{
   
    Rigidbody2D playerPhysics;
    Animator playerAnim;
    SC_PlayerProperties playerProperties;

    //public float rollForce;
    float playerSpeed = 0;


    // Start is called before the first frame update
    void Awake()
    {
        playerProperties = gameObject.GetComponent<SC_PlayerProperties>();
        playerPhysics = gameObject.GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerProperties.canMove)
        {       
                Movement();

            //if (Input.GetKeyDown(KeyCode.V)
            //    && Input.GetAxisRaw("Horizontal") != 0)
            //{
            //    Roll();
            //}
        }


    }

    //void Roll()
    //{
    //    playerPhysics.velocity = new Vector2(0, playerPhysics.velocity.y);
    //    playerPhysics.AddForce(Vector2.right * transform.localScale.x * rollForce, ForceMode2D.Impulse);
    //    playerAnim.SetTrigger("Pressed Roll");


    //}

    void Movement()
    {

        playerSpeed = playerProperties.defaultSpeed;


        playerPhysics.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed * Time.deltaTime, playerPhysics.velocity.y);
        playerAnim.SetFloat("Moving", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }

    void OnDodge()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    void ReturnDodge()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}

