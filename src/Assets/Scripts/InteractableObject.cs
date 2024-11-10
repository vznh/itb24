using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InteractableObject : XRGrabInteractable
{
    // Variables for customization
    [SerializeField]
    private bool useGravityOnRelease = true;

    private Rigidbody rigidBody;

    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();

        if (rigidBody == null)
        {
            Debug.LogError("Rigidbody is missing from the object. Please add a Rigidbody component.");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        // Logic for when the key is grabbed
        if (rigidBody != null)
        {
            rigidBody.isKinematic = true; // Disable physics while interacting
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        // Logic for when the key is released
        if (rigidBody != null)
        {
            rigidBody.isKinematic = false; // Enable physics
            rigidBody.useGravity = useGravityOnRelease; // Use gravity if specified
        }
    }
}
