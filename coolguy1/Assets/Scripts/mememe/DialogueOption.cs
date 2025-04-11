using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueOption : MonoBehaviour
{
    public Button talkButton;
    public Button leaveButton;
    public TextMeshProUGUI dialogueText;

    private QuestNPC currentNPC;

    public void Initialize(QuestNPC npc)
    {
        currentNPC = npc;

        talkButton.onClick.RemoveAllListeners();
        leaveButton.onClick.RemoveAllListeners();

        talkButton.onClick.AddListener(OnTalkButtonClicked);
        leaveButton.onClick.AddListener(OnLeaveButtonClicked);
    }

    public void OnTalkButtonClicked()
    {
        currentNPC.Interact();
        HideDialogueOptions();
    }

    public void OnLeaveButtonClicked()
    {
        HideDialogueOptions();
    }

    public void ShowDialogueOptions(string text)
    {
        dialogueText.text = text;
        gameObject.SetActive(true);
    }

    public void HideDialogueOptions()
    {
        gameObject.SetActive(false);
    }
}
