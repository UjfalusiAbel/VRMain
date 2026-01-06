using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VRMain.Assets.Code.GamePlay.Character;
using VRMain.Assets.Code.GamePlay.NPC;

namespace VRMain.Assets.Code.Managers
{
    public class Activator : MonoBehaviour
    {
        [SerializeField] private NPCDialogueInteractor _interactor;
        [SerializeField] private DialogueController _dialogueController;
        [SerializeField] private MovementController _movementController;
        public void Awake()
        {
            _movementController.enabled = true;
            _interactor.enabled = true;
            _dialogueController.enabled = true;
        }
    }
}