﻿//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright © 2014 - 2024 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RCCP_VehicleUpgrade_NeonManager))]
public class RCCP_VehicleUpgrade_NeonEditor : Editor {

    RCCP_VehicleUpgrade_NeonManager prop;

    public override void OnInspectorGUI() {

        prop = (RCCP_VehicleUpgrade_NeonManager)target;
        serializedObject.Update();

        EditorGUILayout.HelpBox("All neons can be used under this manager. Each neon has target material. Decals and neons are working with URP / HDRP only. In order to use decals and neons, 'Decals' rendering feature must be added and enabled in the URP / HDRP asset. Edit --> Project Settings --> Graphics --> Select your URP asset, and add 'Decals' rendering feature.", MessageType.None);

#if !BCG_URP
        EditorGUILayout.HelpBox("Decals and neons are working with URP / HDRP only!", MessageType.Error);
#endif

        DrawDefaultInspector();

        if (GUI.changed)
            EditorUtility.SetDirty(prop);

        serializedObject.ApplyModifiedProperties();

    }

}
