using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionMatrix 
{
    // Propiedades camara
    public float fov = 60f;
    public float nPlane = 0.1f;
    public float fPlane = 100f;
    public float aspectRatio = 16/9f;
    private Matrix4x4 matrix;
    
    public ProjectionMatrix()
    {
        matrix = CreateProjectionMatrix(fov,aspectRatio,nPlane,fPlane);
    }
    
    public Matrix4x4 CreateProjectionMatrix(float fov,float aspectRatio,float nPlane,float fPlane)
    {
        float fovInRadians = fov * Mathf.Deg2Rad;
        
        Matrix4x4 matrix = new Matrix4x4(
            new Vector4(1/(aspectRatio * Mathf.Tan(fovInRadians/2)), 0, 0, 0),
            new Vector4(0, 1/Mathf.Tan(fovInRadians/2), 0, 0),
            new Vector4(0, 0,(fPlane + nPlane)/(nPlane - fPlane),(2*fPlane*nPlane)/(nPlane - fPlane)),
            new Vector4(0, 0,-1, 0)
        );
        
        matrix = matrix.transpose;
        return matrix;
    }

    public Matrix4x4 GetMatrix4x4()
    {
        return matrix;
    }
}
