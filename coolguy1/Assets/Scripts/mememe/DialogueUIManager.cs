using UnityEngine;
using TMPro;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager Instance;

    public CanvasGroup canvasGroup;
    public TMP_Text dialogueText;

    private void Awake()
    {
        Instance = this;
        HideDialogue();
    }

    public void ShowDialogue(string text)
    {
        dialogueText.text = text;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void HideDialogue()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private void Update()
    {
        if (canvasGroup.alpha > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            HideDialogue();
        }
    }
}
