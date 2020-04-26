using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerAim : MonoBehaviour
{
    SC_PlayerProperties playerProperties;
    //GameObject aimTarget;
    GameObject cameraHolder;
    GameObject cameraHolder2;
    GameObject crosshair;
    GameObject gunCrosshair;
    Vector3 initialGunCrosshairPos;
    public GameObject gunSight;
    GameObject head;
    GameObject arm;
    GameObject leftArm;

    Vector3 cursor;
    Vector3 crosshairPos;
    Vector3 defaultAimPos;
    public Vector3 aimDir;
    float angle;

    //Sway
    GameObject gunSway;
    Vector3 nextSwayPos;
    public float lerpPara = 0;


    public float aimRadius;

   float defaultScaleX;
    // Start is called before the first frame update
    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        gunSway = GameObject.Find("GunSway");
        gunCrosshair = GameObject.Find("GunCrosshair");
        gunSight = GameObject.Find("GunSight");
        head = GameObject.Find("Player_Head");
        arm = GameObject.Find("Player_Upper");
        cameraHolder = GameObject.Find("Camera Holder");
        cameraHolder2 = GameObject.Find("Camera Holder 2");
        playerProperties = gameObject.GetComponent<SC_PlayerProperties>();
        defaultScaleX = transform.localScale.x;
        nextSwayPos = Random.insideUnitSphere;


    }

    // Update is called once per frame
    void Update()
    {
        //crosshair.transform.parent = null;
        LookAt();
        CrosshairUpdate();
    }

    void CrosshairUpdate()
    {
        defaultAimPos = cursor;
        gunSight.transform.localPosition = playerProperties.currentGunInfo.defaultAimOffset;
        gunCrosshair.SetActive(playerProperties.isAiming);
        if (!playerProperties.isAiming)
        {
            //gunCrosshair.transform.position = defaultAimPos;
        }
        if (gunCrosshair.transform.position != defaultAimPos)
        {
            gunCrosshair.transform.position = Vector3.Lerp(gunCrosshair.transform.position, defaultAimPos, playerProperties.currentGunInfo.recoilRecovery * /*Time.deltaTime **/ Mathf.Sin(Time.deltaTime));
        }
        //sway

        if (lerpPara < playerProperties.currentGunInfo.swaySpeed)
        {
            lerpPara += Time.deltaTime;
            gunSway.transform.localPosition = Vector2.Lerp(gunSway.transform.localPosition, nextSwayPos*playerProperties.currentGunInfo.swayRadius, Mathf.Sin(Time.deltaTime)/12);
        }
        else
        {
            nextSwayPos = Random.insideUnitSphere;
            lerpPara = 0;

        }

        //if (lerpPara <= 1f)
        //{
        //    lerpPara += Mathf.Sin(Time.deltaTime * 2/** swaySpeed)*/);
        //    gunSway.transform.position = Vector3.Lerp(gunSway.transform.localPosition, nextSwayPos, /*swaySpeed*/lerpPara);
        //}
        //else
        //{
        //    nextSwayPos = gunSway.transform.localPosition + Random.insideUnitSphere;
        //    lerpPara = 0;

        //}
        //{
        //    lerpPara += Time.deltaTime /* * swaySpeed*/;
        //    if (lerpPara >= 0.9)

        //    {
        //        initialPos = gunCrosshair.transform.position;
        //        nextPos = defaultAimPos += Random.insideUnitSphere /* * swayRadius*/;
        //        lerpPara = 0;

        //    }
        //    if (gunCrosshair.transform.position != defaultAimPos)
        //    {
        //        gunCrosshair.transform.position = Vector3.Lerp(initialPos, nextPos, 30 * lerpPara);
        //    }


        //}

    }

    public IEnumerator aimKick()
    {
        yield return new WaitForSeconds(0.04f);
        gunCrosshair.transform.position += new Vector3(0, playerProperties.currentGunInfo.recoilKick, 0)
            + Random.insideUnitSphere * playerProperties.currentGunInfo.recoilKick /** Mathf.Sqrt(Random.Range(0.0f, 1.0f))*/;
    }

    void LookAt()
    {
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshairPos = crosshair.transform.position;
        crosshair.transform.position = cursor;
        

        aimDir = (gunCrosshair.transform.position - arm.transform.position).normalized;

        angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        if (cursor.x > arm.transform.position.x)
        {
            transform.localScale = new Vector2(defaultScaleX, transform.localScale.y);
            arm.transform.localScale = new Vector2(1, 1);
            head.transform.localScale = new Vector2(1, 1);
            head.transform.eulerAngles = new Vector3(0, 0, angle / 2);



        }
        //if (cursor.x <= arm.transform.position.x)
        if (cursor.x <= arm.transform.position.x)

        {
            transform.localScale = new Vector2(-defaultScaleX, transform.localScale.y);

            if (playerProperties.allowArmLookAt)
            {
                arm.transform.localScale = new Vector2(-1, -1);

            }
            else
            {
                arm.transform.localScale = new Vector2(1, 1);
            }

            head.transform.localScale = new Vector2(1, 1);
            head.transform.eulerAngles = new Vector3(0, 0, (180 + angle) / 2);
            if (angle >= 90)
            {
                head.transform.eulerAngles = new Vector3(0, 0, (angle - 180) / 2);
            }

        }

        if (playerProperties.allowArmLookAt)
        {
            arm.transform.eulerAngles = new Vector3(0, 0, angle);


        }
        else
        {
            arm.transform.eulerAngles = new Vector3(0, 0, 0);

        }

        if (Input.GetMouseButton(1))
        {
            cameraHolder2.transform.position = Vector3.Lerp(cameraHolder.transform.position, new Vector3(crosshairPos.x, crosshairPos.y, cameraHolder.transform.position.z), 0.4f);

        }
        else
        {
            cameraHolder2.transform.localPosition = Vector3.Lerp(cameraHolder2.transform.localPosition, Vector3.zero, 10* Time.deltaTime);

        }


    }
}
