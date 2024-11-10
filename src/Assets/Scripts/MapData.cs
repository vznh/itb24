using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public string mapName;
    public string creator;
    public DateTime creationDate;
    public string description;
    public string difficulty;
    public int version;

    public SpawnPoint spawnPoint;
    public EscapeObjective escapeObjective;
    public List<GameObjectData> objects;

    // Constructor for creating a new map
    public MapData(string name, string creatorName = "Unknown")
    {
        mapName = name;
        creator = creatorName;
        creationDate = DateTime.Now;
        description = "";
        difficulty = "";
        version = 1;
        spawnPoint = new SpawnPoint(Vector3.zero, Vector3.zero);
        escapeObjective = new EscapeObjective("door", Vector3.zero, true);
        objects = new List<GameObjectData>();
    }
}

[Serializable]
public class SpawnPoint
{
    public Vector3 position;
    public Vector3 rotation;

    public SpawnPoint(Vector3 pos, Vector3 rot)
    {
        position = pos;
        rotation = rot;
    }
}

[Serializable]
public class EscapeObjective
{
    public string type;
    public Vector3 position;
    public bool condition;

    public EscapeObjective(string objType, Vector3 pos, bool cond)
    {
        type = objType;
        position = pos;
        condition = cond;
    }
}

[Serializable]
public class GameObjectData
{
    public string id;
    public string type;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public Dictionary<string, object> properties;

    public GameObjectData(string objId, string objType, Vector3 pos, Vector3 rot, Vector3 scl)
    {
        id = objId;
        type = objType;
        position = pos;
        rotation = rot;
        scale = scl;
        properties = new Dictionary<string, object>();
    }
}
