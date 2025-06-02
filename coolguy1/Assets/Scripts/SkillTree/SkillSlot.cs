using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;
    public int skillLevel { get; set; }
    public bool isUnlocked;

    public Image skillIcon;
    public Button skillButton;
    public Image skillBorder;
    public TMP_Text skillNameText;
    public TMP_Text skillLevelText;

    public delegate void AbilityPointSpent(SkillSlot slot);
    public static event AbilityPointSpent OnAbilityPointSpent;
    public delegate void SkillMaxed(SkillSlot slot);
    public static event SkillMaxed OnSkillMaxed;
    public Color unlockedColor;
    public Color lockedColor;

    private void Start()
    {
        skillIcon.sprite = skillSO.skillIcon;
        skillNameText.text = skillSO.skillName;
        skillLevel = 0;

        if (CanUnlockSkill())
        {
            Unlock();
        }
        else
        {
            Lock();
        }
    }

    public bool CanUnlockSkill()
    {
        if (skillSO.requiredSkills == null)
            return true;

        foreach (string skillName in skillSO.requiredSkills)
        {
            SkillSlot[] skillSlots = FindObjectsOfType<SkillSlot>();
            foreach (SkillSlot slot in skillSlots)
            {
                if (slot.skillSO.skillName == skillName && slot.skillLevel <= 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Unlock()
    {
        isUnlocked = true;
        skillBorder.color = unlockedColor;
    }
    private void Lock()
    {
        isUnlocked = false;
        skillBorder.color = lockedColor;
    }

    public void TryUpgradeSkill()
    {
        skillLevel++;
        UpdateUI();
        OnAbilityPointSpent?.Invoke(this);

        if (skillLevel >= skillSO.maxLevel)
        {
            OnSkillMaxed?.Invoke(this);
        }
    }

    public void UpdateUI()
    {
        skillLevelText.text = skillLevel + "/" + skillSO.maxLevel;
    }
}