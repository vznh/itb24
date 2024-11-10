using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorInteractable : XRGrabInteractable
{
    // Reference to the parent Door GameObject (the one that should move)
    [SerializeField] private GameObject door;

    // Variables to control the open and closed rotations
    [SerializeField] private Vector3 openRotation = new Vector3(0, 90, 0); // 90 degrees around the Y-axis
    [SerializeField] private Vector3 closedRotation = new Vector3(0, 0, 0); // Default closed rotation
    private bool isOpen = false;

    // Sound effect to play when the door opens or closes
    [SerializeField] private AudioClip doorSFX;
    private AudioSource audioSource;

    private Rigidbody doorRigidbody;

    protected override void Awake()
    {
        base.Awake();

        // Add an AudioSource to the DoorKnob GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Don't play the sound on awake

        // Ensure the Door has a Rigidbody component for physics interactions
        if (door != null)
        {
            doorRigidbody = door.GetComponent<Rigidbody>();
            if (doorRigidbody == null)
            {
                doorRigidbody = door.AddComponent<Rigidbody>();
                doorRigidbody.isKinematic = true; // Set the Rigidbody to kinematic by default
            }
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Toggle the door state
        isOpen = !isOpen;
        
        // Rotate the entire Door object to the open or closed position
        if (door != null)
        {
            door.transform.localEulerAngles = isOpen ? openRotation : closedRotation;
        }

        // Play the sound effect
        if (doorSFX != null)
        {
            audioSource.clip = doorSFX;
            audioSource.Play();
        }
    }
}
