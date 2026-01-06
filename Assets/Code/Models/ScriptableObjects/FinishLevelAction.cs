using UnityEngine;
using UnityEngine.SceneManagement;
using VRMain.Assets.Code;

namespace VRMain.Assets.Code.Models.ScriptableObjects
{

    [CreateAssetMenu(menuName = "Dialogue/Actions/Finish Level")]
    public class FinishLevelAction : DialogueAction
    {
        [Header("Level to Finish")]
        [SerializeField] private int _levelNumber = 1;

        public override void Execute()
        {
            if (GameManager.Singleton != null)
            {
                GameManager.Singleton.FinishLevel(_levelNumber);
                SceneManager.LoadScene("MainMenu");
                Debug.Log($"Level {_levelNumber} marked as finished!");
            }
            else
            {
                Debug.LogWarning("GameManager.Singleton is null. Cannot finish level.");
            }
        }
    }

}