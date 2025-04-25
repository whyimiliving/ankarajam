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
/// UI delete customization button.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/UI/Modification/RCCP UI Delete Customization Button")]
public class RCCP_UI_DeleteCustomization : RCCP_UIComponent {

    public void OnClick() {

        //  Finding the player vehicle.
        RCCP_CarController playerVehicle = RCCP_SceneManager.Instance.activePlayerVehicle;

        //  If no player vehicle found, return.
        if (!playerVehicle)
            return;

        //  If player vehicle doesn't have the customizer component, return.
        if (!playerVehicle.Customizer)
            return;

        playerVehicle.Customizer.Delete();

    }

}
