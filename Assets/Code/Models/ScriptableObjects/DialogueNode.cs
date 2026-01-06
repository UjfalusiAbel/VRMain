using UnityEngine;
using UnityEngine.Events;

namespace VRMain.Assets.Code.Models.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Dialogue/Dialogue Node")]
    public class DialogueNode : ScriptableObject
    {
        [TextArea]
        public string dialogueText;

        public DialogueChoice optionA;
        public DialogueChoice optionB;
    }

}