using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject miCamara;
    public Vector3 centerPoint,hight;
    public float ratio,velocity, mouseSensitivity;
    private float angle;

    void Start()
    {
        CreateCamera();
        applyRotationCamera(0); // para que empiece en la orbita
    }

    void Update()
    {
        checkControls();
    }
    

    private void checkControls()
    {
        if(Input.GetKey(KeyCode.A))
        {
            applyRotationCamera(-1);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            applyRotationCamera(1);
        }
        else if(Input.GetButton("Fire2"))
        {
            float mouseX = Input.GetAxis("Mouse X");
            applyRotationCamera(mouseX * mouseSensitivity);
        }
    }

    private void applyRotationCamera(float dir)
    {
        angle += Time.deltaTime * velocity * dir;
        float x = ratio * Mathf.Cos(angle) + centerPoint.x;
        float z = ratio * Mathf.Sin(angle) + centerPoint.z;
        miCamara.transform.position = new Vector3(x,hight.y,z); 
        miCamara.transform.LookAt(centerPoint);
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
}
