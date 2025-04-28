using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMatrixCreator
{

    Matrix4x4 matrix;
    public ModelMatrixCreator(Vector3 newPosition, Vector3 newRotation, Vector3 newScale)
    {
        matrix = CreateModelMatrix(newPosition,newRotation,newScale);
    }

    public Matrix4x4 GetMatrix4x4()
    {
        return matrix;
    }

    // public void SetMatrix4x4(Matrix4x4 matrix){
    //     this.matrix = matrix;
    // }

    private Matrix4x4 CreateModelMatrix(Vector3 newPosition, Vector3 newRotation, Vector3 newScale)
    {
        Matrix4x4 positionMatrix = new Matrix4x4(
            new Vector4(1f, 0f, 0f, newPosition.x),   // Primera columna
            new Vector4(0f, 1f, 0f, newPosition.y),   // Segunda columna
            new Vector4(0f, 0f, 1f, newPosition.z),   // Tercera columna
            new Vector4(0f, 0f, 0f, 1f)               // Cuarta columna
        );
        positionMatrix = positionMatrix.transpose;

        Matrix4x4 rotationMatrixX = new Matrix4x4(
            new Vector4(1f, 0f, 0f, 0f),                                       // Primera columna
            new Vector4(0f, Mathf.Cos(newRotation.x), Mathf.Sin(newRotation.x), 0f), // Segunda columna
            new Vector4(0f, -Mathf.Sin(newRotation.x), Mathf.Cos(newRotation.x), 0f), // Tercera columna
            new Vector4(0f, 0f, 0f, 1f)                                        // Cuarta columna
        );

        Matrix4x4 rotationMatrixY = new Matrix4x4(
            new Vector4(Mathf.Cos(newRotation.y), 0f, Mathf.Sin(newRotation.y), 0f), // Primera columna
            new Vector4(0f, 1f, 0f, 0f),                                              // Segunda columna
            new Vector4(-Mathf.Sin(newRotation.y), 0f, Mathf.Cos(newRotation.y), 0f), // Tercera columna
            new Vector4(0f, 0f, 0f, 1f)                                               // Cuarta columna
        );

        Matrix4x4 rotationMatrixZ = new Matrix4x4(
            new Vector4(Mathf.Cos(newRotation.z), -Mathf.Sin(newRotation.z), 0f, 0f), // Primera columna
            new Vector4(Mathf.Sin(newRotation.z), Mathf.Cos(newRotation.z), 0f, 0f),  // Segunda columna
            new Vector4(0f, 0f, 1f, 0f),                                              // Tercera columna
            new Vector4(0f, 0f, 0f, 1f)                                               // Cuarta columna
        );

        Matrix4x4 rotationMatrix = rotationMatrixZ * rotationMatrixY * rotationMatrixX;
        rotationMatrix = rotationMatrix.transpose;

        Matrix4x4 scaleMatrix = new Matrix4x4(
            new Vector4(newScale.x, 0f, 0f, 0f),   // Primera columna
            new Vector4(0f, newScale.y, 0f, 0f),   // Segunda columna
            new Vector4(0f, 0f, newScale.z, 0f),   // Tercera columna
            new Vector4(0f, 0f, 0f, 1f)            // Cuarta columna
        );
        scaleMatrix = scaleMatrix.transpose;

        Matrix4x4 finalMatrix = positionMatrix;
        finalMatrix *= rotationMatrix;
        finalMatrix *= scaleMatrix;
        return (finalMatrix);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
