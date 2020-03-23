using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SC_PlayerProperties))]
public class SC_PlayerMelee : MonoBehaviour
{
    SC_PlayerProperties playerProperties;
    Animator upperAnim;
    float meleeRateCount;

    // Start is called before the first frame update
    void Start()
    {
        playerProperties = gameObject.GetComponent<SC_PlayerProperties>();
        upperAnim = GameObject.Find("Player_Upper").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanMelee();
        OnMelee();
    }

    public void OnMelee()
    {
        if (Input.GetMouseButtonDown(0) && playerProperties.canMelee)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), Vector2.right * transform.localScale.x, playerProperties.buttRange, playerProperties.whatIsEnemies);
            if (hit.collider != null)
            {
                upperAnim.SetTrigger("MeleeButt");
                Debug.Log("BUTT!");

            }
            else
            {
                upperAnim.SetTrigger("Melee");
            }

            Debug.Log("MELEE!");
             meleeRateCount = playerProperties.meleeRate;

        }
    }

    public void CheckCanMelee()
    {
        if (!SC_PlayerProperties.SharedInstance.isAiming)
        {
            if (meleeRateCount > 0)
            {
                playerProperties.canMelee = false;
                meleeRateCount -= Time.deltaTime;
                playerProperties.allowLookAt = true;
            }
            if (meleeRateCount <= 0)
            {
                playerProperties.canMelee = true;
                playerProperties.allowLookAt = false;
            }
        }
        else
        {
            playerProperties.canMelee = false;
        }

    }



   
}

