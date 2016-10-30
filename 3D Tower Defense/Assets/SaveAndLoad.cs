using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Cryptography;

public class SaveAndLoad : MonoBehaviour {
    private string jsonString;
    private Generic cryptographer = new Generic();
    private const string _Key = "GottaGetchaHeadInTheGame";

    public SaveData LoadGameData()
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream fStream = File.Open(Application.dataPath + "/Resources/SaveData.json", FileMode.Open);

        string encryptedText = (string)bFormatter.Deserialize(fStream);
        //Decrypt text with key 
        jsonString = (string)cryptographer.Crypt(Generic.CryptMethod.DECRYPT, Generic.CryptClass.AES, encryptedText, _Key);

        SaveData saveData = JsonMapper.ToObject<SaveData>(jsonString); 
        fStream.Close();
        Debug.Log("Successfully loaded level");
        return saveData;
    }

    public void SaveGameData(SaveData saveData)
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string jsonString = JsonMapper.ToJson(saveData);

        //Debug.Log(jsonString);

        //Encrypt text with key 
        string encryptedString = (string)cryptographer.Crypt(Generic.CryptMethod.ENCRYPT, Generic.CryptClass.AES, jsonString, _Key);

        FileStream fStream = new FileStream(Application.dataPath + "/Resources/SaveData.json", FileMode.OpenOrCreate); 
        bFormatter.Serialize(fStream, encryptedString);
        cryptographer.GetHash(fStream, Generic.HashMethod.MD5);

        fStream.Close();
        Debug.Log("Successully saved level");
    }
}

[System.Serializable]
public class SaveData
{
    public LevelData levelData;
    public PlayerData playerData;
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

    public LevelData()
    {
        this.level = 0;
        this.towerPositions = null;
        this.enemyPositions = null;
        this.round = 0;
        this.spawnRate = null;
        this.coins = 0;
        this.health = 0;
    }

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
    public string playerName = "BANANA";
    public int levelsUnlocked = 1;
    public int numberOfEnemiesKilled = 0;
    public int numberOfTimesDied = 0;
    public int towersPlaced = 0;

    public PlayerData()
    {
        this.playerName = null;
        this.levelsUnlocked = 0;
        this.numberOfEnemiesKilled = 0;
        this.numberOfTimesDied = 0;
        this.towersPlaced = 0;
    }

    public PlayerData(string playerName, int levelsUnlocked, int numberOfEnemiesKilled, int numberOfTimesDied, int towersPlaced)
    {
        this.playerName = playerName;
        this.levelsUnlocked = levelsUnlocked;
        this.numberOfEnemiesKilled = numberOfEnemiesKilled;
        this.numberOfTimesDied = numberOfTimesDied;
        this.towersPlaced = towersPlaced;
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