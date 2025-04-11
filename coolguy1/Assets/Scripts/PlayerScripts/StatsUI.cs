using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    private bool statsOpen = false;

    private void Start()
    {
        UpdateAllStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsOpen)
            {
                Time.timeScale = 1.0f;
                UpdateAllStats();
                statsCanvas.alpha = 0f;
                statsCanvas.blocksRaycasts = false;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0f;
                UpdateAllStats();
                statsCanvas.alpha = 1f;
                statsCanvas.blocksRaycasts = true;
                statsOpen = true;
            }
        }
    }
    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance.damage;
    }
    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance.speed;
    }
    public void UpdateKills()
    {
        statsSlots[8].GetComponentInChildren<TMP_Text>().text = "Kills: " + StatsManager.Instance.kills;
    }
    public void UpdateDeaths()
    {
        statsSlots[9].GetComponentInChildren<TMP_Text>().text = "Deaths: " + StatsManager.Instance.deaths;
    }


    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
        UpdateKills();
        UpdateDeaths();
    }
}
