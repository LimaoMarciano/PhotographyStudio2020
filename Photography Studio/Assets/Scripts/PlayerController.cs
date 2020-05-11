using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public PhotoCamera photoCamera;
    public Transform cameraContainer;
    public float moveSpeed = 10f;
    public float aimVerticalSpeed = 100f;
    public float aimHorizontalSpeed = 100f;
    public float aimMaxAngle = 89f;
    public float ainMinAngle = -89f;
    public float gravity = 10f;

    private CharacterController controller;
    private Vector2 moveInput = Vector2.zero;
    private Vector2 lookInput = Vector2.zero;
    private Vector3 velocity = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 rotation = new Vector3(0, lookInput.x * aimHorizontalSpeed * Time.deltaTime, 0);
        controller.transform.Rotate(rotation);

        float verticalRot = aimVerticalSpeed * lookInput.y * Time.deltaTime;
        Quaternion targetRotation = cameraContainer.localRotation * Quaternion.Euler(-verticalRot, 0, 0);
        targetRotation = ClampRotationAroundXAxis(targetRotation);

        cameraContainer.localRotation = targetRotation;

        velocity = (controller.transform.right * moveSpeed * moveInput.x) + (controller.transform.forward * moveSpeed * moveInput.y);
        controller.SimpleMove(velocity * Time.deltaTime);


    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
       lookInput = context.ReadValue<Vector2>();

    }

    public void OnAimCamera (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            photoCamera.ToggleCameraAim();
        }
    }

    public void OnShutter (InputAction.CallbackContext context)
    {
        if (context.started)
        {
            photoCamera.PressShutter();
        }
        else if (context.canceled)
        {
            photoCamera.ReleaseShutter();
        }
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, ainMinAngle, aimMaxAngle);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
