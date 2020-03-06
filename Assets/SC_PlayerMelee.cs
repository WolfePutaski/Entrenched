using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerMelee : MonoBehaviour
{

    Animator upperAnim;
    public bool canMelee;

    public float meleeRate;
    float meleeRateCount;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetMouseButtonDown(0) && canMelee)
        {
            Debug.Log("MELEE!");
            upperAnim.SetTrigger("Melee");
             meleeRateCount = meleeRate;

        }
    }

    public void CheckCanMelee()
    {
        if (!SC_PlayerProperties.SharedInstance.isAiming)
        {
            if (meleeRateCount > 0)
            {
                canMelee = false;
                meleeRateCount -= Time.deltaTime;
                SC_PlayerAim.SharedInstance.allowLookAt = true;
            }
            if (meleeRateCount <= 0)
            {
                canMelee = true;
                SC_PlayerAim.SharedInstance.allowLookAt = false;
            }
        }
        else
        {
            canMelee = false;
        }

    }
}

