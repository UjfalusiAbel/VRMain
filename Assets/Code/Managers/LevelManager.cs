using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VRMain.Assets.Code.UI.Mechanics;

namespace VRMain.Assets.Code.Managers
{
    public class LevelManager : MonoBehaviour 
    {
        [SerializeField]
        private List<Level> _levels;
        private static LevelManager _instance;
        public static LevelManager Singleton => _instance;

        public void Awake()
        {
            _instance = this;
        }

        public void Start()
        {
            CheckLevels();
        }

        public void CheckLevels()
        {
            foreach(var level in _levels)
            {
                if(level == null)
                {
                    continue;
                }

                if(GameManager.Singleton.PlayerData.LevelsFinished.Contains(level.GetLevel - 1))
                {
                    level.Unlock();
                }
                else
                {
                    level.Lock();
                }
            }
        }
    }
}