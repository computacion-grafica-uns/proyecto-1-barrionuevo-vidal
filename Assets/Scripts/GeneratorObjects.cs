using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorObjects : MonoBehaviour
{
    public List<string> nameObjects; 
    
    // acá ponemos una list<Vector3> de pos por cada OBJ??
    private List<GameObject> objects;
    private ParserOBJ parserObj;

    void Start()
    {
        parserObj = new ParserOBJ();
        objects = new List<GameObject>();
        CreateObjects();
    }

    private void CreateObjects()
    {
        foreach (string name in nameObjects)
        {
            OBJData objData = parserObj.GetOBJ(name);
            CreateModel(objData.GetVertices(), objData.GetTriangles(), name);
        }
    }

    private void CreateModel(List<Vector3> vertices,List<int> triangles,string nameArchive)
    {
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
    
        //TODO: donde guardamos info de la pos de cada objeto??
        ModelMatrixCreator modelMatrix = new ModelMatrixCreator(Vector3.zero, Vector3.zero, Vector3.one);
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
