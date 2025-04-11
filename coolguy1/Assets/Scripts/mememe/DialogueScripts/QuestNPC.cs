using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public enum QuestState
    {
        Mushrooms,
        Boss,
        Completed
    }

    public QuestState currentQuest = QuestState.Mushrooms;

    [Header("Mushroom Quest")]
    public string mushroomsDialogueBefore = "Bring me 3 mushrooms.";
    public string mushroomsDialogueNotEnough = "You don't have enough mushrooms.";
    public string mushroomsDialogueComplete = "Thank you for the mushrooms!";
    public ItemSO mushroomsRequiredItem;
    public int mushroomsRequiredAmount = 3;

    [Header("Boss Quest")]
    public string bossDialogueBefore = "Kill the boss at the end of this location.";
    public string bossDialogueNotKilled = "You haven't killed the boss yet!";
    public string bossDialogueComplete = "Great job! You killed the boss!";
    public string bossTag = "Enemy";
    private bool bossKilled = false;

    private bool isPlayerInRange = false;

    public DialogueUIManager dialogueUIManager;

    private void Start()
    {
        GameObject boss = GameObject.FindGameObjectWithTag(bossTag);
        if (boss != null && boss.GetComponent<EnemyHealth>() != null)
        {
            boss.GetComponent<EnemyHealth>().OnDeath += OnBossDeath;
        }

        dialogueUIManager.Initialize(this);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            ShowDialogueOptions();
        }
    }

    private void ShowDialogueOptions()
    {
        string dialogueText = GetCurrentDialogueText();
        dialogueUIManager.ShowDialogue(dialogueText);
    }

    private string GetCurrentDialogueText()
    {
        switch (currentQuest)
        {
            case QuestState.Mushrooms: return mushroomsDialogueBefore;
            case QuestState.Boss: return bossDialogueBefore;
            case QuestState.Completed: return "All quests completed!";
            default: return "Error: Unknown quest state";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueUIManager.HideDialogue();
        }
    }

    public void Interact()
    {
        switch (currentQuest)
        {
            case QuestState.Mushrooms: HandleMushroomQuest(); break;
            case QuestState.Boss: HandleBossQuest(); break;
            case QuestState.Completed: FinalDialogue(); break;
        }
    }

    private void HandleMushroomQuest()
    {
        int count = InventoryManager.Instance.GetItemCount(mushroomsRequiredItem);

        if (count >= mushroomsRequiredAmount)
        {
            InventoryManager.Instance.RemoveItem(mushroomsRequiredItem, mushroomsRequiredAmount);
            InventoryManager.Instance.gold += 10;
            InventoryManager.Instance.goldText.text = InventoryManager.Instance.gold.ToString();

            currentQuest = QuestState.Boss;
            dialogueUIManager.ShowDialogue(mushroomsDialogueComplete + "\n" + bossDialogueBefore);
        }
        else
        {
            dialogueUIManager.ShowDialogue(count > 0 ? mushroomsDialogueNotEnough : mushroomsDialogueBefore);
        }
    }

    private void HandleBossQuest()
    {
        if (bossKilled)
        {
            currentQuest = QuestState.Completed;
            InventoryManager.Instance.gold += 50;
            InventoryManager.Instance.goldText.text = InventoryManager.Instance.gold.ToString();
            dialogueUIManager.ShowDialogue(bossDialogueComplete);
        }
        else
        {
            dialogueUIManager.ShowDialogue(bossDialogueNotKilled);
        }
    }

    private void FinalDialogue()
    {
        dialogueUIManager.ShowDialogue("Come back later!");
    }

    private void OnBossDeath()
    {
        bossKilled = true;
    }
}
