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

    [Header("Skill UI")]
    public Button skillOpenButton;
    public Button skillCloseButton;

    public GameObject skillPanel;

    public Button shieldSkillButton;
    public Button dashSkillButton;

    public TMP_Text shieldLockText;
    public TMP_Text dashLockText;
    public TMP_Text selectedSkillText;

    private PlayerSaveData saveData;

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

        if (skillOpenButton != null)
            skillOpenButton.onClick.AddListener(OpenSkillPanel);

        if (skillCloseButton != null)
            skillCloseButton.onClick.AddListener(CloseSkillPanel);

        if (shieldSkillButton != null)
            shieldSkillButton.onClick.AddListener(SelectShieldSkill);

        if (dashSkillButton != null)
            dashSkillButton.onClick.AddListener(SelectDashSkill);
    }

    private void Start()
    {
        if (SoundManager.Instance != null)
        {
            bgmToggle.isOn = SoundManager.Instance.GetBGMOn();
            fxToggle.isOn = SoundManager.Instance.GetSFXOn();

            bgmSlider.value = SoundManager.Instance.GetBGMVolume();
            fxSlider.value = SoundManager.Instance.GetSFXVolume();
        }

        if (panel != null)
            panel.SetActive(false);

        if (skillPanel != null)
            skillPanel.SetActive(false);
    }

    private void OnBGMToggleChange(bool isOn)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetBGMOn(isOn);
            SoundManager.Instance.PlaySFX(SfxType.Click);
        }
    }

    private void OnFXToggleChange(bool isOn)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetSFXOn(isOn);
            SoundManager.Instance.PlaySFX(SfxType.Click);
        }
    }

    private void OnBGMSliderChange(float volume)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetBGMVolume(volume);
        }
    }

    private void OnFxSliderChange(float volume)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetSFXVolume(volume);
        }
    }

    private void OpenOptionPanel()
    {
        panel.SetActive(true);

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SfxType.Click);
    }

    private void CloseOptionPanel()
    {
        panel.SetActive(false);

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SfxType.Click);
    }

    private void OpenRankingPanel()
    {
        if (rankingPanel != null)
            rankingPanel.SetActive(true);

        UpdateRankingUI();

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SfxType.Click);
    }

    private void CloseRankingPanel()
    {
        if (rankingPanel != null)
            rankingPanel.SetActive(false);

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SfxType.Click);
    }

    private void UpdateRankingUI()
    {
        string playerName = PlayerPrefs.GetString("CurrentPlayerName", "Player");

        PlayerSaveData data = SaveManager.Load(playerName);

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
        string inputName = playerNameInput.text;

        if (string.IsNullOrWhiteSpace(inputName))
        {
            Debug.Log("플레이어 이름을 입력하세요.");
            return;
        }

        PlayerPrefs.SetString("CurrentPlayerName", inputName);
        PlayerPrefs.Save();

        PlayerSaveData data = SaveManager.Load(inputName);
        data.playerName = inputName;
        SaveManager.Save(data);

        SceneManager.LoadScene("StageSelectScene");

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SfxType.Click);
    }

    private void OpenSkillPanel()
    {
        string playerName = PlayerPrefs.GetString("CurrentPlayerName", "Player");
        saveData = SaveManager.Load(playerName);

        if (skillPanel != null)
            skillPanel.SetActive(true);

        UpdateSkillUI();

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SfxType.Click);
    }

    private void CloseSkillPanel()
    {
        if (skillPanel != null)
            skillPanel.SetActive(false);

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SfxType.Click);
    }

    private void UpdateSkillUI()
    {
        if (saveData == null)
            return;

        if (shieldSkillButton != null)
            shieldSkillButton.interactable = saveData.shieldUnlocked;

        if (dashSkillButton != null)
            dashSkillButton.interactable = saveData.dashUnlocked;

        if (shieldLockText != null)
        {
            shieldLockText.gameObject.SetActive(!saveData.shieldUnlocked);
            shieldLockText.text = "쉬움 클리어 필요";
        }

        if (dashLockText != null)
        {
            dashLockText.gameObject.SetActive(!saveData.dashUnlocked);
            dashLockText.text = "보통 클리어 필요";
        }

        UpdateSelectedSkillText();
    }

    private void SelectShieldSkill()
    {
        if (saveData == null || !saveData.shieldUnlocked)
            return;

        PlayerPrefs.SetString("SelectedSkill", "Shield");
        PlayerPrefs.Save();

        UpdateSelectedSkillText();

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SfxType.Click);

        Debug.Log("보호막 스킬 선택됨");
    }

    private void SelectDashSkill()
    {
        if (saveData == null || !saveData.dashUnlocked)
            return;

        PlayerPrefs.SetString("SelectedSkill", "Dash");
        PlayerPrefs.Save();

        UpdateSelectedSkillText();

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(SfxType.Click);

        Debug.Log("대시 스킬 선택됨");
    }

    private void UpdateSelectedSkillText()
    {
        if (selectedSkillText == null)
            return;

        string selectedSkill = PlayerPrefs.GetString("SelectedSkill", "None");

        if (selectedSkill == "Shield")
            selectedSkillText.text = "선택된 스킬: 보호막";
        else if (selectedSkill == "Dash")
            selectedSkillText.text = "선택된 스킬: 대시";
        else
            selectedSkillText.text = "선택된 스킬: 없음";
    }
}