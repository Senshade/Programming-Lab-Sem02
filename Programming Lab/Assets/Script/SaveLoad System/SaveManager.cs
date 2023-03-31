using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public byte[] score;
}

public class SaveManager : MonoBehaviour {
    private string saveFileName = "savedjsonscore.json";
    private byte[] key = new byte[] { 0x34, 0x69, 0x42, 0x66 }; 

    public void SaveGame(int score) {
        SaveData data = new SaveData();
        data.score = Encoding.UTF8.GetBytes(score.ToString());

      
        for (int i = 0; i < data.score.Length; i++) {
            data.score[i] = (byte)(data.score[i] ^ key[i % key.Length]);
        }

        string jsonData = JsonUtility.ToJson(data);
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        File.WriteAllText(filePath, jsonData);
    }

    public int LoadGame() {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath)) {
            string jsonData = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);

          
            for (int i = 0; i < data.score.Length; i++) {
                data.score[i] = (byte)(data.score[i] ^ key[i % key.Length]);
            }

            string scoreString = Encoding.UTF8.GetString(data.score);
            int score = 0;
            int.TryParse(scoreString, out score);

            return score;
        } 
        else {
            return 0;
        }
    }
}