using UnityEngine;
using UnityEngine.InputSystem;
using VRMain.Assets.Code.GamePlay.Character;
using VRMain.Assets.Code.Models.ScriptableObjects;

namespace VRMain.Assets.Code.GamePlay.NPC
{
    public class NPCDialogueInteractor : MonoBehaviour
    {
        [Header("Dialogue")]
        [SerializeField] private DialogueNode _startingDialogue;
        [SerializeField] private DialogueController _dialogueController;

        [Header("Interaction")]
        [SerializeField] private float _interactionRange = 2f;
        [SerializeField] private Transform _player;
        private bool _finishedInteraction = false;

        private void OnEnable()
        {
            if (MovementController.Singleton != null)
            {
                MovementController.Singleton.OnInteractPressed += TryInteract;
            }
        }

        private void OnDisable()
        {
            if (MovementController.Singleton != null)
            {
                MovementController.Singleton.OnInteractPressed -= TryInteract;
            }
        }

        private void TryInteract()
        {
            if (!IsPlayerInRange() || _finishedInteraction)
            {
                return;
            }

            MovementController.Singleton.IsLocked = true;
            _dialogueController.StartDialogue(_startingDialogue, gameObject);
            _finishedInteraction = true;
            enabled = false;
        }

        private bool IsPlayerInRange()
        {
            float distance = Vector3.Distance(transform.position, _player.position);
            return distance <= _interactionRange;
        }
    }

}