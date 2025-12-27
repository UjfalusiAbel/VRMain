using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VRMain.Assets.Code.Models;
using VRMain.Assets.Code.Utils;

namespace VRMain.Assets.Code
{
    public class GameManager : MonoBehaviour
    {
        private PlayerData _playerData;
        public PlayerData PlayerData => _playerData;
        private static GameManager _instance;
        public static GameManager Singleton => _instance;
        private Encryptor _encryptor = new Encryptor();
        private const string _saveFileName = "PlayerData.dat";

        public void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(_instance);
            }

            if (_instance == null)
            {
                _instance = this;
            }
        }

        public void OnDestroy()
        {
            SaveData();
        }

        public void Start()
        {
            _playerData = new PlayerData();
            var saveFilePath = Path.Combine(Application.persistentDataPath, _saveFileName);
            if (File.Exists(saveFilePath))
            {
                var rawData = File.ReadAllText(saveFilePath);
                var decryptedData = _encryptor.EncryptDecrypt(rawData);
                _playerData = JsonUtility.FromJson<PlayerData>(decryptedData);
            }
            else
            {
                _playerData = new PlayerData();
            }
        }

        public void FinishLevel(int level)
        {
            _playerData.LevelsFinished.Add(level);
        }

        public void SaveData()
        {
            var saveFilePath = Path.Combine(Application.persistentDataPath, _saveFileName);
            if (!File.Exists(saveFilePath))
            {
                File.Create(saveFilePath);
            }

            var jsonText = JsonUtility.ToJson(_playerData);
            var encryptedText = _encryptor.EncryptDecrypt(jsonText);
            File.WriteAllText(saveFilePath,encryptedText);
        }
    }
}