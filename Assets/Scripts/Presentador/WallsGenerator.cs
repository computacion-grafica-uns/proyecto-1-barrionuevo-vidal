using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WallsGenerator 
{
    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colores;
    private ModelMatrixCreator modelMatrix;
    
    public WallsGenerator(){}
    public GameObject CreateLeftWall()
    {
        GameObject paredIzquierda = new GameObject();
        AddMeshAndMaterial(paredIzquierda);
        paredIzquierda.name = "Pared_Izquierda";
        modelMatrix = new ModelMatrixCreator(new Vector3(0,0,0),new Vector3(0,Mathf.Deg2Rad * 90,Mathf.Deg2Rad * 90),new Vector3(1,3,8));
        paredIzquierda.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix",modelMatrix.GetMatrix4x4());
        return paredIzquierda;
    }

    private void AddMeshAndMaterial(GameObject obj)
    {
        obj.AddComponent<MeshFilter>();
        obj.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.AddComponent<MeshRenderer>();
        CreateModel();
        UpdateMesh(obj);
        CreateMaterial(obj);
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

    private void UpdateMesh(GameObject obj)
    {
        obj.GetComponent<MeshFilter>().mesh.vertices = vertices;
        obj.GetComponent<MeshFilter>().mesh.triangles = triangles;
        obj.GetComponent<MeshFilter>().mesh.colors = colores;
    }

    private void CreateMaterial(GameObject obj)
    {
        Material newMaterial = new Material(Shader.Find("SurfaceShader"));
        obj.GetComponent<MeshRenderer>().material = newMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
