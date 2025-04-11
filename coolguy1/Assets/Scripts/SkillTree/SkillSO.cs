using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillTree/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public string[] requiredSkills;
    public int maxLevel;
    public Sprite skillIcon;
}
