using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public string saveFileName = "default_save.xml";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(TMP_InputField inputField)
    {
        Debug.Log("SaveGame called!");
        string saveName = inputField.text;
        SaveData data = new SaveData();

        data.playerX = InventoryManager.Instance.player.position.x;
        data.playerY = InventoryManager.Instance.player.position.y;
        data.playerZ = InventoryManager.Instance.player.position.z;

        data.gold = InventoryManager.Instance.gold;
        foreach (var slot in InventoryManager.Instance.itemSlots)
        {
            if (slot.itemSO != null)
            {
                data.inventory.Add(new InventorySlotData { itemID = slot.itemSO.itemID, quantity = slot.quantity });
            }
        }

        if (SkillTreeManager.Instance != null)
        {
            foreach (var slot in SkillTreeManager.Instance.skillSlots)
            {
                data.skillTree.Add(new SkillSlotData { skillName = slot.skillSO.skillName, skillLevel = slot.skillLevel, isUnlocked = slot.isUnlocked });
            }
        }
        else Debug.LogError("SkillTreeManager.Instance is null!");

#pragma warning disable CS0618 // Тип или член устарел
        QuestNPC questNPC = FindObjectOfType<QuestNPC>();
#pragma warning restore CS0618 // Тип или член устарел
        if (questNPC != null)
        {
            data.questState = questNPC.currentQuest;
        }
        else Debug.LogError("QuestNPC not found in the scene!");

        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        string path = Path.Combine(Application.persistentDataPath, saveName + ".xml");
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }

        Debug.Log("Game Saved to " + path);
    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                SaveData data = serializer.Deserialize(stream) as SaveData;

                InventoryManager.Instance.player.position = new Vector3(data.playerX, data.playerY, data.playerZ);

                InventoryManager.Instance.gold = data.gold;
                InventoryManager.Instance.goldText.text = data.gold.ToString();
                foreach (var slot in InventoryManager.Instance.itemSlots)
                {
                    slot.itemSO = null;
                    slot.quantity = 0;
                    slot.UpdateUI();
                }
                foreach (var itemData in data.inventory)
                {
                    ItemSO item = ItemDatabase.Instance.GetItem(itemData.itemID);
                    InventoryManager.Instance.AddItem(item, itemData.quantity);
                }

                if (SkillTreeManager.Instance != null)
                {
                    foreach (var skillData in data.skillTree)
                    {
                        foreach (var slot in SkillTreeManager.Instance.skillSlots)
                        {
                            if (slot.skillSO.skillName == skillData.skillName)
                            {
                                slot.skillLevel = skillData.skillLevel;
                                slot.isUnlocked = skillData.isUnlocked;
                                slot.UpdateUI();
                                break;
                            }
                        }
                    }
                }
                else Debug.LogError("SkillTreeManager.Instance is null!");

#pragma warning disable CS0618 // Тип или член устарел
                QuestNPC questNPC = FindObjectOfType<QuestNPC>();
#pragma warning restore CS0618 // Тип или член устарел
                if (questNPC != null) questNPC.currentQuest = data.questState;
                else Debug.LogError("QuestNPC not found in the scene!");

                Debug.Log("Game Loaded from " + path);
            }
        }
        else Debug.LogWarning("Save file not found in " + path);
    }
}
