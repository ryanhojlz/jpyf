﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_PS4
using UnityEngine.PS4.VR;
using UnityEngine.PS4;
#endif

using System;

public enum MoveControllerOrientation
{
    Left,
    Right
}

public class MoveController : MonoBehaviour
{

    public bool hand_Enter = false;
    // Debug Reference
   


    [SerializeField]
    private GameObject m_HandModel;

    private MoveControllerOrientation m_Orientation;
    public MoveControllerOrientation orientation {
        get { return m_Orientation; }
        protected set { m_Orientation = value; }
    }

    private Animator m_HandAnimator;

    private int m_ControllerSlot = -1;

    private int m_HandleNumber = -1;

    private int m_ControllerIndex = -1;

    private bool m_IsRegistered = false;

    private bool m_IsGrabbing = false;

    private static GameObject m_CurrentLeftObject = null;
    private static GameObject m_CurrentRightObject = null;

    private GameObject m_Player;

    public Vector3 moveDelta {
        get {
#if UNITY_PS4
            return PS4Input.GetLastMoveGyro(m_ControllerSlot, m_ControllerIndex);
#else
            return Vector3.zero;
#endif
        }
    }

    private void Awake()
    {
        m_CurrentLeftObject = null;
        m_CurrentRightObject = null;
        m_Player = GameObject.FindWithTag("Player");
        m_HandAnimator = m_HandModel.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (m_IsRegistered)
        {
            ProcessControllerMovement();
            ProcessControllerUpdate();
        }
    }

    void Update()
    {
        if (m_IsRegistered)
        {
            ProcessHandAnimation();
            ProcessGrabbing();
            StartMiniGame();
        }
    }

    public void RegisterController(MoveControllerOrientation controllerOrientation, int slotNumber, int handleNumber)
    {
        m_IsRegistered = true;
        m_ControllerSlot = slotNumber;
        m_HandleNumber = handleNumber;
        m_ControllerIndex = (controllerOrientation == MoveControllerOrientation.Left) ? 0 : 1;
        m_Orientation = controllerOrientation;

#if UNITY_PS4
        if (controllerOrientation == MoveControllerOrientation.Left)
        {
            PS4Input.MoveSetLightSphere(slotNumber, m_ControllerIndex, 0, 0, 255);
        }
        else
            PS4Input.MoveSetLightSphere(slotNumber, m_ControllerIndex, 0, 255, 0);

        Tracker.RegisterTrackedDevice(PlayStationVRTrackedDevice.DeviceMove, handleNumber, PlayStationVRTrackingType.Absolute, PlayStationVRTrackerUsage.OptimizedForHmdUser);
#endif

    }

    private void ProcessHandAnimation()
    {
        if (GetButton(MoveControllerHotkeys.buttonGrab))
        {
            m_HandAnimator.SetBool("IsGrabbing", true);
        }
        else
        {
            m_HandAnimator.SetBool("IsGrabbing", false);
        }
    }

    private void ProcessControllerMovement()
    {
#if UNITY_PS4
        Vector3 pos;
        Tracker.GetTrackedDevicePosition(m_HandleNumber, PlayStationVRSpace.Unity, out pos);
        transform.position = pos + m_Player.transform.position;

        Quaternion rot;
        Tracker.GetTrackedDeviceOrientation(m_HandleNumber, PlayStationVRSpace.Unity, out rot);
        transform.rotation = rot;
#endif
    }

