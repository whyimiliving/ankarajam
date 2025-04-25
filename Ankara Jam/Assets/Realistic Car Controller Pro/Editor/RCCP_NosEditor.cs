//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright � 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(RCCP_Nos))]
public class RCCP_NosEditor : Editor {

    RCCP_Nos prop;
    GUISkin skin;

    private void OnEnable() {

        skin = Resources.Load<GUISkin>("RCCP_Gui");

    }

    public override void OnInspectorGUI() {

        prop = (RCCP_Nos)target;
        serializedObject.Update();
        GUI.skin = skin;

        EditorGUILayout.HelpBox("NOS / Boost used to multiply engine torque for a limited time.", MessageType.Info, true);

        GUI.enabled = false;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("nosInUse"), new GUIContent("Nos In Use", "Currently nos in use now or not."));
        GUI.enabled = true;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("amount"), new GUIContent("Amount", "Current amount of NOS."));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("torqueMultiplier"), new GUIContent("Torque Multiplier", "Torque multiplier will be applied to the produced engine torque. Engine torque will be multiplied by this value while using nos."));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("regenerateTime"), new GUIContent("Regenerate Time", "Nos will be generated after this seconds."));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("regenerateRate"), new GUIContent("Regenerate Rate", "Nos will be restored with this rate. Will be restored on higher values."));

        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.EndVertical();

        if (!EditorUtility.IsPersistent(prop)) {

            EditorGUILayout.BeginVertical(GUI.skin.box);

            if (GUILayout.Button("Back"))
                Selection.activeGameObject = prop.GetComponentInParent<RCCP_OtherAddons>(true).gameObject;

            EditorGUILayout.EndVertical();

        }

        prop.transform.localPosition = Vector3.zero;
        prop.transform.localRotation = Quaternion.identity;

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

        serializedObject.ApplyModifiedProperties();

    }

}
