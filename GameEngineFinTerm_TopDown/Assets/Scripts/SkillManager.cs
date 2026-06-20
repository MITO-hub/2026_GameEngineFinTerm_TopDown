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

        if (skillName == "Shield")
            selectedSkill = SkillType.Shield;
        else if (skillName == "Dash")
            selectedSkill = SkillType.Dash;
        else
            selectedSkill = SkillType.None;
    }

    public bool CanUseShield()
    {
        return selectedSkill == SkillType.Shield && !skillUsed;
    }

    public void UseShield()
    {
        skillUsed = true;
        Debug.Log("보호막 사용됨");
    }

    public void UseDash(PlayerController player)
    {
        if (selectedSkill != SkillType.Dash)
            return;

        if (skillUsed)
            return;

        skillUsed = true;

        player.Dash();
        Debug.Log("대시 사용됨");
    }
}