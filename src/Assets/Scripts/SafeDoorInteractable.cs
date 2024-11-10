using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SafeDoorInteractable : XRGrabInteractable
{
    // Reference to the Safe Door GameObject that will disappear
    [SerializeField] private GameObject safeDoor;

    // Optional: Sound effect to play when the door disappears
    [SerializeField] private AudioClip doorSFX;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        // Add an AudioSource if needed for the sound effect
        if (doorSFX != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // Don't play the sound on awake
            audioSource.clip = doorSFX;
        }
    }

    // This method is called when the Safe Door is pinched (selected)
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // Make the Safe Door disappear
        if (safeDoor != null)
        {
            safeDoor.SetActive(false); // Hide the Safe Door
        }

        // Play the sound effect if available
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
