using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager Instance;

    [Header("UI Components")]
    public CanvasGroup dialoguePanel;
    public TMP_Text dialogueText;
    public Button talkButton;
    public Button leaveButton;

    private QuestNPC currentNPC;

    private void Awake()
    {
        Instance = this;
        HideDialogue();
    }

    public void Initialize(QuestNPC npc)
    {
        currentNPC = npc;

        talkButton.onClick.RemoveAllListeners();
        leaveButton.onClick.RemoveAllListeners();

        talkButton.onClick.AddListener(OnTalkButtonClicked);
        leaveButton.onClick.AddListener(OnLeaveButtonClicked);
    }

    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
        dialoguePanel.alpha = 1;
        dialoguePanel.blocksRaycasts = true;
        dialoguePanel.interactable = true;
        Time.timeScale = 0;
    }

    public void HideDialogue()
    {
        dialoguePanel.alpha = 0;
        dialoguePanel.blocksRaycasts = false;
        dialoguePanel.interactable = false;
        Time.timeScale = 1;
    }

    public void OnTalkButtonClicked()
    {
        currentNPC.Interact();
        HideDialogue();
    }

    public void OnLeaveButtonClicked()
    {
        HideDialogue();
    }
}
