using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject miCamara;

    void Start()
    {
        CreateCamera();
    }

    private void CreateCamera()
    {
        miCamara = new GameObject();
        miCamara.name = "Camara";
        miCamara.AddComponent<Camera>();

        miCamara.transform.position = new Vector3(9,2,5);
        miCamara.transform.rotation = Quaternion.Euler(0,-90,0);

        miCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        miCamara.GetComponent<Camera>().backgroundColor = Color.black;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
