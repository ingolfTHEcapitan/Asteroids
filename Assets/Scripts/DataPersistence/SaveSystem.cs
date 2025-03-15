using System.IO;
using Asteroids.DataPersistence.Data;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Asteroids.DataPersistence
{
    public static class SaveSystem
    {
        private static readonly string _saveDirectoryPath;
        private static readonly string _savePath;
        
        static SaveSystem()
        {
            _saveDirectoryPath = Path.Combine(Application.persistentDataPath, "Saves");
            _savePath = Path.Combine(Application.persistentDataPath, "Saves" ,"save.json");
        }

        public static SaveData Load()
        {
            if (File.Exists(_savePath))
            {
                string jsonString = File.ReadAllText(_savePath);
                SaveData saveData = JsonUtility.FromJson<SaveData>(jsonString);
                return saveData;
            }
            
            SaveData newSaveData = new SaveData();
            Save(newSaveData);
            return newSaveData;
        }
        
        public static void Save(SaveData saveData)
        {
            if (!Directory.Exists(_saveDirectoryPath))
            {
                Directory.CreateDirectory(_saveDirectoryPath);
            }
            
            string jsonString = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(_savePath, jsonString);
        }
    }
}