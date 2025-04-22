using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] ParedView objects;
    public Vector3 centerPoint, hight;
    public float ratio, velocity; // mouseSensitivity;
    private float angle;

    void Start()
    {
        ApplyRotation(0); // para que empiece en la orbita

    }
    void Update()
    {
        OrbitalControl(); // Controla la rotacion de la camara orbital
    }
    private void OrbitalControl()
    {
        if(Input.GetKey(KeyCode.A)) 
            ApplyRotation(-1);
        else if(Input.GetKey(KeyCode.D))
            ApplyRotation(1);
    }

    private void ApplyRotation(float dir) // Rotacion de la camara orbital
    {
        angle += Time.deltaTime * velocity * dir;

        float x = ratio * Mathf.Cos(angle) + centerPoint.x;
        float z = ratio * Mathf.Sin(angle) + centerPoint.z;

        Vector3 pos = new Vector3(x, hight.y, z); 
        Vector3 target = centerPoint;
        Vector3 up = new Vector3(0, 1, 0);

        objects.UpdateViewMatrix(pos, target, up); // Actualiza la matriz de vista de los objetos
    }
}
