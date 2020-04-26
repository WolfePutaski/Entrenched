using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[System.Serializable]

public struct WeaponInformation
{
    [System.Serializable]

    public class ShotshellProperties
    {
        public int pelletCount;
        public float pelletSpread;
    }


    public enum FireType
    {
        Manual, Semi, Auto
    };

    public enum HandType
    {
        OneHand, TwoHand
    };
    public enum RelaodType
    {
        Clip, MagazineOpenBolt, MagazineCloseBolt, SingleLoad
    }

    public enum ShotType
    {
        Bullet, Pellets
    }

    public AnimatorOverrideController weaponAnim;
    //public AnimatorOverrideController weaponAutoAnim;

    public HandType handType;
    public Vector3 defaultAimOffset;

    [Header("Damage")]
    public float shootDamage;
    public float criticalModifier;
    public float shootRate;
    public ShotType shotType;
    public ShotshellProperties shotshellProperties;
    public int penetration;

    [Header("Magazine")]
    public bool isInfiniteAmmo;
    public int maxReserveAmmo;
    public int magSize;
    public int clipSize;
    //public int ammoInMag;
    public RelaodType relaodType;

    [Header("Handling")]
    public float recoilKick;
    [Range(1, 60)]
    public float recoilRecovery;
    public float swayRadius;
    public float swaySpeed;


    public float bulletSpeed;
    public FireType fireType;
    public bool hasBayonet;

    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip boltSound;


};

public class SC_GunProperties : MonoBehaviour
{
    public WeaponInformation wpnInformation;

}