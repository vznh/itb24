using System;

[Serializable]
public class MapData
{
  public string mapName;
  public DateTime creationDate;

  public MapData(string name) {
    mapName = name;
    creationDate = DateTime.Now;
  }
}
