using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSelectManager : MonoBehaviour
{
    public GameObject skillPanel;

    public Button shieldButton;
    public Button dashButton;
    public Button closeButton;

    public TMP_Text shieldLockText;
    public TMP_Text dashLockText;

    private PlayerSaveData saveData;

    private void Start()
    {
        if (skillPanel != null)
            skillPanel.SetActive(false);

        if (shieldButton != null)
            shieldButton.onClick.AddListener(SelectShield);

        if (dashButton != null)
            dashButton.onClick.AddListener(SelectDash);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseSkillPanel);
    }

    public void OpenSkillPanel()
    {
        string playerName = PlayerPrefs.GetString("CurrentPlayerName", "Player");
        saveData = SaveManager.Load(playerName);

        if (skillPanel != null)
            skillPanel.SetActive(true);

        UpdateSkillButtons();
    }

    public void CloseSkillPanel()
    {
        if (skillPanel != null)
            skillPanel.SetActive(false);
    }

    private void UpdateSkillButtons()
    {
        shieldButton.interactable = saveData.shieldUnlocked;
        dashButton.interactable = saveData.dashUnlocked;

        if (shieldLockText != null)
            shieldLockText.gameObject.SetActive(!saveData.shieldUnlocked);

        if (dashLockText != null)
            dashLockText.gameObject.SetActive(!saveData.dashUnlocked);
    }

    private void SelectShield()
    {
        PlayerPrefs.SetString("SelectedSkill", "Shield");
        PlayerPrefs.Save();

        Debug.Log("보호막 스킬 선택");
    }

    private void SelectDash()
    {
        PlayerPrefs.SetString("SelectedSkill", "Dash");
        PlayerPrefs.Save();

        Debug.Log("대시 스킬 선택");
    }
}