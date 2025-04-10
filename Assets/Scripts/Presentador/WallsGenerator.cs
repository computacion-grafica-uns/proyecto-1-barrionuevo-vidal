using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WallsGenerator 
{
    private ModelMatrixCreator modelMatrix;
    private ModelCreator wallModel;
    
    public WallsGenerator(){}
    public GameObject CreateLeftWall()
    {
        GameObject paredIzquierda = new GameObject();
        AddMeshAndMaterial(paredIzquierda);
        paredIzquierda.name = "Pared_Izquierda";
        modelMatrix = new ModelMatrixCreator(new Vector3(0,0,0),new Vector3(0,Mathf.Deg2Rad * 90,Mathf.Deg2Rad * 90),new Vector3(3,8,8));
        paredIzquierda.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix",modelMatrix.GetMatrix4x4());
        return paredIzquierda;
    }

    public GameObject CreateRightWall()
    {
        GameObject paredDerecha = new GameObject();
        AddMeshAndMaterial(paredDerecha);
        paredDerecha.name = "Pared_Derecha";
        modelMatrix = new ModelMatrixCreator(new Vector3(8,0,0),new Vector3(0,Mathf.Deg2Rad * 90,Mathf.Deg2Rad * 90),new Vector3(3,8,8));
        paredDerecha.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix",modelMatrix.GetMatrix4x4());
        return paredDerecha;
    }

    public GameObject CreateFrontWall()
    {
        GameObject paredFrontal = new GameObject();
        AddMeshAndMaterial(paredFrontal);
        paredFrontal.name = "Pared_Frontal";
        modelMatrix = new ModelMatrixCreator(new Vector3(8,0,0),new Vector3(0,0,Mathf.Deg2Rad * 90),new Vector3(3,8,8));
        paredFrontal.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix",modelMatrix.GetMatrix4x4());
        return paredFrontal;
    }

    public GameObject CreateBackWall()
    {
        GameObject paredTrasera = new GameObject();
        AddMeshAndMaterial(paredTrasera);
        paredTrasera.name = "Pared_Trasera";
        modelMatrix = new ModelMatrixCreator(new Vector3(8,0,8),new Vector3(0,0,Mathf.Deg2Rad * 90),new Vector3(3,8,8));
        paredTrasera.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix",modelMatrix.GetMatrix4x4());
        return paredTrasera;
    }

    private void AddMeshAndMaterial(GameObject obj)
    {
        obj.AddComponent<MeshFilter>();
        obj.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.AddComponent<MeshRenderer>();
        wallModel = new ModelCreator();
        UpdateMesh(obj,wallModel);
        CreateMaterial(obj);
    }

    private void UpdateMesh(GameObject obj, ModelCreator model)
    {
        obj.GetComponent<MeshFilter>().mesh.vertices = model.GetVertices();
        obj.GetComponent<MeshFilter>().mesh.triangles = model.GetTriangles();
        obj.GetComponent<MeshFilter>().mesh.colors = model.GetColores();
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
