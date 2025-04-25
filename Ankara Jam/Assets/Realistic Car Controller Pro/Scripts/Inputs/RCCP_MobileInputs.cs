//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright � 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Receives inputs from the ui controllers. RCCP_InputManager will process these inputs if controller type is mobile.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/UI/Mobile/RCCP Mobile Inputs")]
public class RCCP_MobileInputs : RCCP_GenericComponent {

    private static RCCP_MobileInputs instance;

    /// <summary>
    /// Instance of the class.
    /// </summary>
    public static RCCP_MobileInputs Instance {

        get {

            if (instance == null)
                instance = FindObjectOfType<RCCP_MobileInputs>();

            return instance;

        }

    }

    /// <summary>
    /// Root canvas to enable / disable depends on the selected option in the RCCP_Settings ("Use Mobile Controller").
    /// </summary>
    public GameObject mobileCanvas;

    //  UI controller buttons.
    public RCCP_UIController throttle;
    public RCCP_UIController brake;
    public RCCP_UIController left;
    public RCCP_UIController right;
    public RCCP_UIController ebrake;
    public RCCP_UIController nos;

    /// <summary>
    /// Steering wheel.
    /// </summary>
    public RCCP_UI_SteeringWheelController steeringWheel;

    /// <summary>
    /// Joystick.
    /// </summary>
    public RCCP_UI_Joystick joystick;

    //  Output inputs.
    [Range(0f, 1f)] public float throttleInput = 0f;
    [Range(-1f, 1f)] public float steerInput = 0f;
    [Range(0f, 1f)] public float brakeInput = 0f;
    [Range(0f, 1f)] public float ebrakeInput = 0f;
    [Range(0f, 1f)] public float nosInput = 0f;

    private void Update() {

        //  If mobile controller is enabled, set canvas true. Otherwise false.
        if (!RCCPSettings.mobileControllerEnabled) {

            if (mobileCanvas.activeSelf)
                mobileCanvas.SetActive(false);

            return;

        }

        if (!mobileCanvas.activeSelf)
            mobileCanvas.SetActive(true);

        if (RCCP_SceneManager.Instance && RCCP_SceneManager.Instance.activePlayerVehicle) {

            if (RCCP_SceneManager.Instance.activePlayerVehicle.OtherAddonsManager && RCCP_SceneManager.Instance.activePlayerVehicle.OtherAddonsManager.Nos) {

                if (nos)
                    nos.gameObject.SetActive(true);

            } else {

                if (nos)
                    nos.gameObject.SetActive(false);

            }

        }

        //  Mobile controller types.
        switch (RCCPSettings.mobileController) {

            //  If touch screen, enable and disable corresponding buttons.
            case RCCP_Settings.MobileController.TouchScreen:

                if (steeringWheel && steeringWheel.gameObject.activeSelf)
                    steeringWheel.gameObject.SetActive(false);

                if (joystick && joystick.gameObject.activeSelf)
                    joystick.gameObject.SetActive(false);

                if (left && !left.gameObject.activeSelf)
                    left.gameObject.SetActive(true);

                if (right && !right.gameObject.activeSelf)
                    right.gameObject.SetActive(true);

                break;

            //  If gyro, enable and disable corresponding buttons.
            case RCCP_Settings.MobileController.Gyro:

                if (steeringWheel && steeringWheel.gameObject.activeSelf)
                    steeringWheel.gameObject.SetActive(false);

                if (joystick && joystick.gameObject.activeSelf)
                    joystick.gameObject.SetActive(false);

                if (left && left.gameObject.activeSelf)
                    left.gameObject.SetActive(false);

                if (right && right.gameObject.activeSelf)
                    right.gameObject.SetActive(false);

#if BCG_NEWINPUTSYSTEM

                if (UnityEngine.InputSystem.Accelerometer.current != null && UnityEngine.InputSystem.Accelerometer.current.device.enabled == false)
                    UnityEngine.InputSystem.InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);

#endif

                break;

            //  If steering wheel, enable and disable corresponding buttons.
            case RCCP_Settings.MobileController.SteeringWheel:

                if (steeringWheel && !steeringWheel.gameObject.activeSelf)
                    steeringWheel.gameObject.SetActive(true);

                if (joystick && joystick.gameObject.activeSelf)
                    joystick.gameObject.SetActive(false);

                if (left && left.gameObject.activeSelf)
                    left.gameObject.SetActive(false);

                if (right && right.gameObject.activeSelf)
                    right.gameObject.SetActive(false);

                break;

            //  If joystick, enable and disable corresponding buttons.
            case RCCP_Settings.MobileController.Joystick:

                if (steeringWheel && steeringWheel.gameObject.activeSelf)
                    steeringWheel.gameObject.SetActive(false);

                if (joystick && !joystick.gameObject.activeSelf)
                    joystick.gameObject.SetActive(true);

                if (left && left.gameObject.activeSelf)
                    left.gameObject.SetActive(false);

                if (right && right.gameObject.activeSelf)
                    right.gameObject.SetActive(false);

                break;

        }

        //  Inputs.
        if (throttle)
            throttleInput = throttle.input;

        if (left && right)
            steerInput = -left.input + right.input;

        if (steeringWheel)
            steerInput += steeringWheel.input;

        if (joystick)
            steerInput += joystick.inputHorizontal;

        if (brake)
            brakeInput = brake.input;

        if (ebrake)
            ebrakeInput = ebrake.input;

        if (nos)
            nosInput = nos.input;

        throttleInput += nosInput;   //  Increasing throttle input with he nos input. But clamping it to 0 - 1 below.

        if (RCCPSettings.mobileController == RCCP_Settings.MobileController.Gyro) {

#if BCG_NEWINPUTSYSTEM
            if (UnityEngine.InputSystem.Accelerometer.current != null)
                steerInput += UnityEngine.InputSystem.Accelerometer.current.acceleration.ReadValue().x * RCCPSettings.gyroSensitivity;
#else
            steerInput += Input.acceleration.x * RCCPSettings.gyroSensitivity;
#endif

        }

        throttleInput = Mathf.Clamp01(throttleInput);
        steerInput = Mathf.Clamp(steerInput, -1f, 1f);
        brakeInput = Mathf.Clamp01(brakeInput);
        ebrakeInput = Mathf.Clamp01(ebrakeInput);
        nosInput = Mathf.Clamp01(nosInput);

    }

}
