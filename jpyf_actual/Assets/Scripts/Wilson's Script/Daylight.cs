using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daylight : MonoBehaviour
{
    float dayTimeTimer;
    float nightTimeTimer;
    bool isChangingDayNight;

    // Start is called before the first frame update
    void Start()
    {
        isChangingDayNight = false;
    }

    // Update is called once per frame
    void Update()
    {
        dayTimeTimer += Time.deltaTime;
        transform.RotateAround(Vector3.zero, Vector3.right, 0);

        //Debug.Log(dayTimeTimer);
        if (dayTimeTimer > 3f)
        {
            isChangingDayNight = true;
            dayTimeTimer = 0f;
        }

        if (isChangingDayNight == true)
            RotatingSun();

        if(isChangingDayNight == true && dayTimeTimer > 3)
        {
            isChangingDayNight = true;
            dayTimeTimer = 0f;
        }
    }

    void RotatingSun()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, 2.0f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
