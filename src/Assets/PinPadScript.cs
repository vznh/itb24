using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro

public class PinPadDoor : MonoBehaviour
{
    [Header("Pin Pad Settings")]
    [SerializeField] private GameObject door; // The door that will open
    [SerializeField] private GameObject pinPadUI; // The UI for the pin pad
    [SerializeField] private TextMeshProUGUI codeDisplay; // TextMeshPro element to show the entered code
    [SerializeField] private int codeLength = 4; // Length of the code

    private string currentCode = ""; // The code being entered
    private string savedCode = ""; // The code that is saved
    private bool codeSet = false; // Flag to check if the code has been set

    private void Start()
    {
        // Show the pin pad UI when the door spawns
        pinPadUI.SetActive(true);
        codeDisplay.text = ""; // Clear the code display
    }

    // Method to handle number button presses
    public void OnNumberButtonPress(string number)
    {
        if (currentCode.Length < codeLength)
        {
            currentCode += number;
            codeDisplay.text = currentCode;
        }

        // If the code has reached the required length
        if (currentCode.Length == codeLength)
        {
            if (!codeSet)
            {
                // Set the code for the first time
                savedCode = currentCode;
                codeSet = true;
                Debug.Log("Code set successfully: " + savedCode);
                ClearCode(); // Clear the code display for the next input
            }
            else
            {
                // Check if the entered code matches the saved code
                if (currentCode == savedCode)
                {
                    UnlockDoor();
                }
                else
                {
                    Debug.Log("Incorrect code. Try again.");
                    ClearCode();
                }
            }
        }
    }

    // Method to unlock the door
    private void UnlockDoor()
    {
        Debug.Log("Door unlocked!");
        Destroy(gameObject); // Destroy the entire PinPad GameObject, including Cube and Canvas
        door.SetActive(false); // Hide the door to simulate it opening
        pinPadUI.SetActive(false); // Hide the pin pad UI
    }

    // Method to clear the current code
    private void ClearCode()
    {
        currentCode = "";
        codeDisplay.text = currentCode;
    }
}

