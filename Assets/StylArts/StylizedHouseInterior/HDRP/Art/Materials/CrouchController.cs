using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CrouchController : MonoBehaviour
{
    public float normalHeight = 1.8f;
    public float crouchHeight = 0.9f;
    public float crouchSpeed = 5f;
    public InputActionReference crouchAction;

    private CharacterController characterController;
    private bool isCrouching = false;
    private float targetHeight;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        targetHeight = normalHeight;
        crouchAction.action.performed += OnCrouch;
        crouchAction.action.Enable();
    }

    void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = !isCrouching;
        targetHeight = isCrouching ? crouchHeight : normalHeight;
    }

    void Update()
    {
        float currentHeight = characterController.height;
        characterController.height = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchSpeed);
        characterController.center = new Vector3(0, characterController.height / 2f, 0);
    }

    void OnDestroy()
    {
        crouchAction.action.performed -= OnCrouch;
    }
}