using UnityEngine;

public class StageManager : MonoBehaviour
{
    public LevelData levelData;

    public GameObject player;
    public GameObject goalPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupStage();
    }

    private void SetupStage()
    {
        if (levelData == null)
        {
            Debug.LogError("LevelData가 연결되지 않았습니다.");
            return;
        }

        if (player != null)
        {
            player.transform.position = levelData.playerStartPosition;
        }

        if (goalPrefab != null)
        {
            Instantiate(goalPrefab, levelData.goalPosition, Quaternion.identity);
        }

        Debug.Log(levelData.stageName + " 시작");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
