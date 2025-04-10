using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsGenerator 
{
    private ModelCreator wallModel;
    
    public WallsGenerator(){}

    public GameObject CreateLeftWall() => CreateWall("Pared_Izquierda", new Vector3(0, 0, 0), new Vector3(0, Mathf.Deg2Rad*90, Mathf.Deg2Rad*90), new Vector3(5, 10, 10));
    public GameObject CreateRightWall() => CreateWall("Pared_Derecha", new Vector3(10, 0, 0), new Vector3(0, Mathf.Deg2Rad*90, Mathf.Deg2Rad*90), new Vector3(5, 10, 10));
    public GameObject CreateFrontWall() => CreateWall("Pared_Frontal", new Vector3(10, 0, 0), new Vector3(0, 0, Mathf.Deg2Rad*90), new Vector3(5, 10, 10));
    public GameObject CreateBackWall() => CreateWall("Pared_Trasera", new Vector3(10, 0, 10), new Vector3(0, 0, Mathf.Deg2Rad*90), new Vector3(5, 10, 10));
    public GameObject CreateRoof() => CreateWall("Techo", new Vector3(0, 5, 10), new Vector3(Mathf.Deg2Rad*90, 0, 0), new Vector3(10, 10, 10));
    public GameObject CreateFloor() => CreateWall("Piso", new Vector3(0, 0, 10), new Vector3(Mathf.Deg2Rad*90, 0, 0), new Vector3(10, 10, 10));

    private GameObject CreateWall(string name, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        GameObject obj = new GameObject(name);
        AddMeshAndMaterial(obj);

        ModelMatrixCreator modelMatrix = new ModelMatrixCreator(position, rotation, scale);

        obj.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", modelMatrix.GetMatrix4x4());

        return obj;
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
