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
using System.Threading.Tasks;
using System.Threading;

/// <summary>
/// Skidmarks Manager for RCCP.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller Pro/Misc/RCCP Skidmarks")]
public class RCCP_Skidmarks : RCCP_GenericComponent {

    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    private bool asyncDrawing = false;

    /// <summary>
    /// Mesh filter.
    /// </summary>
    private MeshFilter meshFilter;

    /// <summary>
    /// Mesh.
    /// </summary>
    private Mesh mesh;

    /// <summary>
    /// Maximum number of marks total handled by one instance of the script.
    /// </summary>
    [Min(128)] public int maxMarks = 1024;

    /// <summary>
    /// The distance the skidmarks is places above the surface it is placed upon. In meters.
    /// </summary>
    [Min(0f)] public float groundOffset = 0.02f;

    /// <summary>
    /// The minimum distance between two marks places next to each other. 
    /// </summary>
    [Min(0f)] public float minDistance = 0.1f;

    private int numMarks = 0;

    /// <summary>
    /// Variables for each mark created. Needed to generate the correct mesh.
    /// </summary>
    public class MarkSection {

        public Vector3 pos = Vector3.zero;
        public Vector3 normal = Vector3.zero;
        public Vector4 tangent = Vector4.zero;
        public Vector3 posl = Vector3.zero;
        public Vector3 posr = Vector3.zero;
        public float intensity = 0.0f;
        public int lastIndex = 0;

    }

    public MarkSection[] skidmarks;

    private bool updated = false;

    /// <summary>
    /// Initiallizes the array holding the skidmark sections.
    /// </summary>
    private void Awake() {

        transform.position = Vector3.zero;

        skidmarks = new MarkSection[maxMarks];

        for (int i = 0; i < maxMarks; i++)
            skidmarks[i] = new MarkSection();

        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;

        updated = false;
        asyncDrawing = false;

    }

    private void OnEnable() {

        asyncDrawing = false;
        updated = false;

    }

    // Function called by the wheels that is skidding. Gathers all the information needed to
    // create the mesh later. Sets the intensity of the skidmark section b setting the alpha
    // of the vertex color.
    public int AddSkidMark(Vector3 pos, Vector3 normal, float intensity, float width, int lastIndex) {

        if (intensity > 1f)
            intensity = 1f;
        if (intensity < 0f)
            return -1;

        if (pos == Vector3.zero)
            return -1;
        if (normal == Vector3.zero)
            return -1;

        if (lastIndex > 0) {

            float sqrDistance = (pos - skidmarks[lastIndex % maxMarks].pos).sqrMagnitude;

            if (sqrDistance < minDistance)
                return lastIndex;

        }

        MarkSection curr = skidmarks[numMarks % maxMarks];
        curr.pos = pos + normal * groundOffset;
        curr.normal = normal;
        curr.intensity = intensity;
        curr.lastIndex = lastIndex;

        if (lastIndex != -1) {

            MarkSection last = skidmarks[lastIndex % maxMarks];
            Vector3 dir = (curr.pos - last.pos);
            Vector3 xDir = Vector3.Cross(dir, normal).normalized;

            curr.posl = curr.pos + xDir * width * 0.5f;
            curr.posr = curr.pos - xDir * width * 0.5f;
            curr.tangent = new Vector4(xDir.x, xDir.y, xDir.z, 1);

            if (last.lastIndex == -1) {

                last.tangent = curr.tangent;
                last.posl = curr.pos + xDir * width * 0.5f;
                last.posr = curr.pos - xDir * width * 0.5f;

            }

        }

        numMarks++;
        updated = true;

        return numMarks - 1;

    }

    // If the mesh needs to be updated, i.e. a new section has been added,
    // the current mesh is removed, and a new mesh for the skidmarks is generated.
    private void LateUpdate() {

        if (!updated)
            return;

        updated = false;

        if (RCCP_SceneManager.Instance.mutlithreadingSupported) {

            if (!asyncDrawing)
                Draw();

        } else {

            DrawRaw();

        }

    }

