using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Projectile : MonoBehaviour
{
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
        StartCoroutine(BulletDespawn());
    }

    IEnumerator BulletDespawn()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
