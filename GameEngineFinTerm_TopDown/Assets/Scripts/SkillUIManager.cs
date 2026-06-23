using TMPro;
using UnityEngine;

public class SkillUIManager : MonoBehaviour
{
    public TMP_Text skillNameText;
    public TMP_Text skillCountText;

    public static SkillUIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateSkillUI();
    }

    public void UpdateSkillUI()
    {
        if (SkillManager.Instance == null)
        {
            skillNameText.text = "현재 스킬 : 없음";
            skillCountText.text = "남은 횟수 : 0";
            return;
        }

        SkillType selectedSkill = SkillManager.Instance.GetSelectedSkill();
        bool skillUsed = SkillManager.Instance.IsSkillUsed();

        if (selectedSkill == SkillType.Shield)
        {
            skillNameText.text = "현재 스킬 : 보호막";
        }
        else if (selectedSkill == SkillType.Dash)
        {
            skillNameText.text = "현재 스킬 : 대시";
        }
        else
        {
            skillNameText.text = "현재 스킬 : 없음";
            skillCountText.text = "남은 횟수 : 0";
            return;
        }

        if (skillUsed)
            skillCountText.text = "사용 완료";
        else
            skillCountText.text = "남은 횟수 : 1";
    }
}