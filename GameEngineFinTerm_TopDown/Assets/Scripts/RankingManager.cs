using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public GameObject helpPanel;
    public GameObject recordPanel;

    public TMP_Text recordText;

    public string gameSceneName = "GameScene";

    private PlayerSaveData saveData;

    private void Start()
    {
        Time.timeScale = 1f;

        saveData = SaveManager.Load();

        if (nameInputField != null)
        {
            nameInputField.text = saveData.playerName;
        }

        if (helpPanel != null)
            helpPanel.SetActive(false);

        if (recordPanel != null)
            recordPanel.SetActive(false);
    }

    public void StartGame()
    {
        if (nameInputField != null && nameInputField.text != "")
        {
            saveData.playerName = nameInputField.text;
        }
        else
        {
            saveData.playerName = "Player";
        }

        SaveManager.Save(saveData);

        SceneManager.LoadScene(gameSceneName);
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
        saveData = SaveManager.Load();

        if (recordPanel != null)
            recordPanel.SetActive(true);

        if (recordText != null)
        {
            recordText.text =
                "플레이어 이름: " + saveData.playerName + "\n\n" +
                "1스테이지 최고 기록: " + GetTimeText(saveData.stage1BestTime) + "\n" +
                "2스테이지 최고 기록: " + GetTimeText(saveData.stage2BestTime) + "\n" +
                "3스테이지 최고 기록: " + GetTimeText(saveData.stage3BestTime);
        }
    }

    public void CloseRecordPanel()
    {
        if (recordPanel != null)
            recordPanel.SetActive(false);
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