using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    //float dayTimeTimer;
    //float nightTimeTimer;
    //bool isChangingDayNight;
    [SerializeField]
    float eulerAngleX;
    [SerializeField]
    float eulerAngleY;
    [SerializeField]
    float eulerAngleZ;
    GameObject Sun;
    GameObject Moon;
    bool isDaytime;

    public static DayNightCycle Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        //isChangingDayNight = false;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        eulerAngleX = Sun.transform.eulerAngles.x;
        eulerAngleY = Sun.transform.localEulerAngles.y;
        eulerAngleZ = Sun.transform.localEulerAngles.z;
        //eulerAngleXm = Moon.transform.localEulerAngles.x;
        //eulerAngleYm = Moon.transform.localEulerAngles.y;
        //eulerAngleZm = Moon.transform.localEulerAngles.z;

        //dayTimeTimer += Time.deltaTime;
        transform.RotateAround(Vector3.zero, Vector3.right, 0); // to rotate by the side instead of front and back

        //if (isChangingDayNight == true)
        RotatingSun();

        if (eulerAngleX <= 90)
        {
            Debug.Log("Daytime");
            isDaytime = true;
        }

        if (eulerAngleX > 90)
        {
            Debug.Log("Night");
            isDaytime = false;
        }
    }

    void RotatingSun()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, 15.0f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
