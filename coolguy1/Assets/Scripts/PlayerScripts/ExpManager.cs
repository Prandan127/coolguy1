using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowthMultiplier = 1.2f; //20%
    public Slider expSlider;
    public TMP_Text currentLevelText;

    private void Start()
    {
        UpdateUI();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            GainExperience(2);
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnMonsterDefeat += GainExperience;   
    }
    private void OnDisable()
    {
        EnemyHealth.OnMonsterDefeat -= GainExperience;
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevel )
        {
            LevelUp();
        }
        UpdateUI();
    }    
    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level;
    }
}
