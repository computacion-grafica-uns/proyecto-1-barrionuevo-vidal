using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorObjects : MonoBehaviour
{
    private List<GameObject> objects;
    public List<OBJData> objectsData;
    private ParserOBJ parserObj;
    //private Matrix4x4 matrizPadre;

    void Awake()
    {
        parserObj = new ParserOBJ();
        objects = new List<GameObject>();
        CreateObjects();
    }

    private void CreateObjects()
    {   
        // Se crea el primer objeto, ya que si toma un color solido no se diferencia entre los objetos, 
        // los colores de los vertices variarán un poco
        /*OBJData info =   objectsData[0];
        OBJModel objModel = parserObj.GetOBJ(info.name);
        CreateModel(objModel.GetVertices(), objModel.GetTriangles(), info,.2f);
        */
        // el resto de los objetos se crean con un color solido
        foreach (OBJData info in objectsData)
        {
            OBJModel objModel = parserObj.GetOBJ(info.name);
            CreateModel(objModel.GetVertices(), objModel.GetTriangles(), info);
        }
    }

    private void CreateModel(List<Vector3> vertices,List<int> triangles,OBJData info)
    {
        Color[] colors = PaintVertices(vertices.ToArray(),info.colorModel,.2f);
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors;
        GameObject obj = new GameObject(info.name);
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        
        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("SurfaceShader"));
    
        Vector3 rotRad = info.rotation * Mathf.Deg2Rad;
        ModelMatrixCreator modelMatrix = new ModelMatrixCreator(info.position, rotRad, info.scale);
        // if(matrizPadre == null)
        // {
        //     matrizPadre = modelMatrix.GetMatrix4x4();
        // } else {
        //     modelMatrix.SetMatrix4x4(modelMatrix.GetMatrix4x4() * matrizPadre);
        // }
        ProjectionMatrix projectionMatrix = new ProjectionMatrix();
        
        obj.GetComponent<Renderer>().material.SetMatrix("_ModelMatrix", modelMatrix.GetMatrix4x4());
        obj.GetComponent<Renderer>().material.SetMatrix("_ProjectionMatrix",GL.GetGPUProjectionMatrix(projectionMatrix.GetMatrix4x4(),true));
        
        objects.Add(obj);
    }

    private Color[] PaintVertices(Vector3[] vertices, Color colorModel,float deltaColor)
    {
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            float colorValue = Random.Range(-deltaColor, deltaColor);
            colors[i] = new Color(colorModel.r + colorValue, colorModel.g + colorValue, colorModel.b  + colorValue);
        }

        return colors;
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
