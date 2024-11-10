using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject menuUI; // Assign your Menu UI in the Inspector
    [SerializeField] private Transform leftControllerTransform; // Assign the Transform of your left controller

    public InputActionProperty palmUpCheckInput; // Input action to toggle menu display

    private bool isMenuVisible = false;

    private void Start()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(false); // Start with the menu hidden
        }
    }

    private void Update()
    {
        if (menuUI == null || leftControllerTransform == null) return;

        // Check if the palm is facing up (you can fine-tune this threshold)
        Vector3 controllerUp = leftControllerTransform.up;
        if (controllerUp.y > 0.7f) // Palm up if the 'up' vector has a significant y component
        {
            if (palmUpCheckInput.action.WasPressedThisFrame())
            {
                ToggleMenu();
            }
        }
    }

    private void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;
        menuUI.SetActive(isMenuVisible);

        if (isMenuVisible)
        {
            // Position the menu above the palm
            menuUI.transform.position = leftControllerTransform.position + leftControllerTransform.up * 0.2f; // Adjust height as needed
            menuUI.transform.rotation = Quaternion.LookRotation(leftControllerTransform.forward, Vector3.up); // Make the menu face forward
        }
    }
}
