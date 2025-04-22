using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectionMatrix 
{
    public static Matrix4x4 createProjectionMatrix(float fov,float aspectRatio,float nPlane,float fPlane)
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
}
