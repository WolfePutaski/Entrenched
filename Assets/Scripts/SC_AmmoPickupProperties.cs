using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AmmoPickupProperties : MonoBehaviour
{
    [System.Serializable]
    public class ammoTypeProperties
    {
        public GameObject weaponAmmoType;
        public int maxAmountToFill;
        public int minAmountToFill;
    }

    public bool isRefillAll;
    public float coolDownTime;
    public Color cooldownColor;
    public Color defaultColor;
    float coolDownCount;
    bool isCooldown;
    SpriteRenderer sprite;
    
    //public ammoTypeProperties typeProperties;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       gameObject.GetComponent<Collider2D>().enabled = !isCooldown;

        if (coolDownCount > 0)
        {
            isCooldown = true;
            sprite.color = cooldownColor;

            coolDownCount -= Time.deltaTime;
        }
        else
        {
            isCooldown = false;
            sprite.color = defaultColor;
        }
    }

    public void CrateCooldown()
    {
        coolDownCount = coolDownTime;
    }
}
