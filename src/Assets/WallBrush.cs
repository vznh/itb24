using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WallBrush : InteractableObject
{
    [Header("Wall Placement Settings")]
    [SerializeField] private GameObject wallPrefab; // The wall prefab to spawn
    [SerializeField] private LayerMask floorLayer; // Layer for detecting the floor
    [SerializeField] private float wallHeight = 3f; // Height of the wall

    private Vector3 startPoint; // The start point of the brush stroke
    private Vector3 endPoint; // The end point of the brush stroke
    private bool isDrawing = false; // Flag to track if the user is drawing

    // Update is called once per frame
    private void Update()
    {
        if (isSelected) // Check if the brush is currently grabbed
        {
            // Use your VR input method to start, update, and end drawing
            if (Input.GetButtonDown("Fire1")) // Customize for VR trigger input
            {
                StartDrawing();
            }
            if (Input.GetButton("Fire1") && isDrawing) // Customize for VR trigger input
            {
                UpdateDrawing();
            }
            if (Input.GetButtonUp("Fire1") && isDrawing) // Customize for VR trigger input
            {
                EndDrawing();
            }
        }
    }

    private void StartDrawing()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, floorLayer))
        {
            startPoint = hit.point;
            isDrawing = true;
        }
    }

    private void UpdateDrawing()
    {
        // Provide visual feedback here, if needed
    }

    private void EndDrawing()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, floorLayer))
        {
            endPoint = hit.point;
            isDrawing = false;
            SpawnWall();
        }
    }

    private void SpawnWall()
    {
        Vector3 direction = endPoint - startPoint;
        float length = direction.magnitude;
        Vector3 position = (startPoint + endPoint) / 2;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject wall = Instantiate(wallPrefab, position, rotation);
        wall.transform.localScale = new Vector3(0.1f, wallHeight, length);
    }
}
