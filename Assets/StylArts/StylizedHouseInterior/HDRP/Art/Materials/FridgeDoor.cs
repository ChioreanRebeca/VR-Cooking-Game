using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FridgeDoor : MonoBehaviour
{
    public float openAngle = 90f;
    public float speed = 3f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private XRSimpleInteractable simpleInteractable;

    void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;

        simpleInteractable = GetComponent<XRSimpleInteractable>();
        simpleInteractable.selectEntered.AddListener(ToggleDoor);
    }

    void ToggleDoor(SelectEnterEventArgs args)
    {
        isOpen = !isOpen;
    }

    void Update()
    {
        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            isOpen ? openRotation : closedRotation,
            Time.deltaTime * speed
        );
    }

    void OnDestroy()
    {
        simpleInteractable.selectEntered.RemoveListener(ToggleDoor);
    }
}