using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Remy : MonoBehaviour
{
   
    [SerializeField] private Animator animator;
    [SerializeField] private Transform handPosition;
    [SerializeField] private GameObject CinemachineCameraTarget;
    [SerializeField] private CinemachineVirtualCamera cinemachineAimCamera;
    [SerializeField] private LayerMask AimColliderLayerMask;
    [SerializeField] private GameObject _mainCamera;

    private float _rotationVelocity;
    private float _targetRotation = 0.0f;
    private bool rotateOnMove = true;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float sensitivity;
    private float normalSensitivity = 1f;
    private float aimSensitivity = 0.5f;
    private float TopClamp = 70.0f;
    private float BottomClamp = -30.0f;
    private float mousex;
    private float mousey;
    private float horizontal;
    private float vertical;
    private float RotationSmoothTime = 0.12f;

    private Vector3 mouseWorldPosition;

    public void Awake()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
    }

    public void Update()
    {

        float targetSpeed = 5f;
        if (horizontal == 0 && vertical == 0) targetSpeed = 0.0f;

        horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
        vertical = UnityEngine.Input.GetAxisRaw("Vertical");
        mousex = UnityEngine.Input.GetAxis("Mouse X");
        mousey = UnityEngine.Input.GetAxis("Mouse Y");

        var direction = new Vector3(horizontal, 0, vertical).normalized;

        animator.SetFloat("speed", targetSpeed);


        mouseWorldPosition = Vector3.zero;
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

        if (UnityEngine.Input.GetKey(KeyCode.Mouse1))
        {
            cinemachineAimCamera.gameObject.SetActive(true);
            sensitivity = aimSensitivity;
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            rotateOnMove = false;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

        }
        else
        {
            cinemachineAimCamera.gameObject.SetActive(false);
            sensitivity = normalSensitivity;
            rotateOnMove = true;
        }

        if (horizontal != 0 || vertical != 0)
        {
            _targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

            if (rotateOnMove)
            {
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        transform.position += targetDirection.normalized * (targetSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        if (mousex != 0 || mousey != 0)
        {

            _cinemachineTargetYaw += mousex * sensitivity;
            _cinemachineTargetPitch += mousey * sensitivity * (-1);
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);

    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

   
}