    private void ProcessGrabbing()
    {
        var currentObject = GetCurrentHandObject();

        if (currentObject == null)
            return;

        var iObject = currentObject.GetComponent<IInteractable>();

        if (iObject == null)
            return;
        
        var grabbableObject = currentObject.GetComponent<IGrabbable>();

        if (grabbableObject == null)
            return;

        // On Grab (Trigger Down)
        if (GetButtonDown(MoveControllerHotkeys.buttonGrab))
        {
            if (grabbableObject.hideControllerOnGrab)
                m_HandModel.SetActive(false);

            grabbableObject.OnGrab(this);

            m_IsGrabbing = true;

            AssignObjectToHand(m_Orientation, currentObject.gameObject);

            if (currentObject.GetComponent<IStationaryGrabbable>() == null)
            {
                Rigidbody otherRb = currentObject.GetComponent<Rigidbody>();
                otherRb.velocity = Vector3.zero;

                Rigidbody rb = GetComponent<Rigidbody>();
                FixedJoint joint;
                joint = currentObject.GetComponent<FixedJoint>();

                if (joint == null)
                    joint = currentObject.AddComponent<FixedJoint>();

                joint.connectedBody = rb;
                joint.breakForce = Mathf.Infinity;
                joint.breakTorque = Mathf.Infinity;
                joint.enablePreprocessing = false;
            }
        }

        // On Item First Use
        if (GetButtonDown(MoveControllerHotkeys.buttonUse))
        {
            iObject.OnUse(this);
        }

        // On Item Use Released
        if (GetButtonUp(MoveControllerHotkeys.buttonUse))
        {
            iObject.OnUseUp(this);
        }

        // On Grab Released (Trigger Up)
        if (GetButtonUp(MoveControllerHotkeys.buttonGrab))
        {
            DetachCurrentObject(true);
        }

    }

