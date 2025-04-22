using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public enum CameraMode { FirstPerson, Orbital }
    public CameraMode mode = CameraMode.Orbital;

    [SerializeField] private ParedView objects;

    // primera persona
    public Vector3 position, forward, up, target;
    public float speed, sensitivity, xRotation, yRotation;

    // orbital
    public Vector3 centerPoint, hight;
    public float ratio, velocity;
    private float angle;

    void Start()
    {
        forward = Vector3.forward;
        up = Vector3.up;
        position = transform.position;
        CreateCamera();
        ApplyRotationOrbital(0); // empieza en la orbital
    }

    void Update()
    {
        checkControls();

        switch (mode)
        {
            case CameraMode.FirstPerson:
                CheckControlsFPS();
                ApplyRotationFPS();
                break;
            case CameraMode.Orbital:
                CheckControlsOrbital();
                break;
        }
    }

    private void checkControls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(mode == CameraMode.FirstPerson)
            {
                mode = CameraMode.Orbital;
                ApplyRotationOrbital(0); // reinicia la rotación orbital al cambiar de modo
            }
            else if (mode == CameraMode.Orbital)
            {
                mode = CameraMode.FirstPerson;
                
                 // Recalcular rotaciones a partir del forward actual
                Vector3 dir = (target - position).normalized;
                Quaternion rot = Quaternion.LookRotation(dir,up);
                Vector3 euler = rot.eulerAngles;

                xRotation = euler.x;
                yRotation = euler.y;
            }
        }
    }

    private void CheckControlsFPS()
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
        position += deltaPosition * Time.deltaTime * speed;
        target = position + forward;
        UpdateView();
    }

    private void ApplyRotationFPS()
    {
        float mouseY = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseX;
        yRotation += mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        forward = rotation * Vector3.forward;
        up = rotation * Vector3.up;
        target = position + forward;

        UpdateView();
    }

    private void CheckControlsOrbital()
    {
        if (Input.GetKey(KeyCode.A))
            ApplyRotationOrbital(-1);
        else if (Input.GetKey(KeyCode.D))
            ApplyRotationOrbital(1);
    }

    private void ApplyRotationOrbital(float dir)
    {
        angle += Time.deltaTime * velocity * dir;
        float x = ratio * Mathf.Cos(angle) + centerPoint.x;
        float z = ratio * Mathf.Sin(angle) + centerPoint.z;
        position = new Vector3(x, hight.y, z);
        target = centerPoint;
        up = Vector3.up;

        UpdateView();
    }

    private void UpdateView()
    {
        objects.UpdateViewMatrix(position, target, up);
    }

    private void CreateCamera()
    {
        GameObject miCamara = new GameObject();
        miCamara.name = "Camara";
        miCamara.AddComponent<Camera>();

        miCamara.transform.position = new Vector3(0,0,-0.5f);
        //miCamara.transform.rotation = Quaternion.Euler(0,-90,0);

        miCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        miCamara.GetComponent<Camera>().backgroundColor = Color.black;
    }
}
