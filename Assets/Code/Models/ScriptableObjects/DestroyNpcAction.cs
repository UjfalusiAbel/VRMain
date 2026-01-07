using UnityEngine;

namespace VRMain.Assets.Code.Models.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Dialogue/Actions/Destroy NPC")]
    public class DestroyNpcAction : DialogueAction
    {
        public override void Execute(GameObject context)
        {
            if (context == null)
            {
                Debug.LogWarning("DestroyNpcAction executed with null context.");
                return;
            }

            Destroy(context);
        }
    }
}
