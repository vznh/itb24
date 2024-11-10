using UnityEngine;

public class LockedSafe : MonoBehaviour
{
    [SerializeField] private GameObject safeDoor; // Reference to the Safe Door GameObject
    [SerializeField] private AudioClip unlockSound; // Optional sound effect for unlocking
    private AudioSource audioSource;

    private void Awake()
    {
        // Set up the AudioSource if there's an unlock sound
        if (unlockSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = unlockSound;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a key
        InteractableObject key = other.GetComponent<InteractableObject>();
        if (key != null)
        {
            // Play the unlock sound if available
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Make the safe door disappear
            if (safeDoor != null)
            {
                safeDoor.SetActive(false);
            }

            // Destroy the key object
            Destroy(key.gameObject);

            Debug.Log("Safe unlocked and key destroyed!");
        }
    }
}
