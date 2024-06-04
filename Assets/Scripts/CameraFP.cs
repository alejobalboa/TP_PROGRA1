using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFP : MonoBehaviour
{
    public Transform playerRef;
    [SerializeField] Transform TorsoRef;
    [SerializeField]public float mouseSensitivity = 5f;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); /*Sirve para que la cámara no pueda pasar de los 90 grados para arriba o para abajo*/
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerRef.Rotate(Vector3.up * mouseX);
        TorsoRef.Rotate(Vector3.up * mouseX);
    }
}
