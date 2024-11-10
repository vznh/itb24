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
    }
  }

  // Method to save maps to a JSON file
  public void SaveMaps()
  {
    string json = JsonConvert.SerializeObject(maps, Formatting.Indented);
    File.WriteAllText(savePath, json);
  }

  // Method to create and add a new map
  public void AddNewMap(string mapName)
  {
    MapData newMap = new MapData(mapName);
    maps.Add(newMap);
    SaveMaps();
  }

  // Get all map names from loaded
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
