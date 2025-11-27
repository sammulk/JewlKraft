using System;
using System.IO;
using Core.Shop_files.Scripts.CustomerScripts;
using UnityEngine;

namespace Core.Inventory_files.Scripts
{
    public static class SaveManager
    {
        private static string SAVE_PATH =>
            Path.Combine(Application.persistentDataPath, "jewlsave.json");

        public static void SaveGame(GameSave save){
            string json = JsonUtility.ToJson(save, true);
            File.WriteAllText(SAVE_PATH, json);
        }

        public static GameSave LoadGame()
            //PlayerUpgradeManager upgrades)
        {
            if (!File.Exists(SAVE_PATH))
                return null;

            string json = File.ReadAllText(SAVE_PATH);
            GameSave save = JsonUtility.FromJson<GameSave>(json);

            return save;
        }
    }

    [Serializable]
    public class GameSave
    {
        public PlayerInventorySaveData playerInventory;
        public CustomerSaveData customers;
        //public PlayerUpgradeSaveData upgrades;
    }
    
    public interface ISaveable<T>
    {
        T ToSaveData();
        void FromSaveData(T data);
    }

}