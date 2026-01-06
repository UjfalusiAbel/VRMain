using UnityEngine;
using UnityEngine.Events;
using VRMain.Assets.Code.Models.ScriptableObjects;

namespace VRMain.Assets.Code.Models
{
    [System.Serializable]
    public class DialogueChoice
    {
        public string choiceText;
        public DialogueNode nextNode;
        public DialogueAction action;
    }

}