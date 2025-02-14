using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public PlayerCombat combat;
    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandlerAbilityPointSpent;
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandlerAbilityPointSpent;
    }

    private void HandlerAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;
        
        switch(skillName)
        {
            case "Max Health Boost":
                {
                    StatsManager.Instance.UpdateMaxHealth(1);
                    break;
                }
            case "Sword Slash":
                {
                    combat.enabled = true;
                    break;
                }
            default:
                {
                    Debug.LogWarning("Unknown Skill: " + skillName);
                    break;
                }
        }
    }
}
