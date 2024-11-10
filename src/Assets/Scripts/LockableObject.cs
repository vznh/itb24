using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LockableObject : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable interactable; // Reference to XRGrabInteractable
    private Rigidbody rigidBody;
    private bool isLocked = false; // Lock state

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            Debug.LogError("Rigidbody is missing from the object. Please add a Rigidbody component.");
            return;
        }

        // Make sure the object has gravity enabled and is not kinematic by default
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
    }

    // Function called when the lock button is clicked
    public void ToggleLock()
    {
        isLocked = !isLocked; // Toggle the lock state

        if (interactable != null)
        {
            interactable.enabled = !isLocked; // Enable or disable the XRGrabInteractable component
        }

        if (rigidBody != null)
        {
            rigidBody.isKinematic = isLocked; // Make the Rigidbody kinematic if locked
            rigidBody.useGravity = !isLocked; // Disable gravity if locked
        }
    }
}
