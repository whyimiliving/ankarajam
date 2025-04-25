﻿//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// A simple example manager for how the car selection scene works. 
/// </summary>
public class RCCP_CarSelectionExample : RCCP_GenericComponent {

    /// <summary>
    /// Our spawned vehicle list. No need to instantiate same vehicles over and over again. 
    /// </summary>
    private List<RCCP_CarController> spawnedVehicles = new List<RCCP_CarController>();

    /// <summary>
    /// Spawn transform.
    /// </summary>
    public Transform spawnPosition;

    /// <summary>
    /// Selected vehicle index. Next and previous buttons are affecting this value.
    /// </summary>
    [Min(0)] public int selectedIndex = 0;

    /// <summary>
    /// Enabling / disabling camera selection script on RCCP Camera if choosen.
    /// </summary>
    public RCCP_Camera RCCCamera;

    /// <summary>
    /// Name of the target scene when we select the vehicle.
    /// </summary>
    public string nextScene;

    private void Start() {

        //	Getting RCCP Camera.
        if (!RCCCamera)
            RCCCamera = RCCP_SceneManager.Instance.activePlayerCamera;

        selectedIndex = PlayerPrefs.GetInt("SelectedRCCPVehicle", 0);

        // First, we are instantiating all vehicles and store them in _spawnedVehicles list.
        CreateVehicles();

    }

    /// <summary>
    /// Creating all vehicles at once.
    /// </summary>
    private void CreateVehicles() {

        for (int i = 0; i < RCCP_DemoVehicles.Instance.vehicles.Length; i++) {

            // Spawning the vehicle with no controllable, no player, and engine off. We don't want to let player control the vehicle while in selection menu.
            RCCP_CarController spawnedVehicle = RCCP.SpawnRCC(RCCP_DemoVehicles.Instance.vehicles[i], spawnPosition.position, spawnPosition.rotation, false, false, false);

            // Disabling spawned vehicle. 
            spawnedVehicle.gameObject.SetActive(false);

            // Adding and storing it in spawnedVehicles list.
            spawnedVehicles.Add(spawnedVehicle);

        }

        SpawnVehicle();

        // If RCCP Camera is choosen, it wil enable RCCP_CameraCarSelection script. This script was used for orbiting camera.
        if (RCCCamera) {

            if (RCCCamera.GetComponent<RCCP_CameraCarSelection>())
                RCCCamera.GetComponent<RCCP_CameraCarSelection>().enabled = true;

        }

    }

    /// <summary>
    /// Increasing selected index, disabling all other vehicles, enabling current selected vehicle.
    /// </summary>
    public void NextVehicle() {

        selectedIndex++;

        // If index exceeds maximum, return to 0.
        if (selectedIndex > spawnedVehicles.Count - 1)
            selectedIndex = 0;

        SpawnVehicle();

    }

    /// <summary>
    /// Decreasing selected index, disabling all other vehicles, enabling current selected vehicle.
    /// </summary>
    public void PreviousVehicle() {

        selectedIndex--;

        // If index is below 0, return to maximum.
        if (selectedIndex < 0)
            selectedIndex = spawnedVehicles.Count - 1;

        SpawnVehicle();

    }

    /// <summary>
    /// Spawns the current selected vehicle.
    /// </summary>
    public void SpawnVehicle() {

        // Disabling all vehicles.
        for (int i = 0; i < spawnedVehicles.Count; i++)
            spawnedVehicles[i].gameObject.SetActive(false);

        // And enabling only selected vehicle.
        spawnedVehicles[selectedIndex].gameObject.SetActive(true);

    }

    /// <summary>
    /// Registering the spawned vehicle as player vehicle, enabling controllable.
    /// </summary>
    public void SelectVehicle() {

        if (RCCP_SceneManager.Instance && RCCP_SceneManager.Instance.activePlayerCamera)
            RCCP_SceneManager.Instance.activePlayerCamera.ChangeCamera(RCCP_Camera.CameraMode.TPS);

        // Registers the vehicle as player vehicle.
        RCCP.RegisterPlayerVehicle(spawnedVehicles[selectedIndex], true, true);

        // Starts engine and enabling controllable when selected.
        spawnedVehicles[selectedIndex].StartEngine();
        spawnedVehicles[selectedIndex].SetCanControl(true);

        // Save the selected vehicle for instantianting it on next scene.
        PlayerPrefs.SetInt("SelectedRCCPVehicle", selectedIndex);

        // If RCCP Camera is choosen, it will disable RCCP_CameraCarSelection script. This script was used for orbiting camera.
        if (RCCCamera) {

            if (RCCCamera.GetComponent<RCCP_CameraCarSelection>())
                RCCCamera.GetComponent<RCCP_CameraCarSelection>().enabled = false;

        }

        //  If next scene name is entered, load the next scene.
        if (nextScene != "")
            OpenScene();

    }

    /// <summary>
    /// Deactivates selected vehicle and returns to the car selection.
    /// </summary>
    public void DeSelectVehicle() {

        if (RCCP_SceneManager.Instance && RCCP_SceneManager.Instance.activePlayerCamera) {

            RCCP_SceneManager.Instance.activePlayerCamera.ChangeCamera(RCCP_Camera.CameraMode.TPS);
            RCCP_SceneManager.Instance.activePlayerCamera.ResetCamera();

        }

        // De-registers the vehicle.
        RCCP.DeRegisterPlayerVehicle();

        // Resets position and rotation.
        spawnedVehicles[selectedIndex].transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);

        // Kills engine and disables controllable.
        spawnedVehicles[selectedIndex].KillEngine();
        spawnedVehicles[selectedIndex].SetCanControl(false);

        // Resets the velocity of the vehicle.
        spawnedVehicles[selectedIndex].GetComponent<Rigidbody>().ResetInertiaTensor();
        spawnedVehicles[selectedIndex].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        spawnedVehicles[selectedIndex].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        // If RCCP Camera is choosen, it wil enable RCCP_CameraCarSelection script. This script was used for orbiting camera.
        if (RCCCamera) {

            if (RCCCamera.GetComponent<RCCP_CameraCarSelection>())
                RCCCamera.GetComponent<RCCP_CameraCarSelection>().enabled = true;

        }

    }

    /// <summary>
    /// Loads the target scene.
    /// </summary>
    public void OpenScene() {

        //	Loads next scene.
        SceneManager.LoadScene(nextScene);

    }

}
