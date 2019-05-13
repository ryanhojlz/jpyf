using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiToolScript : GrabbableObject
{
    // Only one instance of this tool so yea
    public static MultiToolScript Instance = null;

    // Check whether tool is released
    public bool b_ToolReleased = true;

    // Reset rotation
    Vector3 localResetPos = Vector3.zero;

    // Original Rotation
    Vector3 originalLocalAngles = Vector3.zero;

    // Timer to reset object
    public float timerToReset;
    // Angle of reset
    Vector3 ocd = new Vector3(0, 0, 90);

    // Move Controller Reference
    MoveController controllerRef = null;

    public Transform[] ListOfTools;
   

    // Use this for initialization
    void Start ()
    {
        localResetPos = this.transform.localPosition;
        originalLocalAngles = this.transform.eulerAngles;
        b_ToolReleased = true;

        for (int i = 0; i < transform.childCount; ++i)
        {
            ListOfTools[i] = transform.GetChild(i).transform;
            
        }

        ListOfTools[0].gameObject.SetActive(false);
        ListOfTools[1].gameObject.SetActive(false);
        ListOfTools[2].gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update ()
    {
        PositionResetCode();
        ProcessSwapping();
    }

    public override void OnGrab(MoveController currentController)
    {
        base.OnGrab(currentController);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        b_ToolReleased = false;
        controllerRef = currentController;
    }

    public override void OnGrabReleased(MoveController currentController)
    {
        base.OnGrabReleased(currentController);
        b_ToolReleased = true;
        timerToReset = 0;
        controllerRef = null;
    }

    // Code to reset the position
    void PositionResetCode()
    {
        if (b_ToolReleased)
        {
            timerToReset += 1 * Time.deltaTime;
        }
        else
        {
            timerToReset = 0;
        }
        if (timerToReset >= 3.5f)
        {
            timerToReset = 0;
            this.transform.localPosition = localResetPos;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.transform.eulerAngles = originalLocalAngles;
            ListOfTools[0].gameObject.SetActive(false);
            ListOfTools[1].gameObject.SetActive(false);
            ListOfTools[2].gameObject.SetActive(false);
            //b_ToolReleased = false;
        }
    }

    // Swapping of multi tool
    void ProcessSwapping()
    {
#if UNITY_PS4
        if (controllerRef)
        {
            // Cross button
            if (controllerRef.GetButtonDown(MoveControllerHotkeys.buttonCross))
            {
                ListOfTools[0].gameObject.SetActive(false);
                ListOfTools[1].gameObject.SetActive(false);
                ListOfTools[2].gameObject.SetActive(false);
            }
            // Circle button
            else if (controllerRef.GetButtonDown(MoveControllerHotkeys.buttonConfirm))
            {
                ListOfTools[0].gameObject.SetActive(false);
                ListOfTools[1].gameObject.SetActive(false);
                ListOfTools[2].gameObject.SetActive(true);
            }
            // Square button // Change to hammer
            else if (controllerRef.GetButtonDown(MoveControllerHotkeys.buttonSquare))
            {
                ListOfTools[0].gameObject.SetActive(true);
                ListOfTools[1].gameObject.SetActive(false);
                ListOfTools[2].gameObject.SetActive(false);

            }
            // Triangle button // Change to weapon
            if (controllerRef.GetButtonDown(MoveControllerHotkeys.buttonTriangle))
            {
                ListOfTools[0].gameObject.SetActive(false);
                ListOfTools[1].gameObject.SetActive(true);
                ListOfTools[2].gameObject.SetActive(false);
            }

            if (ListOfTools[2].gameObject.activeInHierarchy)
            {
                if (controllerRef.GetButton(MoveControllerHotkeys.buttonUse))
                {
                    ListOfTools[2].transform.GetChild(0).GetComponent<RangeAttackScript>().SpawnBullet();
                }
            }
            
        }
        
#endif

        if (Input.GetKeyDown(KeyCode.T))        
        {
            ListOfTools[0].gameObject.SetActive(false);
            ListOfTools[1].gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ListOfTools[0].gameObject.SetActive(true);
            ListOfTools[1].gameObject.SetActive(false);
        }


    }




}