    private void ProcessControllerUpdate()
    {
        var currentObject = GetCurrentHandObject();

        if (currentObject == null)
            return;

        var iObject = currentObject.GetComponent<IInteractable>();

        if (iObject == null)
            return;

        iObject.OnControllerStay(this);

        var grabbableObject = currentObject.GetComponent<IGrabbable>();

        if (grabbableObject == null)
            return;

        // On Item Holding Down Use 
        if (GetButton(MoveControllerHotkeys.buttonUse))
        {
            iObject.OnUseDown(this);
        }

        // On Grab Use
        if (GetButton(MoveControllerHotkeys.buttonGrab) && m_IsGrabbing)
        {
            grabbableObject.OnGrabStay(this);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        var iObject = other.GetComponent<IInteractable>();

        if (GetCurrentHandObject() == null && iObject != null && GetCurrentHandObject(true) != other.gameObject)
        {
            AssignObjectToHand(m_Orientation, other.gameObject);
            iObject.OnControllerEnter(this);
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        var iObject = other.GetComponent<IInteractable>();

        if (iObject != null && GetCurrentHandObject() == null && GetCurrentHandObject() != other.gameObject && GetCurrentHandObject(true) != other.gameObject)
        {
            AssignObjectToHand(m_Orientation, other.gameObject);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        var iObject = other.GetComponent<IInteractable>();

        if (iObject != null)
        {
            if (other.gameObject == GetCurrentHandObject())
            {
                iObject.OnControllerExit(this);
                DetachCurrentObject(false);
            }
        }
    }

    /// <summary>
    /// Detaches the object that this controller is currently holding
    /// </summary>
    /// <param name="transferVelocity">Transfer the current velocity of the controller to the object?</param>
    public void DetachCurrentObject(bool transferVelocity, bool destroyJoint = true)
    {
        m_IsGrabbing = false;

        var currentObject = GetCurrentHandObject();

        if (currentObject != null)
        {
            var grabbableObject = currentObject.GetComponent<IGrabbable>();

            if (grabbableObject != null)
            {
                if (grabbableObject.hideControllerOnGrab)
                    m_HandModel.SetActive(true);

                grabbableObject.OnGrabReleased(this);

                AssignObjectToHand(m_Orientation, null);

                if (destroyJoint && currentObject.GetComponent<FixedJoint>() != null)
                {
                    Destroy(currentObject.GetComponent<FixedJoint>());
                }

                if (currentObject.GetComponent<IStationaryGrabbable>() == null && transferVelocity)
                {
                    Rigidbody rb = currentObject.GetComponent<Rigidbody>();
                    rb.velocity = GetVelocity();
                    rb.angularVelocity = GetAngularVelocity();
                }
            }
        }
    }

    private void AssignObjectToHand(MoveControllerOrientation hand, GameObject go)
    {
        if (hand == MoveControllerOrientation.Left)
            m_CurrentLeftObject = go;
        else
            m_CurrentRightObject = go;
    }

    public GameObject GetCurrentHandObject(bool otherHand = false)
    {
        if (orientation == MoveControllerOrientation.Left)
            return (otherHand) ? m_CurrentRightObject : m_CurrentLeftObject;
        else
            return (otherHand) ? m_CurrentLeftObject : m_CurrentRightObject;
    }

    public static MoveController GetControllerThatHolds(GameObject obj)
    {
        var controllers = FindObjectsOfType<MoveController>();

        foreach (var controller in controllers)
        {
            if (controller.GetCurrentHandObject() == obj)
                return controller;
        }

        return null;
    }

    public MoveController GetOtherController()
    {
        var controllers = FindObjectsOfType<MoveController>();

        foreach (var controller in controllers)
        {
            if (controller.orientation != orientation)
                return controller;
        }

        return null;
    }

    /// <summary>
    /// Same as Unity GetKey
    /// </summary>
    public bool GetButton(MoveControllerButton button)
    {
        return Input.GetKey(GetButtonKeyCode(button));
    }

    /// <summary>
    /// Same as Unity GetKeyUp
    /// </summary>
    public bool GetButtonUp(MoveControllerButton button)
    {
        return Input.GetKeyUp(GetButtonKeyCode(button));
    }

    /// <summary>
    /// Same as Unity GetKeyDown
    /// </summary>
    public bool GetButtonDown(MoveControllerButton button)
    {
        return Input.GetKeyDown(GetButtonKeyCode(button));
    }

    /// <summary>
    /// Returns TRUE if the analog trigger is held down
    /// </summary>
    /// <param name="downThreshold">How far to the trigger is down before returning TRUE</param>    
    public bool GetTriggerDown(int downThreshold)
    {

#if UNITY_PS4
        // Move controllers use an API for their analog buttons, DualShock 4 uses an axis name for R2
        return ((PS4Input.MoveGetAnalogButton(m_ControllerSlot, m_ControllerIndex) > downThreshold) ? true : false);
#else
        return false;
#endif
    }

    public Vector3 GetVelocity()
    {
        Vector3 value = Vector3.zero;
#if UNITY_PS4
        Tracker.GetTrackedDeviceVelocity(m_HandleNumber, out value);
#endif
        return value;
    }

    public Vector3 GetAngularVelocity()
    {
        Vector3 value = Vector3.zero;
#if UNITY_PS4
        Tracker.GetTrackedDeviceAngularVelocity(m_HandleNumber, out value);
#endif
        return -value;
    }

    public int GetControllerSlot()
    {
        return m_ControllerSlot;
    }

    public int GetControllerHandle()
    {
        return m_HandleNumber;
    }

    public int GetControllerIndex()
    {
        return m_ControllerIndex;
    }

    private int GetButtonIndex(MoveControllerButton button)
    {
        // By: Isaac
        // For Dualshock controllers
        // * X                -0 
        // * Circle           -1 
        // * Square           -2
        // * Triangle         -3
        // * BackTrigger      -4
        // * MiddleButton     -5
        // * TouchPad         -6
        // * Start            -7
        switch (button)
        {
            case MoveControllerButton.X:
                return 64;
            case MoveControllerButton.Circle:
                return 32;
            case MoveControllerButton.Square:
                return 128;
            case MoveControllerButton.Triangle:
                return 16;
            case MoveControllerButton.BackTrigger:
                return 2;
            case MoveControllerButton.MiddleButton:
                return 4;
            case MoveControllerButton.Start:
                return 8;
            default:
                return 0;
        }
    }

    public KeyCode GetButtonKeyCode(MoveControllerButton button)
    {
        switch (button)
        {
            case MoveControllerButton.X:
                return (m_Orientation == MoveControllerOrientation.Left) ? KeyCode.Joystick5Button0 : KeyCode.Joystick6Button0;
            case MoveControllerButton.Circle:
                return (m_Orientation == MoveControllerOrientation.Left) ? KeyCode.Joystick5Button1 : KeyCode.Joystick6Button1;
            case MoveControllerButton.Square:
                return (m_Orientation == MoveControllerOrientation.Left) ? KeyCode.Joystick5Button2 : KeyCode.Joystick6Button2;
            case MoveControllerButton.Triangle:
                return (m_Orientation == MoveControllerOrientation.Left) ? KeyCode.Joystick5Button3 : KeyCode.Joystick6Button3;
            case MoveControllerButton.BackTrigger:
                return (m_Orientation == MoveControllerOrientation.Left) ? KeyCode.Joystick5Button4 : KeyCode.Joystick6Button4;
            case MoveControllerButton.MiddleButton:
                return (m_Orientation == MoveControllerOrientation.Left) ? KeyCode.Joystick5Button5 : KeyCode.Joystick6Button5;
            case MoveControllerButton.Start:
                return (m_Orientation == MoveControllerOrientation.Left) ? KeyCode.Joystick5Button7 : KeyCode.Joystick6Button7;

            default:
            case MoveControllerButton.NONE:
                return KeyCode.None;
        }
    }

    bool isVibrating = false;

    /// <summary>
    /// Vibrates the controller - Ranges: (Stop: 0-63) (Vibrate 64-255)
    /// </summary>
    public void Vibrate(int intensity, float durationInSeconds)
    {
        if (isVibrating)
            StopVibration();

        StartCoroutine(VibrateRoutine(intensity, durationInSeconds));
    }

    private IEnumerator VibrateRoutine(int intensity, float durationInSeconds)
    {
#if UNITY_PS4
        PS4Input.MoveSetVibration(m_ControllerSlot, m_ControllerIndex, intensity);
#endif

        isVibrating = true;

        yield return new WaitForSeconds(durationInSeconds);

        StopVibration();
    }

    public void StopVibration()
    {
        StopCoroutine(VibrateRoutine(0, 0));
#if UNITY_PS4
        PS4Input.MoveSetVibration(m_ControllerSlot, m_ControllerIndex, 0);
#endif

        isVibrating = false;
    }


    void StartMiniGame()
    {
        if (GetButtonDown(MoveControllerHotkeys.buttonConfirm))
        {
            GameObject.Find("MiniGame1").GetComponent<HandBuffMiniGame>().SetMiniGame(true);
        }

        if (GetButtonDown(MoveControllerHotkeys.buttonSquare))
        {
            
            //GameObject.Find("DonHandle_1").gameObject.GetComponent<DonHandle>().ResetPos();
            //GameObject.Find("DonHandle").gameObject.GetComponent<DonHandle>().ResetPos();
            
        }

        if (GetButtonDown(MoveControllerHotkeys.buttonTriangle))
        {
            GameObject.Find("MiniGame1").gameObject.GetComponent<GoGoAction>().StartMiniGame();
        }

        if (GetButtonDown(MoveControllerHotkeys.buttonCross))
        {
            GameObject.Find("MiniGame1").gameObject.GetComponent<RelaxAction>().StartMiniGame2();
        }

        if (GetButtonDown(MoveControllerHotkeys.buttonUse))
        {
            //if (GameObject.Find("DonDon").activeSelf == false)
            //{
            //    GameObject.Find("DonDon").SetActive(true);
            //}
            //else if (GameObject.Find("DonDon").activeSelf == true)
            //{
            //    GameObject.Find("DonDon").SetActive(false);
            //}
            GameObject.Find("armthing").GetComponent<CardScript>().SpawnCard("Tank_Card");
            GameObject.Find("armthing").GetComponent<CardScript>().SpawnCard("Healer_Card");
            GameObject.Find("armthing").GetComponent<CardScript>().SpawnCard("Ranger_Card");


        }
    }

}