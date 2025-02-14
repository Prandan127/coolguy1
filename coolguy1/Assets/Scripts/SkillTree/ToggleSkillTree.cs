using UnityEngine;

public class ToggleSkillTree : MonoBehaviour
{
    public CanvasGroup statsCanvas;
    private bool skillTreeOpen = false;

    private void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1.0f;
                statsCanvas.alpha = 0f;
                statsCanvas.blocksRaycasts = false;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0f;
                statsCanvas.alpha = 1.0f;
                statsCanvas.blocksRaycasts = true;
                skillTreeOpen = true;
            }
        }
    }
}
