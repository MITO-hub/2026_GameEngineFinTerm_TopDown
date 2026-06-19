using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string GetPath(string playerName)
    {
        string safeName = playerName.Replace(" ", "_");
        return Application.persistentDataPath + "/saveData_" + safeName + ".json";
    }

    public static void Save(PlayerSaveData data)
    {
        string path = GetPath(data.playerName);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("저장 완료: " + path);
    }

    public static PlayerSaveData Load(string playerName)
    {
        string path = GetPath(playerName);

        if (!File.Exists(path))
        {
            Debug.Log("저장 파일 없음. 새 데이터 생성");

            PlayerSaveData newData = new PlayerSaveData();
            newData.playerName = playerName;
            return newData;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerSaveData>(json);
    }
}
