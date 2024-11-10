using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.UI;

public class UIMOVE : XRGrabInteractable
{
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
            lockButton.onClick.AddListener(SetPartialLock);
        }

        // Initialize in PartialLock mode
        SetPartialLock();
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

    private void SetPartialLock()
    {
        rigidBody.isKinematic = true;
        useGravityOnRelease = false;
        interactionLayers = InteractionLayerMask.GetMask("Default"); // Allow grabbing and hovering
    }
}
