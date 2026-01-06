using UnityEngine;

namespace VRMain.Assets.Code.Models.ScriptableObjects
{
    public abstract class DialogueAction : ScriptableObject
    {
        public abstract void Execute();
    }
}