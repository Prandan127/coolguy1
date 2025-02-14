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
            case "Health":
                {
                    StatsManager.Instance.UpdateMaxHealth(1);
                    break;
                }
            case "CombatUnlock":
                {
                    combat.enabled = true;
                    break;
                }
            case "Strength":
                {
                    StatsManager.Instance.UpdateDamage(1);
                    break;
                }
            case "Agility":
                {

                    break;
                }
            case "Regen":
                {

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
