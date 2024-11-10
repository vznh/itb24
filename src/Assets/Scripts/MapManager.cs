using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class MapManager : MonoBehaviour
{
    public List<MapData> maps = new List<MapData>();
    private string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savedMaps.json");
        LoadMaps();
    }

    // Load maps from JSON
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

    // Save maps to JSON
    public void SaveMaps()
    {
        string json = JsonConvert.SerializeObject(maps, Formatting.Indented);
        File.WriteAllText(savePath, json);
        Debug.Log("Maps saved successfully.");
    }

    // Create a new map
    public void AddNewMap(string mapName, string creatorName = "Unknown")
    {
        MapData newMap = new MapData(mapName, creatorName);
        maps.Add(newMap);
        SaveMaps();
    }

    // Get all map names
    public List<string> GetMapNames()
    {
        List<string> mapNames = new List<string>();
        foreach (MapData map in maps)
        {
            mapNames.Add(map.mapName);
        }
        return mapNames;
    }
}
