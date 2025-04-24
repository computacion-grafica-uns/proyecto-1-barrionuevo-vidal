using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public enum CameraMode { FirstPerson, Orbital }
    public CameraMode mode;
    //[SerializeField] private ParedView objects;
    [SerializeField] private GeneratorObjects generatorObjects; // nuevo scripts para generar objetos en base al parser

    // Propiedades camara (no se usan acá)
    public float fov = 60f;
    public float nearClip = 0.1f;
    public float farClip = 100f;
    public float aspect = 19/6f;
    
    // Primera Persona
    public float speed, sensitivity, hightFirstPerson;
    private float xRotation, yRotation;
    private Vector3 position, forwardCamera, up, target;

    // Orbital
    public Vector3 centerPoint;
    public float ratio, velocity,hightCamera;
    private float angle;

    void Start()
    {
        forwardCamera = Vector3.forward;
        up = Vector3.up;
        //position = transform.position;
        CreateCamera();
        if(mode == CameraMode.Orbital)
            ApplyRotationOrbital(0); // Empieza en la orbital
        else
            position = new Vector3(position.x,hightFirstPerson, position.z);
    }

    void Update()
    {
        CheckControls();

        switch (mode)
        {
            case CameraMode.FirstPerson:
                CheckControlsFirstPerson();
                ApplyRotationFirstPerson();
                break;
            case CameraMode.Orbital:
                CheckControlsOrbital();
                break;
        }
    }

    private void CheckControls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(mode == CameraMode.FirstPerson)
            {
                mode = CameraMode.Orbital;
                ApplyRotationOrbital(0); // Reinicia la rotación orbital al cambiar de modo
            }
            else if (mode == CameraMode.Orbital)
            {
                mode = CameraMode.FirstPerson;
                
                // Recalcular rotaciones a partir del forward actual
                position = new Vector3(position.x,hightFirstPerson, position.z);
                Vector3 dir = (target - position).normalized;
                Quaternion rot = Quaternion.LookRotation(dir,up);
                Vector3 euler = rot.eulerAngles;

                xRotation = euler.x;
                yRotation = euler.y;
            }
        }
    }

    private void CheckControlsFirstPerson()
    {
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
        Vector3 forward = rotation * Vector3.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = rotation * Vector3.right;
        right.y = 0;
        right.Normalize();

        if (Input.GetKey(KeyCode.W))
            ApplyTransform(forward);

        if (Input.GetKey(KeyCode.S))
            ApplyTransform(-forward);

        if (Input.GetKey(KeyCode.A))
            ApplyTransform(right * -1);

        if (Input.GetKey(KeyCode.D))
            ApplyTransform(right);
    }

    private void ApplyTransform(Vector3 deltaPosition)
    {
        position += deltaPosition * Time.deltaTime * speed;
        target = position + forwardCamera;
        UpdateView();
    }

    private void ApplyRotationFirstPerson()
    {
        float mouseY = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseX;
        yRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        forwardCamera = rotation * Vector3.forward;
        up = rotation * Vector3.up;
        target = position + forwardCamera;

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
        position = new Vector3(x, hightCamera, z);
        target = centerPoint;
        up = Vector3.up;

        UpdateView();
    }

    private void UpdateView()
    {
        //objects.UpdateViewMatrix(position, target, up);

        // descomentar si se quiere visualizar los objetos parseados .obj
        generatorObjects.UpdateViewMatrix(position, target, up); 
    }

    private void CreateCamera()
    {
        GameObject miCamara = new GameObject();
        miCamara.name = "Camara";
        miCamara.AddComponent<Camera>();

        miCamara.transform.position = new Vector3(0,0,-0.5f);
        miCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        miCamara.GetComponent<Camera>().backgroundColor = Color.black;
    }
}
