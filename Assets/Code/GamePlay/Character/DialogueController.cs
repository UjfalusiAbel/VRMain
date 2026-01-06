using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRMain.Assets.Code.Models;
using VRMain.Assets.Code.Models.ScriptableObjects;

namespace VRMain.Assets.Code.GamePlay.Character
{

    public class DialogueController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private Button optionAButton;
        [SerializeField] private Button optionBButton;
        [SerializeField] private TMP_Text optionAText;
        [SerializeField] private TMP_Text optionBText;

        private DialogueNode _currentNode;

        private void Awake()
        {
            optionAButton.onClick.AddListener(() => SelectOption(_currentNode.optionA));
            optionBButton.onClick.AddListener(() => SelectOption(_currentNode.optionB));
        }

        public void StartDialogue(DialogueNode startNode)
        {
            dialoguePanel.SetActive(true);
            SetNode(startNode);
        }

        private void SetNode(DialogueNode node)
        {
            _currentNode = node;

            dialogueText.text = node.dialogueText;

            optionAText.text = node.optionA.choiceText;
            optionBText.text = node.optionB.choiceText;

            optionAButton.gameObject.SetActive(node.optionA != null);
            optionBButton.gameObject.SetActive(node.optionB != null);
        }

        private void SelectOption(DialogueChoice choice)
        {
            choice.action?.Execute();

            if (choice.nextNode != null)
            {
                SetNode(choice.nextNode);
            }
            else
            {
                EndDialogue();
            }
        }

        private void EndDialogue()
        {
            dialoguePanel.SetActive(false);

            if (MovementController.Singleton != null)
            {
                MovementController.Singleton.IsLocked = false;
            }
        }
    }

}