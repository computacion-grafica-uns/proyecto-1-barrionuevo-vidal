using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ViewMatrix 
{
    public static Matrix4x4 createViewMatrix(Vector3 pos, Vector3 target, Vector3 up)
    {
        Vector3 F = Vector3.Normalize(target - pos); 
        Vector3 R = Vector3.Normalize(Vector3.Cross(up,F)); // Right
        Vector3 U = Vector3.Cross(F, R); // Recalculado para asegurar ortogonalidad

        Matrix4x4 matrix = new Matrix4x4(
            new Vector4(R.x, R.y, R.z, -Vector3.Dot(R, pos)),     
            new Vector4(U.x, U.y, U.z, -Vector3.Dot(U, pos)),      
            new Vector4(-F.x, -F.y, -F.z, Vector3.Dot(F, pos)),  
            new Vector4(0, 0, 0, 1)                               
        );
        
        matrix = matrix.transpose;
        return matrix;
    }
}
