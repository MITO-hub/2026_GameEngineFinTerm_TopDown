using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    private SkillType selectedSkill = SkillType.None;
    private bool skillUsed = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        string skillName = PlayerPrefs.GetString("SelectedSkill", "None");

        int currentStageNumber = 1;

        if (GameManager.Instance != null)
        {
            currentStageNumber = GameManager.Instance.currentStageNumber;
        }

        if (currentStageNumber == 1)
        {
            selectedSkill = SkillType.None;
        }
        else if (currentStageNumber == 2)
        {
            if (skillName == "Shield")
                selectedSkill = SkillType.Shield;
            else
                selectedSkill = SkillType.None;
        }
        else if (currentStageNumber == 3)
        {
            if (skillName == "Shield")
                selectedSkill = SkillType.Shield;
            else if (skillName == "Dash")
                selectedSkill = SkillType.Dash;
            else
                selectedSkill = SkillType.None;
        }
    }

    public bool CanUseShield()
    {
        return selectedSkill == SkillType.Shield && !skillUsed;
    }

    public void UseShield()
    {
        skillUsed = true;

        if (SkillUIManager.Instance != null)
        {
            SkillUIManager.Instance.UpdateSkillUI();
        }
    }

    public void UseDash(PlayerController player)
    {
        if (selectedSkill != SkillType.Dash)
            return;

        if (skillUsed)
            return;

        skillUsed = true;

        if (SkillUIManager.Instance != null)
        {
            SkillUIManager.Instance.UpdateSkillUI();
        }

        player.Dash();
    }

    public bool IsSkillUsed()
    {
        return skillUsed;
    }

    public SkillType GetSelectedSkill()
    {
        return selectedSkill;
    }
}