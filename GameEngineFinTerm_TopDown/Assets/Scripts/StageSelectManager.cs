using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    [Header("Stage Buttons")]
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    [Header("Lock Text")]
    public TMP_Text normalLockText;
    public TMP_Text hardLockText;

    private PlayerSaveData saveData;

    private void Start()
    {
        saveData = SaveManager.Load();

        UpdateStageButtons();
    }

    private void UpdateStageButtons()
    {
        // Easy는 항상 플레이 가능
        easyButton.interactable = true;

        // Normal은 Easy 클리어 후 가능
        normalButton.interactable = saveData.stage1Cleared;

        // Hard는 Normal 클리어 후 가능
        hardButton.interactable = saveData.stage2Cleared;

        if (normalLockText != null)
        {
            normalLockText.gameObject.SetActive(!saveData.stage1Cleared);
            normalLockText.text = "쉬움 스테이지 클리어 필요";
        }

        if (hardLockText != null)
        {
            hardLockText.gameObject.SetActive(!saveData.stage2Cleared);
            hardLockText.text = "보통 스테이지 클리어 필요";
        }
    }

    public void LoadEasyStage()
    {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void LoadNormalStage()
    {
        if (!saveData.stage1Cleared)
            return;

        SceneManager.LoadScene("Stage2Scene");
    }

    public void LoadHardStage()
    {
        if (!saveData.stage2Cleared)
            return;

        SceneManager.LoadScene("Stage3Scene");
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}