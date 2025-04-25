﻿//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(RCCP_GroundMaterials))]
public class RCC_GroundMaterialsEditor : Editor {

    RCCP_GroundMaterials prop;

    Vector2 scrollPos;
    List<RCCP_GroundMaterials.GroundMaterialFrictions> groundMaterials = new List<RCCP_GroundMaterials.GroundMaterialFrictions>();

    Color orgColor;

    public override void OnInspectorGUI() {

        prop = (RCCP_GroundMaterials)target;
        serializedObject.Update();
        orgColor = GUI.color;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Wheels Editor", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("This editor will keep update necessary .asset files in your project. Don't change directory of the ''Resources/RCCP Assets''.", EditorStyles.helpBox);
        EditorGUILayout.Space();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        EditorGUIUtility.labelWidth = 110f;

        GUILayout.Label("Ground Materials", EditorStyles.boldLabel);

        if (prop.frictions != null) {

            for (int i = 0; i < prop.frictions.Length; i++) {

                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();

                if (prop.frictions[i].groundMaterial)
                    EditorGUILayout.LabelField(prop.frictions[i].groundMaterial.name + (i == 0 ? " (Default)" : ""), EditorStyles.boldLabel);

                GUI.color = Color.red; if (GUILayout.Button("X", GUILayout.Width(25f))) { RemoveGroundMaterial(i); }
                GUI.color = orgColor;

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();

                prop.frictions[i].groundMaterial = (PhysicsMaterial)EditorGUILayout.ObjectField("Physic Material", prop.frictions[i].groundMaterial, typeof(PhysicsMaterial), false, GUILayout.Width(250f));
                prop.frictions[i].forwardStiffness = EditorGUILayout.FloatField("Forward Stiffness", prop.frictions[i].forwardStiffness, GUILayout.Width(150f));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                prop.frictions[i].groundSound = (AudioClip)EditorGUILayout.ObjectField("Wheel Sound", prop.frictions[i].groundSound, typeof(AudioClip), false, GUILayout.Width(250f));
                prop.frictions[i].sidewaysStiffness = EditorGUILayout.FloatField("Sideways Stiffness", prop.frictions[i].sidewaysStiffness, GUILayout.Width(150f));

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();

                prop.frictions[i].volume = EditorGUILayout.Slider("Volume", prop.frictions[i].volume, 0f, 1f, GUILayout.Width(250f));

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                prop.frictions[i].groundParticles = (GameObject)EditorGUILayout.ObjectField("Wheel Particles", prop.frictions[i].groundParticles, typeof(GameObject), false, GUILayout.Width(200f));
                prop.frictions[i].skidmark = (RCCP_Skidmarks)EditorGUILayout.ObjectField("Wheel Skidmarks", prop.frictions[i].skidmark, typeof(RCCP_Skidmarks), false, GUILayout.Width(200f));

                EditorGUILayout.Space();

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                prop.frictions[i].slip = EditorGUILayout.FloatField("Slip", prop.frictions[i].slip, GUILayout.Width(150f));
                prop.frictions[i].damp = EditorGUILayout.FloatField("Damp", prop.frictions[i].damp, GUILayout.Width(150f));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();

            }

        } else {

            prop.frictions = new RCCP_GroundMaterials.GroundMaterialFrictions[1];
            prop.frictions[0] = new RCCP_GroundMaterials.GroundMaterialFrictions();

        }

        GUILayout.Label("Terrain Ground Materials", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("terrainFrictions"), new GUIContent("Terrain Physic Material"), true);

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        GUI.color = Color.cyan;

        if (GUILayout.Button("Create New Ground Material"))
            AddNewMaterial();

        if (GUILayout.Button("--< Return To Asset Settings"))
            OpenGeneralSettings();

        GUI.color = orgColor;

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Created by Ekrem Bugra Ozdoganlar\nBoneCracker Games", EditorStyles.centeredGreyMiniLabel, GUILayout.MaxHeight(50f));

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

        serializedObject.ApplyModifiedProperties();

    }

    private void AddNewMaterial() {

        groundMaterials.Clear();
        groundMaterials.AddRange(prop.frictions);
        RCCP_GroundMaterials.GroundMaterialFrictions newGroundMaterial = new RCCP_GroundMaterials.GroundMaterialFrictions();
        groundMaterials.Add(newGroundMaterial);
        prop.frictions = groundMaterials.ToArray();

        EditorUtility.SetDirty(RCCP_GroundMaterials.Instance);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        serializedObject.ApplyModifiedProperties();

    }

    private void RemoveGroundMaterial(int index) {

        groundMaterials.Clear();
        groundMaterials.AddRange(prop.frictions);
        groundMaterials.RemoveAt(index);
        prop.frictions = groundMaterials.ToArray();

        EditorUtility.SetDirty(RCCP_GroundMaterials.Instance);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        serializedObject.ApplyModifiedProperties();

    }

    private void OpenGeneralSettings() {

        Selection.activeObject = RCCP_Settings.Instance;

    }

}
