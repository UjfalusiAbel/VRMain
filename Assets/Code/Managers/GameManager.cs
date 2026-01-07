using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using VRMain.Assets.Code.GamePlay.Character;
using VRMain.Assets.Code.GamePlay.NPC;
using VRMain.Assets.Code.Managers;
using VRMain.Assets.Code.Models;
using VRMain.Assets.Code.Utils;

namespace VRMain.Assets.Code
{
    public class GameManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject _levelFailedPrefab;
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

        public void Start()
        {
            _playerData = new PlayerData();
            var saveFilePath = Path.Combine(Application.persistentDataPath, _saveFileName);
            if (File.Exists(saveFilePath))
            {
                var rawData = File.ReadAllText(saveFilePath);
                var decryptedData = _encryptor.EncryptDecrypt(rawData);
                _playerData = JsonConvert.DeserializeObject<PlayerData>(decryptedData);
            }
            else
            {
                _playerData = new PlayerData();
            }
        }

        public void FinishLevel(int level)
        {
            _playerData.LevelsFinished.Add(level);
            SaveData();
        }

        public void LooseLevel()
        {
            if (_levelFailedPrefab == null)
            {
                Debug.LogError("LevelFailed prefab not assigned!");
                return;
            }

            var canvas = GameObject.FindGameObjectWithTag("Canvas");

            Instantiate(_levelFailedPrefab, canvas.transform);

            if (MovementController.Singleton != null)
            {
                MovementController.Singleton.IsLocked = true;
            }
        }

        public void ResetAll()
        {
            _playerData = new PlayerData();
            SaveData();
            LevelManager.Singleton.CheckLevels();
        }


        public void SaveData()
        {
            var saveFilePath = Path.Combine(Application.persistentDataPath, _saveFileName);
            if (!File.Exists(saveFilePath))
            {
                File.Create(saveFilePath);
            }

            var jsonText = JsonConvert.SerializeObject(_playerData);
            var encryptedText = _encryptor.EncryptDecrypt(jsonText);
            File.WriteAllText(saveFilePath, encryptedText);
            Debug.Log($"{jsonText} and {_playerData.LevelsFinished.ToList().Count}");
        }
    }
}