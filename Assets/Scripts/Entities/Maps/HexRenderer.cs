using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;

    private List<HexFace> m_faces;

    public Material material;
    public float innerSize = 0f;
    public float outerSize = 1f;
    public float height = 1f;
    public bool isFlatTopped;

    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_mesh = new Mesh();
        m_mesh.name = "Hex";

        m_meshFilter.mesh = m_mesh;
        m_meshRenderer.material = material;
    }

    private void OnEnable()
    {
        DrawMesh();
    }

    public void OnValidate()
    {
        if (Application.isPlaying)
        {
            DrawMesh();
        }
    }

    public void SetMeterial(Material material)
    {
        this.material = material;
        m_meshRenderer.material = material;
    }

    private void DrawMesh()
    {
        if (m_mesh == null) return;

        DrawFaces();
        CombineFaces();
    }

    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < m_faces.Count; i++)
        {
            vertices.AddRange(m_faces[i].vertices);
            uvs.AddRange(m_faces[i].uvs);

            int offset = 4 * i;
            foreach (int triangle in m_faces[i].triangles)
            {
                tris.Add(triangle);
            }
        }

        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = tris.ToArray();
        m_mesh.uv = uvs.ToArray();
        m_mesh.RecalculateNormals();
    }

    private void DrawFaces()
    {
        m_faces = new List<HexFace>();

        // Top faces
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
        }

        // Bottom faces
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point, true));
        }

        // Outer faces
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CreateFace(outerSize, outerSize, -height / 2f, -height / 2f, point, true));
        }

        // Inner faces
        for (int point = 0; point < 6; point++)
        {
            m_faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point, false));
        }
    }

    private HexFace CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRad, heightA, point);

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangels = new List<int>() { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

        if (reverse)
            vertices.Reverse();

        return new HexFace(vertices, triangels, uvs);
    }

    private Vector3 GetPoint(float size, float height, int index)
    {
        float angleDeg = isFlatTopped ? 60 * index : 60 * index - 30;
        float angleRad = Mathf.PI / 180 * angleDeg;
        return new Vector3(size * Mathf.Cos(angleRad), height, size * Mathf.Sin(angleRad));
    }
}
