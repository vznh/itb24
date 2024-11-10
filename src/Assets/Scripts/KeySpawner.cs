using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform playerHeadTransform;
    public float spawnDistance = 1.5f;

    // spawn the key in front of the player
    public void SpawnKey()
    {
        if (keyPrefab != null && playerHeadTransform != null)
        {
            Vector3 spawnPosition = playerHeadTransform.position + playerHeadTransform.forward * spawnDistance;

            spawnPosition.y = playerHeadTransform.position.y;

            Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Key Prefab or Player Head Transform is missing!");
        }
    }
}
