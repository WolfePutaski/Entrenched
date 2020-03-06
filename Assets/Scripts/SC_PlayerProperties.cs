using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerProperties : MonoBehaviour
{
    public static SC_PlayerProperties SharedInstance;

    Rigidbody2D playerPhysics;
    //SC_CameraController cameraController;
    public Animator playerAnim;
    public Animator upperAnim;



    [Header("Health")]
    public float HP;
    public float MaxHP;

    [Header("AttackRequest")]

    public int MaxMeleeAttackers = 1;
    public List<GameObject> MeleeAttackers;

    public int MaxRangedAttackers = 1;
    public List<GameObject> RangedAttackers;

    [Header("States")]
    public bool isAiming;

    // Start is called before the first frame update
    void Start()
    {
        SharedInstance = this;
        HP = MaxHP;
        playerPhysics = GetComponent<Rigidbody2D>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
        upperAnim = GameObject.Find("Player_Upper").GetComponent<Animator>();
        //cameraController = FindObjectOfType<SC_CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        IsAim();
    }


    public void GetAttackRequest(GameObject requestor)
    {
        MeleeAttackers.RemoveAll(item => item == null);
        if (MeleeAttackers.Count < MaxMeleeAttackers)
        {
            if (!MeleeAttackers.Contains(requestor))
            {
                requestor.SendMessage("AllowtoAttack");
                MeleeAttackers.Add(requestor);
                Debug.Log("Attack Allowing");
            }
        }
        else { }
    }

    public void IsAim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
        }

        if (isAiming)
        {
            SC_PlayerAim.SharedInstance.allowLookAt = true;

            upperAnim.SetBool("Aim",true);
        }
        else
        {
            SC_PlayerAim.SharedInstance.allowLookAt = false;

            upperAnim.SetBool("Aim", false);
        }
    }


    public void CancelAttacker(GameObject requestor)
    {
        MeleeAttackers.Remove(requestor);
    }

    public void Attacked(float damage, float push)
    {
        //if (playerBlock.isBlocking)
        //{
        //    playerAnim.SetTrigger("AttackBlocked");
        //    //playerPhysics.velocity = new Vector2(0, playerPhysics.velocity.y);
        //    playerPhysics.AddForce(Vector2.right* push);

        //}
        //else
        //{

        //    cameraController.Shake();
        //    //playerPhysics.velocity = new Vector2(0, playerPhysics.velocity.y);
        //    playerPhysics.AddForce(Vector2.right* push);
        //    playerAnim.SetTrigger("IsHurt");
        //    HP -= damage;
        //}
    }

}