    public async void Draw() {

        asyncDrawing = true;

        Vector3[] vertices = new Vector3[0];
        Vector3[] normals = new Vector3[0];
        Vector4[] tangents = new Vector4[0];
        Color[] colors = new Color[0];
        Vector2[] uvs = new Vector2[0];
        int[] triangles = new int[0];

        if (cancellationTokenSource == null)
            cancellationTokenSource = new CancellationTokenSource();

        await Task.Run(() => {

            int segmentCount = 0;

            for (int j = 0; j < numMarks && j < maxMarks; j++) {

                if (skidmarks[j].lastIndex != -1 && skidmarks[j].lastIndex > numMarks - maxMarks)
                    segmentCount++;

            }

            vertices = new Vector3[segmentCount * 4];
            normals = new Vector3[segmentCount * 4];
            tangents = new Vector4[segmentCount * 4];
            colors = new Color[segmentCount * 4];
            uvs = new Vector2[segmentCount * 4];

            triangles = new int[segmentCount * 6];
            segmentCount = 0;

            for (int i = 0; i < numMarks && i < maxMarks; i++) {

                if (skidmarks[i].lastIndex != -1 && skidmarks[i].lastIndex > numMarks - maxMarks) {

                    MarkSection curr = skidmarks[i];
                    MarkSection last = skidmarks[curr.lastIndex % maxMarks];

                    if (last.pos != Vector3.zero && last.normal != Vector3.zero && Vector3.Distance(curr.pos, last.pos) < 10f) {

                        vertices[segmentCount * 4 + 0] = last.posl;
                        vertices[segmentCount * 4 + 1] = last.posr;
                        vertices[segmentCount * 4 + 2] = curr.posl;
                        vertices[segmentCount * 4 + 3] = curr.posr;

                        normals[segmentCount * 4 + 0] = last.normal;
                        normals[segmentCount * 4 + 1] = last.normal;
                        normals[segmentCount * 4 + 2] = curr.normal;
                        normals[segmentCount * 4 + 3] = curr.normal;

                        tangents[segmentCount * 4 + 0] = last.tangent;
                        tangents[segmentCount * 4 + 1] = last.tangent;
                        tangents[segmentCount * 4 + 2] = curr.tangent;
                        tangents[segmentCount * 4 + 3] = curr.tangent;

                        colors[segmentCount * 4 + 0] = new Color(0, 0, 0, last.intensity);
                        colors[segmentCount * 4 + 1] = new Color(0, 0, 0, last.intensity);
                        colors[segmentCount * 4 + 2] = new Color(0, 0, 0, curr.intensity);
                        colors[segmentCount * 4 + 3] = new Color(0, 0, 0, curr.intensity);

                        uvs[segmentCount * 4 + 0] = new Vector2(0, 0);
                        uvs[segmentCount * 4 + 1] = new Vector2(1, 0);
                        uvs[segmentCount * 4 + 2] = new Vector2(0, 1);
                        uvs[segmentCount * 4 + 3] = new Vector2(1, 1);

                        triangles[segmentCount * 6 + 0] = segmentCount * 4 + 0;
                        triangles[segmentCount * 6 + 2] = segmentCount * 4 + 1;
                        triangles[segmentCount * 6 + 1] = segmentCount * 4 + 2;

                        triangles[segmentCount * 6 + 3] = segmentCount * 4 + 2;
                        triangles[segmentCount * 6 + 5] = segmentCount * 4 + 1;
                        triangles[segmentCount * 6 + 4] = segmentCount * 4 + 3;

                    }

                    segmentCount++;

                }

            }

        }, cancellationTokenSource.Token);

        if (cancellationTokenSource.IsCancellationRequested)
            return;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.tangents = tangents;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.uv = uvs;

        asyncDrawing = false;

    }

    public void DrawRaw() {

        mesh.Clear();

        int segmentCount = 0;

        for (int j = 0; j < numMarks && j < maxMarks; j++) {

            if (skidmarks[j].lastIndex != -1 && skidmarks[j].lastIndex > numMarks - maxMarks)
                segmentCount++;

        }

        Vector3[] vertices = new Vector3[segmentCount * 4];
        Vector3[] normals = new Vector3[segmentCount * 4];
        Vector4[] tangents = new Vector4[segmentCount * 4];
        Color[] colors = new Color[segmentCount * 4];
        Vector2[] uvs = new Vector2[segmentCount * 4];

        int[] triangles = new int[segmentCount * 6];
        segmentCount = 0;

        for (int i = 0; i < numMarks && i < maxMarks; i++) {

            if (skidmarks[i].lastIndex != -1 && skidmarks[i].lastIndex > numMarks - maxMarks) {

                MarkSection curr = skidmarks[i];
                MarkSection last = skidmarks[curr.lastIndex % maxMarks];

                if (last.pos != Vector3.zero && last.normal != Vector3.zero && Vector3.Distance(curr.pos, last.pos) < 10f) {

                    vertices[segmentCount * 4 + 0] = last.posl;
                    vertices[segmentCount * 4 + 1] = last.posr;
                    vertices[segmentCount * 4 + 2] = curr.posl;
                    vertices[segmentCount * 4 + 3] = curr.posr;

                    normals[segmentCount * 4 + 0] = last.normal;
                    normals[segmentCount * 4 + 1] = last.normal;
                    normals[segmentCount * 4 + 2] = curr.normal;
                    normals[segmentCount * 4 + 3] = curr.normal;

                    tangents[segmentCount * 4 + 0] = last.tangent;
                    tangents[segmentCount * 4 + 1] = last.tangent;
                    tangents[segmentCount * 4 + 2] = curr.tangent;
                    tangents[segmentCount * 4 + 3] = curr.tangent;

                    colors[segmentCount * 4 + 0] = new Color(0, 0, 0, last.intensity);
                    colors[segmentCount * 4 + 1] = new Color(0, 0, 0, last.intensity);
                    colors[segmentCount * 4 + 2] = new Color(0, 0, 0, curr.intensity);
                    colors[segmentCount * 4 + 3] = new Color(0, 0, 0, curr.intensity);

                    uvs[segmentCount * 4 + 0] = new Vector2(0, 0);
                    uvs[segmentCount * 4 + 1] = new Vector2(1, 0);
                    uvs[segmentCount * 4 + 2] = new Vector2(0, 1);
                    uvs[segmentCount * 4 + 3] = new Vector2(1, 1);

                    triangles[segmentCount * 6 + 0] = segmentCount * 4 + 0;
                    triangles[segmentCount * 6 + 2] = segmentCount * 4 + 1;
                    triangles[segmentCount * 6 + 1] = segmentCount * 4 + 2;

                    triangles[segmentCount * 6 + 3] = segmentCount * 4 + 2;
                    triangles[segmentCount * 6 + 5] = segmentCount * 4 + 1;
                    triangles[segmentCount * 6 + 4] = segmentCount * 4 + 3;

                }

                segmentCount++;

            }

        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.tangents = tangents;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.uv = uvs;

    }

    /// <summary>
    /// Clean all skidmarks.
    /// </summary>
    public void Clean() {

        numMarks = 0;
        updated = true;

    }

    public void OnDestroy() {

        if (cancellationTokenSource == null)
            cancellationTokenSource = new CancellationTokenSource();

        cancellationTokenSource.Cancel();

    }

    private void OnDisable() {

        asyncDrawing = false;
        updated = false;

    }

}