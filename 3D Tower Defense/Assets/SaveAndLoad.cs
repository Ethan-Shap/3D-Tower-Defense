using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;

public class SaveAndLoad : MonoBehaviour {
    private string jsonString;
    private JsonData gameData;

    public void ReadGameData(out PlayerData playerData, out LevelData levelData)
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string jsonString = null;
        FileStream fStream = File.Open(Application.dataPath + "Resources/SaveData.json", FileMode.Open);
        jsonString = (string)bFormatter.Deserialize(fStream);
        SaveData saveData = JsonMapper.ToObject<SaveData>(jsonString); 
        fStream.Close();
        playerData = saveData.playerData;
        levelData = saveData.levelData;
    }

    public void SaveGameData(PlayerData playerData, LevelData levelData)
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        SaveData data = new SaveData(levelData, playerData);
        JsonData jsonString = JsonMapper.ToJson(data);
        FileStream fStream = new FileStream(Application.dataPath + "Resources/SaveData.json", FileMode.OpenOrCreate);
        bFormatter.Serialize(fStream, jsonString); 
        fStream.Close();
    }
}

[System.Serializable]
public class SaveData
{
    public LevelData levelData;
    public PlayerData playerData;

    public SaveData(LevelData levelData, PlayerData playerData)
    {
        this.levelData = levelData;
        this.playerData = playerData;
    }
}
 
public class LevelData
{
    public int level;
    public Dictionary<string, int> towerPositions; 
    public Dictionary<string, int> enemyPositions;
    public int round;
    public string spawnRate;
    public int coins;
    public int health;

    public LevelData(int level, Dictionary<string, int> towerPositions, Dictionary<string, int> enemyPositions, int round, string spawnRate, int coins, int health)
    {
        this.level = level;
        this.towerPositions = towerPositions;
        this.enemyPositions = enemyPositions;
        this.round = round;
        this.spawnRate = spawnRate;
        this.coins = coins;
        this.health = health;
    }
}

public class PlayerData
{
    public string name;
    public int levelsUnlocked;

    public PlayerData(string name, int levelsUnlocked)
    {
        this.name = name;
        this.levelsUnlocked = levelsUnlocked;
    }
}

 /// <summary>
 /// Since unity doesn't flag the Vector3 as serializable, we
 /// need to create our own version. This one will automatically convert
 /// between Vector3 and SerializableVector3
 /// </summary>
[System.Serializable]
public struct SerializableVector3
{
    /// <summary>
    /// x component
    /// </summary>
    public float x;

    /// <summary>
    /// y component
    /// </summary>
    public float y;

    /// <summary>
    /// z component
    /// </summary>
    public float z;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="rX"></param>
    /// <param name="rY"></param>
    /// <param name="rZ"></param>
    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    /// <summary>
    /// Returns a string representation of the object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Format("[{0}, {1}, {2}]", x, y, z);
    }

    /// <summary>
    /// Automatic conversion from SerializableVector3 to Vector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator Vector3(SerializableVector3 rValue)
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }

    /// <summary>
    /// Automatic conversion from Vector3 to SerializableVector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator SerializableVector3(Vector3 rValue)
    {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }
}
