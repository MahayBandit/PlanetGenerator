using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRegionDivider : MonoBehaviour
{
    public int latitudeSegments = 10;
    public int longitudeSegments = 20;
    public Material[] regionMaterials; // Array of materials for each region

    void Start()
    {
        DivideSphere();
    }

    void DivideSphere()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter not found!");
            return;
        }

        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        int totalVertices = vertices.Length;
        int totalTriangles = triangles.Length / 3;

        Vector3[] normals = new Vector3[totalVertices];
        Vector2[] uv = new Vector2[totalVertices];

        for (int i = 0; i < totalTriangles; i++)
        {
            int triangleIndex = i * 3;
            Vector3 v1 = vertices[triangles[triangleIndex]];
            Vector3 v2 = vertices[triangles[triangleIndex + 1]];
            Vector3 v3 = vertices[triangles[triangleIndex + 2]];

            Vector3 normal = Vector3.Cross(v2 - v1, v3 - v1).normalized;

            for (int j = 0; j < 3; j++)
            {
                int vertexIndex = triangles[triangleIndex + j];
                normals[vertexIndex] += normal;
            }
        }

        for (int i = 0; i < totalVertices; i++)
        {
            normals[i] = normals[i].normalized;
            uv[i] = SphericalUVMapping(normals[i]);
        }

        mesh.normals = normals;
        mesh.uv = uv;

        int regionIndex = 0;

        for (int i = 0; i < totalTriangles; i++)
        {
            int triangleIndex = i * 3;
            Vector3 normal = normals[triangles[triangleIndex]];
            float latitude = Mathf.Asin(normal.y) * Mathf.Rad2Deg;

            float longitude = Mathf.Atan2(normal.z, normal.x) * Mathf.Rad2Deg;
            if (longitude < 0f)
                longitude += 360f;

            int region = GetRegion(latitude, longitude);
            AssignMaterialToTriangle(mesh, triangleIndex, region);
        }
    }

    Vector2 SphericalUVMapping(Vector3 normal)
    {
        float latitude = Mathf.Asin(normal.y) / Mathf.PI + 0.5f;
        float longitude = Mathf.Atan2(normal.z, normal.x) / (2 * Mathf.PI) + 0.5f;

        return new Vector2(longitude, latitude);
    }

    int GetRegion(float latitude, float longitude)
    {
        int latSegments = Mathf.FloorToInt(latitudeSegments * (latitude + 90f) / 180f);
        int lonSegments = Mathf.FloorToInt(longitudeSegments * longitude / 360f);

        return latSegments * longitudeSegments + lonSegments;
    }

    void AssignMaterialToTriangle(Mesh mesh, int triangleIndex, int region)
    {
        if (region < regionMaterials.Length)
        {
            int submeshIndex = mesh.subMeshCount > 1 ? 1 : 0;
            int[] triangles = new int[] { triangleIndex, triangleIndex + 1, triangleIndex + 2 };
            mesh.SetTriangles(triangles, submeshIndex);
        }
    }
}