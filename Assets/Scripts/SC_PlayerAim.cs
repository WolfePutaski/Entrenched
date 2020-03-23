using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerAim : MonoBehaviour
{
    public static SC_PlayerAim SharedInstance;
    SC_PlayerProperties playerProperties;
    //GameObject aimTarget;
    GameObject crosshair;
    GameObject gunCrosshair;
    GameObject head;
    GameObject arm;

    Vector3 cursor;
    Vector3 crosshairPos;
    Vector3 defaultAimPos;
    public Vector3 aimDir;
    float angle;

   float defaultScaleX;
    // Start is called before the first frame update
    void Start()
    {
        SharedInstance = this;
        crosshair = GameObject.Find("Crosshair");
        gunCrosshair = GameObject.Find("GunCrosshair");
        head = GameObject.Find("Player_Head");
        arm = GameObject.Find("Player_Upper");
        playerProperties = gameObject.GetComponent<SC_PlayerProperties>();
        defaultScaleX = transform.localScale.x;

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
        defaultAimPos = cursor + playerProperties.defaultAimOffset;

        gunCrosshair.SetActive(playerProperties.isAiming);
        if (!playerProperties.isAiming)
        {
            gunCrosshair.transform.position = defaultAimPos;
        }
        if (gunCrosshair.transform.position != defaultAimPos)
        {
            gunCrosshair.transform.position = Vector3.Lerp(gunCrosshair.transform.position, defaultAimPos, Time.deltaTime);
        }

    }

    public void aimKick()
    {
        gunCrosshair.transform.position += new Vector3(0, playerProperties.recoilKick, 0);
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
            if (SC_PlayerProperties.SharedInstance.allowLookAt)
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


        //if (SC_PlayerProperties.SharedInstance.isAiming)
        if (SC_PlayerProperties.SharedInstance.allowLookAt)
        {
            arm.transform.eulerAngles = new Vector3(0, 0, angle);

        }
        else
        {
            arm.transform.eulerAngles = new Vector3(0, 0, 0);

        }
    }
}
