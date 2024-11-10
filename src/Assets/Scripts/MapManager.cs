using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class MapManager : MonoBehaviour
{
    public List<MapData> maps = new List<MapData>();
    private string savePath;

    [Header("Prefabs")]
    public GameObject keyPrefab;
    public GameObject doorPrefab;
    public GameObject puzzleBoxPrefab;

    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();

    void Start()
    {
        // Set the path where maps will be saved and loaded
        savePath = Path.Combine(Application.persistentDataPath, "maps.json");
        LoadMaps();
    }

    // Method to load maps from a JSON file
    public void LoadMaps()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            maps = JsonConvert.DeserializeObject<List<MapData>>(json);

            Debug.Log("Maps loaded successfully.");
        }
        else
        {
            Debug.LogWarning("No saved maps found.");
        }
    }

    // Method to save maps to a JSON file
    public void SaveMaps()
    {
        string json = JsonConvert.SerializeObject(maps, Formatting.Indented);
        File.WriteAllText(savePath, json);
        Debug.Log("Maps saved successfully.");
    }

    // Method to create and add a new map
    public void AddNewMap(string mapName, string creatorName = "Unknown")
    {
        MapData newMap = new MapData(mapName, creatorName);
        maps.Add(newMap);
        SaveMaps();
    }

    // Method to get all names of loaded maps
    public List<string> GetMapNames()
    {
        List<string> mapNames = new List<string>();
        foreach (MapData map in maps)
        {
            mapNames.Add(map.mapName);
        }
        return mapNames;
    }

    // Method to instantiate objects from the loaded map
    public void LoadMapIntoScene(MapData map)
    {
        ClearScene();

        // Set player spawn point
        SetPlayerSpawn(map.spawnPoint);

        // Instantiate escape objective (e.g., door)
        InstantiateObject(map.escapeObjective.type, map.escapeObjective.position, Vector3.zero, Vector3.one);

        // Instantiate all objects in the map
        foreach (var objData in map.objects)
        {
            GameObject obj = InstantiateObject(objData.type, objData.position, objData.rotation, objData.scale);
            if (obj != null)
            {
                instantiatedObjects[objData.id] = obj;
                ApplyProperties(obj, objData.properties);
            }
        }
    }

    private void ClearScene()
    {
        foreach (var obj in instantiatedObjects.Values)
        {
            Destroy(obj);
        }
        instantiatedObjects.Clear();
    }

    private void SetPlayerSpawn(SpawnPoint spawnPoint)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = spawnPoint.position;
            player.transform.rotation = Quaternion.Euler(spawnPoint.rotation);
        }
    }

    private GameObject InstantiateObject(string type, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        GameObject prefab = GetPrefabByType(type);
        if (prefab == null)
        {
            Debug.LogWarning($"No prefab found for type: {type}");
            return null;
        }

        GameObject obj = Instantiate(prefab, position, Quaternion.Euler(rotation));
        obj.transform.localScale = scale;
        return obj;
    }

    private GameObject GetPrefabByType(string type)
    {
        switch (type)
        {
            case "Key": return keyPrefab;
            case "Door": return doorPrefab;
            case "PuzzleBox": return puzzleBoxPrefab;
            default: return null;
        }
    }

    private void ApplyProperties(GameObject obj, Dictionary<string, object> properties)
    {
        if (properties == null) return;

        // Example: Applying properties to a Door or Key object
        if (obj.TryGetComponent(out Door door))
        {
            if (properties.ContainsKey("isLocked"))
                door.isLocked = (bool)properties["isLocked"];
        }
        else if (obj.TryGetComponent(out Key key))
        {
            if (properties.ContainsKey("unlockDoorId"))
                key.unlockDoorId = properties["unlockDoorId"].ToString();
        }
    }
}
