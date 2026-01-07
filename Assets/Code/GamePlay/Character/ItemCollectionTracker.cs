using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace VRMain.Assets.Code.GamePlay.Character
{
    public class ItemCollectionTracker : MonoBehaviour
    {
        [SerializeField] private int _requiredItemCount = 5;

        public int CollectedCount { get; private set; }

        public event Action<int, int> OnProgressChanged;

        private void Start()
        {
            CollectedCount = 0;
            OnProgressChanged?.Invoke(CollectedCount, _requiredItemCount);
        }

        public void CollectItem()
        {
            if (CollectedCount >= _requiredItemCount)
            {
                return;
            }

            CollectedCount++;
            OnProgressChanged?.Invoke(CollectedCount, _requiredItemCount);
            if (IsComplete())
            {
                GameManager.Singleton.FinishLevel(2);
                SceneManager.LoadScene("MainMenu");
            }
        }

        public bool IsComplete()
        {
            return CollectedCount >= _requiredItemCount;
        }
    }
}