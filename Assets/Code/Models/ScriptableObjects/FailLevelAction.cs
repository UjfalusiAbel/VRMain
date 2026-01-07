using UnityEngine;

namespace VRMain.Assets.Code.Models.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Dialogue/Actions/Fail Level")]
    public class FailLevelAction : DialogueAction
    {
        public override void Execute(GameObject context)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameManager.Singleton.LooseLevel();
        }
    }
}