using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Remy_New : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject CinemachineCameraTarget;
    [SerializeField] private LayerMask AimColliderLayerMask;
    [SerializeField] private GameObject _mainCamera;

    private float targetRotation = 0.0f;
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
    private float sensitivity = 1.3f;
    private float TopClamp = 70.0f;
    private float BottomClamp = -30.0f;
    private float mousex;
    private float mousey;
    private float horizontal;
    private float vertical;

    private Vector3 mouseWorldPosition;

    public void Start()
    {
        cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
    }

    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        mousex = Input.GetAxis("Mouse X");
        mousey = Input.GetAxis("Mouse Y");

        float targetSpeed = (horizontal == 0 && vertical == 0) ? 0.0f : 5f;
        animator.SetFloat("speed", targetSpeed);

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        UpdateMouseWorldPosition();

        if (horizontal != 0 || vertical != 0)
        {
            MoveCharacter(targetSpeed, direction);
        }

        
    }

    private void LateUpdate()
    {
        CameraRotation();
        RotateTowardsMouseWorldPosition();
    }


    private void UpdateMouseWorldPosition()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 200f, AimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }
        else
        {
            mouseWorldPosition = Camera.main.transform.position + Camera.main.transform.forward * 200f;
        }
    }

    private void MoveCharacter(float targetSpeed, Vector3 direction)
    {
        targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        transform.position += targetDirection.normalized * (targetSpeed * Time.deltaTime);
    }

    private void RotateTowardsMouseWorldPosition()
    {
        Vector3 worldTarget = mouseWorldPosition;
        worldTarget.y = transform.position.y;
        Vector3 rotateDirection = (worldTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, rotateDirection, Time.deltaTime * 20f);
    }

    private void CameraRotation()
    {
        if (mousex != 0 || mousey != 0)
        {
            cinemachineTargetYaw += mousex * sensitivity;
            cinemachineTargetPitch += mousey * sensitivity * (-1);
        }

        cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, BottomClamp, TopClamp);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch, cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
