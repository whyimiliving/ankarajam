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
/// Upgradable paint.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Customization/RCCP Vehicle Upgrade Paint")]
public class RCCP_VehicleUpgrade_Paint : RCCP_Component {

    /// <summary>
    /// Target material for painting.
    /// </summary>
    public Material paintMaterial;

    /// <summary>
    /// Target keyword for painting. Use "_BaseColor" for URP shaders.
    /// </summary>
    public string id = "_DiffuseColor";

    /// <summary>
    /// Instanced materials.
    /// </summary>
    private List<Material> instanceMaterials = new List<Material>();

    /// <summary>
    /// Paint the material with target color.
    /// </summary>
    /// <param name="newColor"></param>
    public void UpdatePaint(Color newColor) {

        //  Return if paint material is null.
        if (!paintMaterial) {

            Debug.LogError("Body material is not selected for this painter, disabling this painter!");
            enabled = false;
            return;

        }

        if (instanceMaterials == null || (instanceMaterials != null && instanceMaterials.Count == 0))
            instanceMaterials = new List<Material>();

        //  Getting all mesh renderers and instance of materials.
        MeshRenderer[] meshRenderers = CarController.transform.GetComponentsInChildren<MeshRenderer>(true);

        foreach (MeshRenderer item in meshRenderers) {

            for (int i = 0; i < item.sharedMaterials.Length; i++) {

                if (item.sharedMaterials[i] != null && Equals(item.sharedMaterials[i], paintMaterial))
                    instanceMaterials.Add(item.materials[i]);

            }

        }

        //  Painting all instances.
        for (int i = 0; i < instanceMaterials.Count; i++) {

            if (instanceMaterials[i] != null)
                instanceMaterials[i].SetColor(id, newColor);

        }

    }

}
