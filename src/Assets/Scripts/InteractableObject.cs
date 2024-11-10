using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;

public class InteractableObject : XRGrabInteractable
{
    public enum LockMode { Draggable, PartialLock }
    [SerializeField]
    private LockMode currentLockMode = LockMode.Draggable;

    [SerializeField]
    private bool useGravityOnRelease = true;

    [SerializeField]
    private Canvas lockCanvas;
    [SerializeField]
    private Button lockButton;

    private Rigidbody rigidBody;

    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();

        if (rigidBody == null)
        {
            Debug.LogError("Rigidbody is missing from the object. Please add a Rigidbody component.");
        }

        if (lockCanvas != null)
        {
            lockCanvas.gameObject.SetActive(false);
        }

        if (lockButton != null)
        {
            lockButton.onClick.AddListener(CycleLockMode);
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        if (lockCanvas != null)
        {
            lockCanvas.gameObject.SetActive(true);
        }
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        if (lockCanvas != null)
        {
            lockCanvas.gameObject.SetActive(false);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (currentLockMode == LockMode.Draggable)
        {
            rigidBody.isKinematic = true;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (currentLockMode == LockMode.Draggable)
        {
            rigidBody.isKinematic = false;
            rigidBody.useGravity = useGravityOnRelease;
        }
    }

    private void CycleLockMode()
    {
        currentLockMode = (LockMode)(((int)currentLockMode + 1) % 2);

        switch (currentLockMode)
        {
            case LockMode.Draggable:
                rigidBody.isKinematic = false;
                useGravityOnRelease = true;
                interactionLayers = InteractionLayerMask.GetMask("Default"); // Enable full interaction
                break;

            case LockMode.PartialLock:
                rigidBody.isKinematic = true;
                useGravityOnRelease = false;
                interactionLayers = InteractionLayerMask.GetMask("Default"); // Allow grabbing and hovering
                break;
        }
    }
}
