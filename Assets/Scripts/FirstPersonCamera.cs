using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float speed,sensitivity;
    [SerializeField] private Vector3 position,target,forward, up;
    [SerializeField] private GameObject cameraPrefab;
    [SerializeField] private ParedView objects;
    private float xRotation,yRotation;
    
    void Start()
    {
        forward = new Vector3(0,0,1);
        xRotation = -90f;
        yRotation = 0f;
    }
    void Update()
    {
        CheckControls();
        ApplyRotation();
    }

    private void CheckControls()
    {
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        if (Input.GetKey(KeyCode.W))
            ApplyTransform(rotation * Vector3.forward);
        
        if (Input.GetKey(KeyCode.S))
            ApplyTransform(rotation * Vector3.back);
        
        if (Input.GetKey(KeyCode.A))
            ApplyTransform(rotation * Vector3.left);
        
        if (Input.GetKey(KeyCode.D))
            ApplyTransform(rotation * Vector3.right);
    }

    private void ApplyTransform(Vector3 deltaPosition)
    {
        position  += deltaPosition * Time.deltaTime * speed;
        target = position + forward;
        objects.UpdateViewMatrix(position, target, up);
    }

    private void ApplyRotation()
    { 
        float mouseY = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseX;
        yRotation += mouseY;
       // xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        forward = rotation * Vector3.forward; // nuevo forward en base a rotación
        target = position + forward;
        up = rotation * Vector3.up;

        objects.UpdateViewMatrix(position, target, up);
    }
}
