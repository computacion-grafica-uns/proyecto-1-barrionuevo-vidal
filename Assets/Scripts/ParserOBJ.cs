using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ParserOBJ : MonoBehaviour
{
    [SerializeField] private string nameArchive;
    private List<Vector3> vertices;
    private List<int> triangles;
    void Start()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        string path = "Assets/Modelos3D/" + nameArchive + ".obj";
        StreamReader reader = new StreamReader(path);
        string fileData = reader.ReadToEnd();
        reader.Close();
        readerLines(fileData);
        createModel();
    }

    private void readerLines(string fileData)
    {
        string[] lines = fileData.Split('\n');

        foreach (string line in lines)
        {
            if (line.StartsWith("v "))
            {
                string[] parts = line.Split(' ');
                float x = float.Parse(parts[1],System.Globalization.CultureInfo.InvariantCulture);
                float y = float.Parse(parts[2],System.Globalization.CultureInfo.InvariantCulture);
                float z = float.Parse(parts[3],System.Globalization.CultureInfo.InvariantCulture);
                vertices.Add(new Vector3(x, y, z));
            }
            else if (line.StartsWith("f "))
            {
                string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // deja solo un espacio entre caracteres
                for (int i = 1; i < parts.Length; i++)
                {
                    string[] vertexData = parts[i].Split('/');
                    int vertexIndex = int.Parse(vertexData[0]) - 1;
                    triangles.Add(vertexIndex);
                }
            }
        }
    }
    private void createModel()
    {
        // Centralizar modelo
        Vector3 min = vertices[0];
        Vector3 max = vertices[0];

        foreach (Vector3 v in vertices)
        {
            min = Vector3.Min(min, v);
            max = Vector3.Max(max, v);
        }

        Vector3 center = (min + max) / 2f;

        for (int i = 0; i < vertices.Count; i++)
        {
            vertices[i] -= center;
        }

        Color[] colors = new Color[vertices.Count];
        
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = new Color(Random.Range(0,1f),Random.Range(0,1f), Random.Range(0,1f));
        }

        // crea la malla
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors;
        GameObject obj = new GameObject(nameArchive);
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        
        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("SurfaceShader"));
    }
}