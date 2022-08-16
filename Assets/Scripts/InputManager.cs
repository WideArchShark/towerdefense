using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Transform cameraGroundTransform;
    private Vector3 newGroundPosition;
    private Vector3 groundMoveDirection;
    public Camera mainCamera;

    public float minX, maxX;
    public float minZ, maxZ;

    public void Update()
    {
        newGroundPosition = cameraGroundTransform.position + groundMoveDirection * Time.deltaTime;
        float groundPositionX = Mathf.Clamp(newGroundPosition.x, minX, maxX);
        float groundPositionZ = Mathf.Clamp(newGroundPosition.z, minZ, maxZ);
        cameraGroundTransform.position = new Vector3(groundPositionX, 0f, groundPositionZ);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 directionVector = context.ReadValue<Vector2>();
        groundMoveDirection = new Vector3(directionVector.x, 0f, directionVector.y);
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        Vector2 scrollVector = context.ReadValue<Vector2>();
        float newFieldOfView = Mathf.Clamp(mainCamera.fieldOfView +  scrollVector.y, 30, 55);
        mainCamera.fieldOfView = newFieldOfView;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        // Debug.Log("Click: " + context);
    }

    public void Point(InputAction.CallbackContext context)
    {
        // Debug.Log("Click: " + context);
    }
}
