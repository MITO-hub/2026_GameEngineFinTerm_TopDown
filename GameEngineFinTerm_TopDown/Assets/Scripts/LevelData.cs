using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public string stageName;

    public int stageNumber;

    public int coinCount;
    public int obstacleCount;

    public float timeLimit;

    public Vector2 playerStartPosition;
    public Vector2 goalPosition;
}