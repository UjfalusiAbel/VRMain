using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRMain.Assets.Code.GamePlay.Character;

namespace VRMain.Assets.Code.UI.Utility
{
    public class TextDisappear : MonoBehaviour
    {
        [Header("Fade Settings")]
        [SerializeField] private float _fadeDuration = 1f;
        [TextArea][SerializeField] private string _text;
        private Button _closeButton;
        private TMP_Text _tmpText;
        private Coroutine _fadeCoroutine;

        private void Awake()
        {
            _tmpText = GetComponentInChildren<TMP_Text>();
            _tmpText.text = _text;
            _tmpText.alpha = 0f;
            _closeButton = GetComponentInChildren<Button>();
            _closeButton.gameObject.SetActive(false);

            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(FadeOut);
            }
        }

        private void Start()
        {
            FadeIn();
        }

        private void FadeIn()
        {
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }

            MovementController.Singleton.IsLocked = true;
            _fadeCoroutine = StartCoroutine(FadeRoutine(0f, 1f));
            _closeButton.gameObject.SetActive(true);
        }

        private void FadeOut()
        {
            _closeButton.gameObject.SetActive(false);
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }

            _fadeCoroutine = StartCoroutine(FadeOutRoutine());
            MovementController.Singleton.IsLocked = false;
        }

        private IEnumerator FadeRoutine(float from, float to)
        {
            float elapsed = 0f;

            while (elapsed < _fadeDuration)
            {
                elapsed += Time.deltaTime;
                _tmpText.alpha = Mathf.Lerp(from, to, elapsed / _fadeDuration);
                yield return null;
            }

            _tmpText.alpha = to;
        }

        private IEnumerator FadeOutRoutine()
        {
            yield return FadeRoutine(1f, 0f);
            gameObject.SetActive(false);
        }
    }
}
