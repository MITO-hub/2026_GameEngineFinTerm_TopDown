using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/saveData.json";

    public static void Save(PlayerSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("저장 완료: " + path);
    }

    public static PlayerSaveData Load()
    {
        if (!File.Exists(path))
        {
            Debug.Log("저장 파일 없음. 새 데이터 생성");
            return new PlayerSaveData();
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerSaveData>(json);
    }
}
