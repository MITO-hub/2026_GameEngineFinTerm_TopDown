using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public LevelData levelData;

    public GameObject player;

    public GameObject goalPrefab;
    public GameObject coinPrefab;
    public GameObject obstaclePrefab;

    public Transform[] spawnPoints;

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

        SpawnObjects();

        Debug.Log(levelData.stageName + " 시작");
    }

    private void SpawnObjects()
    {
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        SpawnRandomObjects(coinPrefab, levelData.coinCount, availablePoints);
        SpawnRandomObjects(obstaclePrefab, levelData.obstacleCount, availablePoints);
    }

    private void SpawnRandomObjects(GameObject prefab, int count, List<Transform> availablePoints)
    {
        if (prefab == null)
        {
            Debug.LogWarning("생성할 프리팹이 연결되지 않았습니다.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            if (availablePoints.Count <= 0)
            {
                Debug.LogWarning("사용 가능한 SpawnPoint가 부족합니다.");
                return;
            }

            int randomIndex = Random.Range(0, availablePoints.Count);
            Transform selectedPoint = availablePoints[randomIndex];

            Instantiate(prefab, selectedPoint.position, Quaternion.identity);

            availablePoints.RemoveAt(randomIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
