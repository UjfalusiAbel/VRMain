using UnityEngine;
using VRMain.Assets.Code.GamePlay.Character;

namespace VRMain.Assets.Code.Models.ScriptableObjects
{

    [CreateAssetMenu(menuName = "Dialogue/Actions/Collect Scene Item")]
    public class CollectItemAction : DialogueAction
    {
        public override void Execute(GameObject context)
        {
            var tracker = FindFirstObjectByType<ItemCollectionTracker>();

            if (tracker == null)
            {
                Debug.LogWarning("ItemCollectionTracker not found in scene.");
                return;
            }

            tracker.CollectItem();
        }
    }

}