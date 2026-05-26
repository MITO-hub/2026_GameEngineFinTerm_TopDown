using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text timeText;
    public TMP_Text coinText;

    private int coinCount = 0;
    private float playTime = 0f;
    private bool isPlaying = true;

    public TMP_InputField inputField;
    public Button gameStartButton;


    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;                                                //
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameStartButton.onClick.AddListener(OnGameStartButtonClicked);

        UpdateCoinUI();
        UpdateCoinUI();
    }

    void Update()
    {
        if (!isPlaying)
            return;

        playTime += Time.deltaTime;
        UpdateTimeUI();
    }

    private void UpdateTimeUI()
    {
        if (timeText != null)
        {
            timeText.text = "Time: " + playTime.ToString("F2");
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coin: " + coinCount;
    }

    private void OnGameStartButtonClicked()
    {
        string playerName = inputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("플레이어 이름을 입력하세요.");
            return;
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        Debug.Log("플레이어 이름 저장됨: " + playerName);

        SceneManager.LoadScene("Level_1");
    }

    public void ClearStage()
    {
        isPlaying = false;

        PlayerController player = FindObjectOfType<PlayerController>();     //
        if (player != null)                                                 //
        {
            player.SetCanMove(false);                                       //
        }

        Time.timeScale = 0f;

        Debug.Log("스테이지 클리어!");
        Debug.Log("클리어 시간: " + playTime.ToString("F2") + "초");
        Debug.Log("획득 코인: " + coinCount);
    }

    public void PlayerDead()
    {
        isPlaying = false;

        Time.timeScale = 1f;                                                //

        Debug.Log("플레이어 사망. 스테이지 재시작");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float GetPlayTime()
    {
        return playTime;
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        UpdateCoinUI();
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
