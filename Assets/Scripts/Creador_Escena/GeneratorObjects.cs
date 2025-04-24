using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorObjects : MonoBehaviour
{
    //public List<string> nameObjects; 
    
    private List<GameObject> objects;
    public List<OBJData> objectsData;
    private ParserOBJ parserObj;

    void Start()
    {
        parserObj = new ParserOBJ();
        objects = new List<GameObject>();
        objectsData = new List<OBJData>();
        CreateObjects();
    }

    private void CreateObjects()
    {
        foreach (OBJData info in objectsData)
        {
            OBJModel objData = parserObj.GetOBJ(info.name);
            CreateModel(objData.GetVertices(), objData.GetTriangles(), info);
        }
    }

    private void CreateModel(List<Vector3> vertices,List<int> triangles,OBJData info)
    {
        Color[] colors = new Color[vertices.Count];
        
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = new Color(Random.Range(0,1f),Random.Range(0,1f), Random.Range(0,1f));
        }

        // Crea la malla
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors;
        GameObject obj = new GameObject(info.name);
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        
        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("SurfaceShader"));
    
        //USAR CLASE CON LA INFO DE LOS OBJ: donde guardamos info de la pos, rot, escala y nombre de cada objeto??
        Vector3 rotRad = info.rotation * Mathf.Deg2Rad;
        ModelMatrixCreator modelMatrix = new ModelMatrixCreator(info.position, rotRad, info.scale);
        ProjectionMatrix projectionMatrix = new ProjectionMatrix();
        
        obj.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", modelMatrix.GetMatrix4x4());
        obj.GetComponent<Renderer>().material.SetMatrix("_ProjectionMatrix",GL.GetGPUProjectionMatrix(projectionMatrix.GetMatrix4x4(),true));
        
        objects.Add(obj);
    }

    public void UpdateViewMatrix(Vector3 posCamera,Vector3 target,Vector3 up)
    {
        ViewMatrixCreator modelviewMatrix = new ViewMatrixCreator(posCamera, target, up);
        
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Renderer>().material.SetMatrix("_ViewMatrix",modelviewMatrix.GetMatrix4x4());
        }
    }
}
