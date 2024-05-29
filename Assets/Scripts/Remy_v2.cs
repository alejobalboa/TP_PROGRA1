using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remy_v2 : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject CinemachineCameraTarget;
    [SerializeField] private CinemachineVirtualCamera cinemachineAimCamera;
    [SerializeField] private LayerMask AimColliderLayerMask;
    [SerializeField] private GameObject _mainCamera;

    [SerializeField] private Weapon SelectedWeapon;

    private float targetRotation = 0.0f;
    private float cinemachineTargetX;
    private float cinemachineTargetY;
    private float sensitivity;
    private float normalSensitivity = 1f;
    private float aimSensitivity = 0.5f;
    private float TopClamp = 70.0f;
    private float BottomClamp = -30.0f;
    private float mousex;
    private float mousey;
    private float horizontal;
    private float vertical;

    private Vector3 mouseWorldPosition;

    public void Awake()
    {
        //Establece el angulo y de la camara con respecto al target, en este caso la cabeza del personaje, con respecto al mundo
        cinemachineTargetX = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
    }

    public void Update()
    {
        //Si no le cambio la speed, el personaje se sigue moviendo porque al final del código, se multiplica la dirección por la speed, por lo que siempre se va a estar moviendo,
        //no importa si estas apretando o no las flechitas. Ver mas abajo
        float targetSpeed = 5f;
        if (horizontal == 0 && vertical == 0) targetSpeed = 0.0f;

        horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
        vertical = UnityEngine.Input.GetAxisRaw("Vertical");
        mousex = UnityEngine.Input.GetAxis("Mouse X");
        mousey = UnityEngine.Input.GetAxis("Mouse Y");

        var direction = new Vector3(horizontal, 0, vertical).normalized;

        animator.SetFloat("speed", targetSpeed);

        //Establece la posición del mouse en el mundo en 0
        mouseWorldPosition = Vector3.zero;
        //Establece el punto central de la pantalla
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        //Rayo que ubica el centro de la pantalla hacia el mundo, se utiliza para saber si hay algo adelante que deba visualizar la camara
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        //Aca le da un máximo de visualización, es decir, si antes de los 200f no encuentra nada, solo visualizarpá hasta ahí, si encuentra un punto de colision antes, visualizara hasta ese punto
        //Esto por que? porque el personaje gira según la posición de la camara, y para saber a donde apunta la camara, se lanza el rayo, y si el rayo no choca contra nada, sigue buscando la posición
        //Entonces el personaje deja de moverse porque no tiene tope la camara, al ponerle 200f, va a buscar algo con que chocar, con un límite de 200.
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 200f, AimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }
        else
        {
            mouseWorldPosition = Camera.main.transform.position + Camera.main.transform.forward * 200f;
        }

        if (UnityEngine.Input.GetKey(KeyCode.Mouse1))
        {
            //Cuando presiono el click derecho, cambia de camara y sensibilidad (Para apuntar)
            cinemachineAimCamera.gameObject.SetActive(true);
            sensitivity = aimSensitivity;

        }
        else
        {
            //Si no lo tiene seleccionado, cambia de camara a CameraFollowPlayer (por la prioridad)
            cinemachineAimCamera.gameObject.SetActive(false);
            sensitivity = normalSensitivity;
        }

        if (horizontal != 0 || vertical != 0)
        {
            targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        //Por esto es que es necesario agregar la primera linea de código de esta funcion
        transform.position += targetDirection.normalized * (targetSpeed * Time.deltaTime);


    }

    private void LateUpdate()
    {
        CameraRotation();
        //Establece la posición del eje y del personaje igual a como apunta la camara, para poder dar vuelta con el mouse, no puede dar vuelta con las teclas
        Vector3 worldTarget = mouseWorldPosition;
        worldTarget.y = transform.position.y;
        Vector3 rotateDirection = (worldTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, rotateDirection, Time.deltaTime * 20f);
    }

    private void CameraRotation()
    {
        //Establece la rotación de la camara segun los inputs del mouse
        if (mousex != 0 || mousey != 0)
        {

            cinemachineTargetX += mousex * sensitivity;
            cinemachineTargetY += mousey * sensitivity * (-1);
        }

        cinemachineTargetX = ClampAngle(cinemachineTargetX, float.MinValue, float.MaxValue);
        cinemachineTargetY = ClampAngle(cinemachineTargetY, BottomClamp, TopClamp);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetY, cinemachineTargetX, 0.0f);

    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        //Esta funcion permite que tengan un máximo de angulo para girar la camara. Es decir, la camara en y va a girar 360, pero para mirar para arriba y para abajo tiene un límite para rotar.
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
