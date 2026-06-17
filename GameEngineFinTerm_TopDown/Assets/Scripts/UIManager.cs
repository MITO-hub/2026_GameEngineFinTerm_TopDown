using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Option UI")]
    public Toggle bgmToggle;
    public Toggle fxToggle;

    public Button openButton;
    public Button closeButton;

    public Slider bgmSlider;
    public Slider fxSlider;

    public GameObject panel;

    [Header("Ranking UI")]
    public Button rankingOpenButton;
    public Button rankingCloseButton;

    public GameObject rankingPanel;

    public TMP_Text stage1RankText;
    public TMP_Text stage2RankText;
    public TMP_Text stage3RankText;

    [Header("Title UI")]
    public TMP_InputField playerNameInput;

    private void Awake()
    {
        if (bgmToggle != null)
            bgmToggle.onValueChanged.AddListener(OnBGMToggleChange);

        if (fxToggle != null)
            fxToggle.onValueChanged.AddListener(OnFXToggleChange);

        if (openButton != null)
            openButton.onClick.AddListener(OpenOptionPanel);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseOptionPanel);

        if (bgmSlider != null)
            bgmSlider.onValueChanged.AddListener(OnBGMSliderChange);

        if (fxSlider != null)
            fxSlider.onValueChanged.AddListener(OnFxSliderChange);

        if (rankingOpenButton != null)
            rankingOpenButton.onClick.AddListener(OpenRankingPanel);

        if (rankingCloseButton != null)
            rankingCloseButton.onClick.AddListener(CloseRankingPanel);
    }

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);

        if (rankingPanel != null)
            rankingPanel.SetActive(false);
    }

    private void OnBGMToggleChange(bool isOn)
    {
        // SoundManager_1.Instance.SetBgmOn(isOn);
    }

    private void OnFXToggleChange(bool isOn)
    {
        // SoundManager_1.Instance.SetSfxOn(isOn);
    }

    private void OnBGMSliderChange(float volume)
    {
        // SoundManager_1.Instance.SetBgmVolume(volume);
    }

    private void OnFxSliderChange(float volume)
    {
        // SoundManager_1.Instance.SetSfxVolume(volume);
    }

    private void OpenOptionPanel()
    {
        if (panel != null)
            panel.SetActive(true);
    }

    private void CloseOptionPanel()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    private void OpenRankingPanel()
    {
        if (rankingPanel != null)
            rankingPanel.SetActive(true);

        UpdateRankingUI();
    }

    private void CloseRankingPanel()
    {
        if (rankingPanel != null)
            rankingPanel.SetActive(false);
    }

    private void UpdateRankingUI()
    {
        PlayerSaveData data = SaveManager.Load();

        stage1RankText.text = data.playerName + " / Stage 1 최고 기록: " + FormatBestTime(data.stage1BestTime);
        stage2RankText.text = data.playerName + " / Stage 2 최고 기록: " + FormatBestTime(data.stage2BestTime);
        stage3RankText.text = data.playerName + " / Stage 3 최고 기록: " + FormatBestTime(data.stage3BestTime);
    }

    private string FormatBestTime(float time)
    {
        if (time <= 0f)
            return "기록 없음";

        return time.ToString("F2") + "초";
    }

    public void StartGame()
    {
        PlayerSaveData data = SaveManager.Load();

    string inputName = playerNameInput.text;

    if (string.IsNullOrWhiteSpace(inputName))
    {
        inputName = "Player";
    }

    data.playerName = inputName;

    SaveManager.Save(data);

    SceneManager.LoadScene("StageSelectScene");
    }
}