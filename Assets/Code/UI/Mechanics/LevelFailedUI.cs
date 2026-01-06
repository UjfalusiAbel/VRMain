using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VRMain.Assets.Code.UI.Mechanics
{

    namespace VRMain.Assets.Code.UI
    {
        public class LevelFailedUI : MonoBehaviour
        {
            [SerializeField] private Button _backToMenuButton;
            [SerializeField] private string _menuSceneName = "MainMenu";

            private void Awake()
            {
                _backToMenuButton.onClick.AddListener(GoToMenu);
            }

            private void GoToMenu()
            {
                SceneManager.LoadScene(_menuSceneName);
            }
        }
    }

}