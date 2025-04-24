using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGenerator 
{
    //private ModelCreator wallModel;
    private ProjectionMatrix projectionMatrix;
    
    public ObjectsGenerator(){}

    // Pared izquierda
    public GameObject CreateLeftWall_BigSquare() => CreatePlane("Pared_Izquierda1", new Vector3(0, 0, 15), new Vector3(0, 0, 0), new Vector3(15, 5, 1));
    public GameObject CreateLeftWall_SmallScuare() => CreatePlane("Pared_Izquierda2", new Vector3(15, 0, 8), new Vector3(0, 0, 0), new Vector3(5, 5, 1));
    
    // Pared derecha
    public GameObject CreateRightWall() => CreatePlane("Pared_Derecha", new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(20, 5, 1));
    
    // Pared frontal (Puerta)
    public GameObject CreateFrontWall_Square1() => CreatePlane("Pared_Frontal1", new Vector3(20, 0, 0), new Vector3(0, Mathf.Deg2Rad*-90, 0), new Vector3(4, 5, 1));
    public GameObject CreateFrontWall_Square2() => CreatePlane("Pared_Frontal2", new Vector3(20, 2, 4), new Vector3(0, Mathf.Deg2Rad*-90, 0), new Vector3(1, 3, 1));
    public GameObject CreateFrontWall_Square3() => CreatePlane("Pared_Frontal3", new Vector3(20, 0, 5), new Vector3(0, Mathf.Deg2Rad*-90, 0), new Vector3(3, 5, 1));
    
    // Pared trasera (Ventana)
    public GameObject CreateBackWall_Square1() => CreatePlane("Pared_Trasera1", new Vector3(0, 0, 0), new Vector3(0, Mathf.Deg2Rad*-90, 0), new Vector3(2, 4, 1));
    public GameObject CreateBackWall_Square2() => CreatePlane("Pared_Trasera2", new Vector3(0, 4, 0), new Vector3(0, Mathf.Deg2Rad*-90, 0), new Vector3(8, 1, 1));
    public GameObject CreateBackWall_Square3() => CreatePlane("Pared_Trasera3", new Vector3(0, 0, 2), new Vector3(0, Mathf.Deg2Rad*-90, 0), new Vector3(6, 1, 1));
    public GameObject CreateBackWall_Square4() => CreatePlane("Pared_Trasera4", new Vector3(0, 0, 8), new Vector3(0, Mathf.Deg2Rad*-90, 0), new Vector3(7, 5, 1));
    
    // Techo
    public GameObject CreateRoof_BigSquare() => CreatePlane("Techo1", new Vector3(0, 5, 0), new Vector3(Mathf.Deg2Rad*-90, 0, 0), new Vector3(15, 15, 1));
    public GameObject CreateRoof_SmallScuare() => CreatePlane("Techo2", new Vector3(15, 5, 0), new Vector3(Mathf.Deg2Rad*-90, 0, 0), new Vector3(5, 8, 1));
    
    // Piso
    public GameObject CreateFloor_BigSquare() => CreatePlane("Piso1", new Vector3(0, 0, 0), new Vector3(Mathf.Deg2Rad*-90, 0, 0), new Vector3(15, 15, 1));
    public GameObject CreateFloor_SmallScuare() => CreatePlane("Piso2", new Vector3(15, 0, 0), new Vector3(Mathf.Deg2Rad*-90, 0, 0), new Vector3(5, 8, 1));

    // Pared baño
    public GameObject CreateBathroomDoor_Scuare1() => CreatePlane("ParedBañoPuerta1", new Vector3(10, 0, 8), new Vector3(0, 0, 0), new Vector3(1, 5, 1));
    public GameObject CreateBathroomDoor_Scuare2() => CreatePlane("ParedBañoPuerta2", new Vector3(11, 2, 8), new Vector3(0, 0, 0), new Vector3(1, 3, 1));
    public GameObject CreateBathroomDoor_Scuare3() => CreatePlane("ParedBañoPuerta3", new Vector3(12, 0, 8), new Vector3(0, 0, 0), new Vector3(3, 5, 1));
    public GameObject CreateBathroomWall1() => CreatePlane("ParedBaño1", new Vector3(10, 0, 8), new Vector3(0, Mathf.Deg2Rad*-90, 0), new Vector3(7, 5, 1));
    public GameObject CreateBathroomWall2() => CreatePlane("ParedBaño2", new Vector3(15, 0, 8), new Vector3(0, Mathf.Deg2Rad*-90, 0), new Vector3(7, 5, 1));
    
    
    private GameObject CreatePlane(string name, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        GameObject obj = new GameObject(name);
        AddMeshAndMaterial(obj);

        ModelMatrixCreator modelMatrix = new ModelMatrixCreator(position, rotation, scale);
        ProjectionMatrix projectionMatrix = new ProjectionMatrix();
        

        obj.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", modelMatrix.GetMatrix4x4());
        //obj.GetComponent<Renderer>().material.SetMatrix("_ViewMatrix", viewMatrix);
        obj.GetComponent<Renderer>().material.SetMatrix("_ProjectionMatrix",GL.GetGPUProjectionMatrix(projectionMatrix.GetMatrix4x4(),true));
            
        return obj;
    }

    private void AddMeshAndMaterial(GameObject obj)
    {
        obj.AddComponent<MeshFilter>();
        obj.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.AddComponent<MeshRenderer>();
        ModelCreator wallModel = new ModelCreator();
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
}
