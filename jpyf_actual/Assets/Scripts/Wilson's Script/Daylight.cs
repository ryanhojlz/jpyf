using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daylight : MonoBehaviour
{
    //float dayTimeTimer;
    //float nightTimeTimer;
    //bool isChangingDayNight;
    bool isDay;
    [SerializeField]
    float eulerAngleX;
    [SerializeField]
    float eulerAngleY;
    [SerializeField]
    float eulerAngleZ;
    [SerializeField]
    float eulerAngleXm;
    [SerializeField]
    float eulerAngleYm;
    [SerializeField]
    float eulerAngleZm;
    GameObject Sun;
    GameObject Moon;

    // Start is called before the first frame update
    void Start()
    {
        //isChangingDayNight = false;
        Sun = GameObject.Find("Sun");
        Moon = GameObject.Find("Moon");
    }

    // Update is called once per frame
    void Update()
    {
        eulerAngleX = Sun.transform.localEulerAngles.x;
        eulerAngleY = Sun.transform.localEulerAngles.y;
        eulerAngleZ = Sun.transform.localEulerAngles.z;
        eulerAngleXm = Moon.transform.localEulerAngles.x;
        eulerAngleYm = Moon.transform.localEulerAngles.y;
        eulerAngleZm = Moon.transform.localEulerAngles.z;

        //dayTimeTimer += Time.deltaTime;
        transform.RotateAround(Vector3.zero, Vector3.right, 0); // to rotate by the side instead of front and back

        //if (isChangingDayNight == true)
        RotatingSun();

        if (/*(*/eulerAngleX == 360f /*> 0f && eulerAngleX < 180f) && (eulerAngleX > 180f && eulerAngleX < 360f)*/)
        {
            Debug.Log("Daytime");
        }
    }

    void RotatingSun()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, 15.0f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
