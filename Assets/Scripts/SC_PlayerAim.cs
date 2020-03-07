using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerAim : MonoBehaviour
{
    public static SC_PlayerAim SharedInstance;
    GameObject crosshair;
    GameObject head;
    GameObject arm;

    Vector3 cursor;
    Vector3 aimDir;
    float angle;

   float defaultScaleX;
    // Start is called before the first frame update
    void Start()
    {
        SharedInstance = this;
        crosshair = GameObject.Find("Crosshair");
        head = GameObject.Find("Player_Head");
        arm = GameObject.Find("Player_Upper");

        defaultScaleX = transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        crosshair.transform.parent = null;
        LookAt();
    }

    void LookAt()
    {
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshair.transform.position = cursor;

        aimDir = (crosshair.transform.position - arm.transform.position).normalized;

        angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        if (crosshair.transform.position.x > arm.transform.position.x)
        {
            transform.localScale = new Vector2(defaultScaleX, transform.localScale.y);
            arm.transform.localScale = new Vector2(1, 1);
            head.transform.localScale = new Vector2(1, 1);
            head.transform.eulerAngles = new Vector3(0, 0, angle / 2);



        }
        //if (cursor.x <= arm.transform.position.x)
        if (crosshair.transform.position.x <= arm.transform.position.x)

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
