﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SC_PlayerProperties))]
public class SC_PlayerBlock : MonoBehaviour
{
    SC_PlayerProperties playerProperties;
    Animator playerAnim;


    public bool canBlock;
    public bool isBlocking;

    // Start is called before the first frame update
    void Start()
    {
        playerProperties = GetComponent<SC_PlayerProperties>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canBlock)
        {
            {
                if (Input.GetMouseButtonDown(1))
                {
                    isBlocking = true;
                }

                if (Input.GetMouseButtonUp(1))
                {
                    isBlocking = false;
                }
            }

                playerAnim.SetBool("Is Blocking", isBlocking);


            
        }


    }

    void Unblock()
    {
        isBlocking = false;
    }

    void NotAllowBlock()
    {
        canBlock = false;
    }

    void AllowBlock()
    {
        canBlock = true;
    }
}
