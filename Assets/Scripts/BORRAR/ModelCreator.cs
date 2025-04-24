using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCreator
{
    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colores;

    public ModelCreator()
    {
        CreateModel();
    }
    private void CreateModel()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0), //0
            new Vector3(0,1,0), //1
            new Vector3(1,0,0), //2
            new Vector3(1,1,0), //3
            
        };

        triangles = new int[]
        {
            0,1,2,
            3,2,1,
        };

        colores = new Color[]
        {
            new Color(1,0,0),
            new Color(0,0,1),
            new Color(0,1,0),
            new Color(1,1,1),
        };
    }
    public Vector3[] GetVertices()
    {
        return vertices;
    }
    public int[] GetTriangles()
    {
        return triangles;
    }
    public Color[] GetColores()
    {
        return colores;
    }
}
