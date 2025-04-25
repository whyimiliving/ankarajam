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

/// <summary>
/// Tracks the player vehicle and keeps orientation nicely for cinematic angles. It has a pivot gameobject named "Animation Pivot". This pivot gameobject has 3 animations itself. 
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Camera/RCCP Cinematic Camera")]
public class RCCP_CinematicCamera : RCCP_Singleton<RCCP_CinematicCamera> {

    /// <summary>
    /// // Animation Pivot.
    /// </summary>
    public GameObject pivot;

    /// <summary>
    /// Target position for tracking.
    /// </summary>
    private Vector3 targetPosition;

    /// <summary>
    /// Target field of view.
    /// </summary>
    [Min(0f)] public float targetFOV = 60f;

    private void Start() {

        // If pivot is not selected in Inspector Panel, create it.
        if (!pivot) {

            pivot = new GameObject("Pivot");
            pivot.transform.SetParent(transform, false);
            pivot.transform.localPosition = Vector3.zero;
            pivot.transform.localRotation = Quaternion.identity;

        }

    }

    private void Update() {

        // If current camera is null, return.
        if (!RCCP_SceneManager.Instance.activePlayerCamera)
            return;

        // If current camera is null, return.
        if (RCCP_SceneManager.Instance.activePlayerCamera.cameraTarget.playerVehicle == null)
            return;

        Transform target = RCCP_SceneManager.Instance.activePlayerCamera.cameraTarget.playerVehicle.transform;

        if (target == null)
            return;

        // Rotates smoothly towards to vehicle.
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, target.eulerAngles.y + 180f, transform.eulerAngles.z), Time.deltaTime * 3f);

        // Calculating target position.
        targetPosition = target.position;
        targetPosition -= transform.rotation * Vector3.forward * 10f;

        // Assigning transform.position to targetPosition.
        transform.position = targetPosition;

    }

}
