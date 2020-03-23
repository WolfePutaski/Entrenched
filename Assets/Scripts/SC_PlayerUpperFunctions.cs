using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerUpperFunctions : MonoBehaviour
{
    SC_PlayerProperties playerProperties;

    private void Start()
    {
        playerProperties = FindObjectOfType<SC_PlayerProperties>();
    }

    void Detect_MeleeCollision()
    {
        playerProperties.Detect_MeleeCollision();
    }

    void AmmoFullLoaded()
    {
        playerProperties.ammoInMag = playerProperties.magSize;
    }

}
