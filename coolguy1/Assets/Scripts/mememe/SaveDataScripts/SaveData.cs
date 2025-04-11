using System.Collections.Generic;
using UnityEngine;
using static QuestNPC;

[System.Serializable]
public class SaveData
{
    public float playerX;
    public float playerY;
    public float playerZ;

    public int gold;
    public List<InventorySlotData> inventory = new List<InventorySlotData>();
    public List<SkillSlotData> skillTree = new List<SkillSlotData>();

    public QuestState questState;
}

[System.Serializable]
public class InventorySlotData
{
    public string itemID;
    public int quantity;
}

[System.Serializable]
public class SkillSlotData
{
    public string skillName;
    public int skillLevel;
    public bool isUnlocked;
}
