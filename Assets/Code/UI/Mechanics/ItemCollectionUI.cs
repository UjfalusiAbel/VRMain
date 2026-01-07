using TMPro;
using UnityEngine;
using VRMain.Assets.Code.GamePlay.Character;

namespace VRMain.Assets.Code.UI.Mechanics
{
    namespace VRMain.Assets.Code.UI
    {
        public class ItemCollectionUI : MonoBehaviour
        {
            [SerializeField] private TMP_Text _progressText;

            private ItemCollectionTracker _tracker;

            private void Start()
            {
                _tracker = FindFirstObjectByType<ItemCollectionTracker>();

                if (_tracker == null)
                {
                    Debug.LogError("ItemCollectionTracker not found.");
                    return;
                }

                _tracker.OnProgressChanged += UpdateUI;
                UpdateUI(_tracker.CollectedCount, 5);
            }

            private void OnDestroy()
            {
                if (_tracker != null)
                {
                    _tracker.OnProgressChanged -= UpdateUI;
                }
            }

            private void UpdateUI(int collected, int required)
            {
                _progressText.text = $"Items: {collected} / {required}";
            }
        }
    }

}