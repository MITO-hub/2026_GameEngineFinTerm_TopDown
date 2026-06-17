[System.Serializable]
public class PlayerSaveData
{
    public string playerName = "Player";

    public float stage1BestTime = 0f;
    public float stage2BestTime = 0f;
    public float stage3BestTime = 0f;

    public bool stage1Cleared = false;
    public bool stage2Cleared = false;
    public bool stage3Cleared = false;
}