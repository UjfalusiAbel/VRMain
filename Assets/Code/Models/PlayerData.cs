using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRMain.Assets.Code.Models
{
    [Serializable]
    public class PlayerData
    {
        public HashSet<int> LevelsFinished { get; set; }
        public PlayerData()
        {
            LevelsFinished = new HashSet<int>();
            LevelsFinished.Add(0);
        }
    }
}