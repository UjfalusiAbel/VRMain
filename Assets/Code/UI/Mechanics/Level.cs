using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VRMain.Assets.Code.Enums;

namespace VRMain.Assets.Code.UI.Mechanics
{
    public class Level
    {
        [SerializeField]
        private int _level;
        [SerializeField]
        private LevelDifficulty _difficulty;
        private bool _isLocked = true;
    }
}