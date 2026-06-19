using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RankingManager : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public GameObject helpPanel;
    public GameObject recordPanel;

    public TMP_Text recordText;

    public string stageSelectSceneName = "StageSelectScene";

    private PlayerSaveData saveData;

    private void Start()
    {
        Time.timeScale = 1f;

        string currentPlayerName = PlayerPrefs.GetString("CurrentPlayerName", "");

        if (nameInputField != null)
        {
            nameInputField.text = currentPlayerName;
        }

        if (helpPanel != null)
            helpPanel.SetActive(false);

        if (recordPanel != null)
            recordPanel.SetActive(false);
    }

    public void StartGame()
    {
        string playerName = "Player";

        if (nameInputField != null && !string.IsNullOrWhiteSpace(nameInputField.text))
        {
            playerName = nameInputField.text;
        }

        PlayerPrefs.SetString("CurrentPlayerName", playerName);
        PlayerPrefs.Save();

        saveData = SaveManager.Load(playerName);
        saveData.playerName = playerName;
        SaveManager.Save(saveData);

        SceneManager.LoadScene(stageSelectSceneName);
    }

    public void OpenHelpPanel()
    {
        if (helpPanel != null)
            helpPanel.SetActive(true);
    }

    public void CloseHelpPanel()
    {
        if (helpPanel != null)
            helpPanel.SetActive(false);
    }

    public void OpenRecordPanel()
    {
        string playerName = GetCurrentInputName();

        saveData = SaveManager.Load(playerName);

        if (recordPanel != null)
            recordPanel.SetActive(true);

        if (recordText != null)
        {
            recordText.text =
                "플레이어 이름: " + saveData.playerName + "\n\n" +
                "쉬움 스테이지 최고 기록: " + GetTimeText(saveData.stage1BestTime) + "\n" +
                "보통 스테이지 최고 기록: " + GetTimeText(saveData.stage2BestTime) + "\n" +
                "어려움 스테이지 최고 기록: " + GetTimeText(saveData.stage3BestTime);
        }
    }

    public void CloseRecordPanel()
    {
        if (recordPanel != null)
            recordPanel.SetActive(false);
    }

    private string GetCurrentInputName()
    {
        if (nameInputField != null && !string.IsNullOrWhiteSpace(nameInputField.text))
        {
            return nameInputField.text;
        }

        return PlayerPrefs.GetString("CurrentPlayerName", "Player");
    }

    private string GetTimeText(float time)
    {
        if (time <= 0f)
            return "기록 없음";

        return time.ToString("F2") + "초";
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}