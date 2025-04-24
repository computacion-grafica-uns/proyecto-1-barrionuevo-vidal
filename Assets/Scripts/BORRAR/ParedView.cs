using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedView : MonoBehaviour
{
    private ObjectsGenerator generator;
    private GameObject[] paredes;
    
    void Awake()
    {
        generator = new ObjectsGenerator();

        paredes = new GameObject[]
        {
            // generator.CreateLeftWall_BigSquare(),
            // generator.CreateLeftWall_SmallScuare(),
            // generator.CreateRightWall(),
            // generator.CreateFrontWall_Square1(),
            // generator.CreateFrontWall_Square2(),
            // generator.CreateFrontWall_Square3(),
            // generator.CreateBackWall_Square1(),
            // generator.CreateBackWall_Square2(),
            // generator.CreateBackWall_Square3(),
            // generator.CreateBackWall_Square4(),
            // generator.CreateRoof_BigSquare(),
            // generator.CreateRoof_SmallScuare(),
            // generator.CreateFloor_BigSquare(),
            // generator.CreateFloor_SmallScuare(),
            // generator.CreateBathroomDoor_Scuare1(),
            // generator.CreateBathroomDoor_Scuare2(),
            // generator.CreateBathroomDoor_Scuare3(),
            // generator.CreateBathroomWall1(),
            // generator.CreateBathroomWall2(),
        };        
    }

    public void UpdateViewMatrix(Vector3 posCamera,Vector3 target,Vector3 up)
    {
        ViewMatrixCreator modelviewMatrix = new ViewMatrixCreator(posCamera, target, up);
        
        foreach (GameObject obj in paredes)
        {
            obj.GetComponent<Renderer>().material.SetMatrix("_ViewMatrix",modelviewMatrix.GetMatrix4x4());
        }
    }
}
