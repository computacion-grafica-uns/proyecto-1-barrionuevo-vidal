using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ParserOBJ 
{
    private List<Vector3> vertices;
    private List<int> triangles;

    public void ParseOBJ(){}
    public OBJModel GetOBJ(string nameArchive)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        string path = "Assets/Modelos3D/" + nameArchive + ".obj";
        StreamReader reader = new StreamReader(path);
        string fileData = reader.ReadToEnd();
        reader.Close();
        return ReaderLines(fileData);
    }

    private OBJModel ReaderLines(string fileData)
    {
        string[] lines = fileData.Split('\n');
        
        foreach (string line in lines)
        {
            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        
            if (line.StartsWith("v "))
            {
                CheckVertices(parts);
            }
            else if (line.StartsWith("f "))
            {
                CheckFaces(parts);
            }
        }

        CenterModel();

        OBJModel objData = new OBJModel(vertices, triangles);
        return objData;
    }
    private void CheckVertices(string[] parts)
    {
        float x = float.Parse(parts[1],System.Globalization.CultureInfo.InvariantCulture);
        float y = float.Parse(parts[2],System.Globalization.CultureInfo.InvariantCulture);
        float z = float.Parse(parts[3],System.Globalization.CultureInfo.InvariantCulture);
        vertices.Add(new Vector3(x, y, z));
    }

    private void CheckFaces(string[] parts)
    {
        List<int> faceIndices = new List<int>();

        for (int i = 1; i < parts.Length; i++)
        {
            string[] vertexData = parts[i].Split('/');
            int vertexIndex = int.Parse(vertexData[0]) - 1;
            faceIndices.Add(vertexIndex);
        }

        if (faceIndices.Count == 3)
        {
            triangles.AddRange(faceIndices); // triángulo
        }
        else if (faceIndices.Count == 4)
        {
            // quad: se crean dos triángulos
            triangles.Add(faceIndices[0]);
            triangles.Add(faceIndices[1]);
            triangles.Add(faceIndices[2]);

            triangles.Add(faceIndices[0]);
            triangles.Add(faceIndices[2]);
            triangles.Add(faceIndices[3]);
        }
    }

    private void CenterModel()
    {
        Vector3 min = vertices[0];
        Vector3 max = vertices[0];

        foreach (Vector3 v in vertices)
        {
            min = Vector3.Min(min, v);
            max = Vector3.Max(max, v);
        }

        Vector3 center = new Vector3((min.x + max.x) / 2f, min.y, (min.z + max.z) / 2f);

        for (int i = 0; i < vertices.Count; i++)
        {
            vertices[i] -= center;
        }
    }
}