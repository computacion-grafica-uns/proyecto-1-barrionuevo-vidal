using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJModel 
{
    private List<Vector3> vertices;
    private List<int> triangles;
    public OBJModel(List<Vector3>vertices,List<int> triangles)
    {
        this.vertices = vertices;
        this.triangles = triangles;
    }

    public List<Vector3> GetVertices() => vertices;
    
    public List<int> GetTriangles() => triangles;
}