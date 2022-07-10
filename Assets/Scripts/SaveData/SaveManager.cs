using System.IO;
using UnityEngine;

//����� ��������������� ������ ��� ���������� ��������� ����� ������
public static class SaveManager
{
    //����� ����������� ������ � ������ �� �����
    //���������: ����� ������
    public static void SavePP<T>(string key, T saveData)
    {
        //��������� ������ � ������ ������� Json
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        //��������� � ������ ��� ������������ ������
        PlayerPrefs.SetString(key, jsonDataString);
    }

    //����� ��� �������� ������ �� ������� �� �����
    //���������: ����
    public static T LoadPP<T>(string key) where T: new()
    {
        //���� � ������� ���� ������ ��� ����� ������
        if (PlayerPrefs.HasKey(key))
        {
            //��������� ������ �� ����� �� �������
            string loadedString = PlayerPrefs.GetString(key);
            //��������� ������ json  � ������ ������ ������ 
            return JsonUtility.FromJson<T>(loadedString);
        }
        else 
        {
            //����� ������ ����������� ������
            return new T();
        }
    }

    //����� ��� �������� ������ �� ������� �� �����
    //���������: ����
    public static bool FindPP(string key) 
    {
        //���� � ������� ���� ������ ��� ����� ������
        return PlayerPrefs.HasKey(key);
    }

    //����� ��� ���������� ������ � ���� ������� json 
    //���������: ������, ��� ����� ��� ����������
    public static void SaveJson<T>(T saveData, string name)
    {
        string path; //���� � ����� ���������� �����
        //��������� ����� � ����� � ������ �����
#if UNITY_ANDROID && !UNITY_EDITOR 
        path = Path.Combine(Application.persistentDataPath, name + ".json"); //���� ����������� �� �������
#else
        path = Path.Combine(Application.dataPath, name + ".json"); //���� ����������� �� ������ ��������� ��� � ����� ����������
#endif
        File.WriteAllText(path, JsonUtility.ToJson(saveData)); //���������� ������ � ���� ������� ���� json
    }

    //����� ��� ������ ������ �� ����� ���� json
    //���������: ��� �����
    public static T LoadJson<T>(string name) where T : new()
    {
        string path;//������ ���� � �����
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, name + ".json"); //���� �� ��������
#else
        path = Path.Combine(Application.dataPath, name + ".json");//���� �� ������ ���������
#endif
        //���� ���� � ����� ��������� � �����
        if (File.Exists(path))
        {
            //��������� ����� ����� � ��������� json ������� � ������ ������
            return JsonUtility.FromJson<T>(File.ReadAllText(path)); 
        }
        else 
        {
            return new T(); //������ ����������� ���� �� �����
        }
    }

    //������ ��� ������ ����� ���� json �� ��������
    //���������: �������� �����
    public static bool FindJson<T>(string name)
    {
        string path;//������ ���� � �����
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, name + ".json"); //���� �������
#else
        path = Path.Combine(Application.dataPath, name + ".json"); //���� ������ ���������
#endif
        //���� ���� �� �������� � ������ �����
        if (File.Exists(path))
        {
            return true;//���� ������
        }
        else
        {
            return false;//���� �� ������
        }
    }

    //����� ��� �������� ���������� ������� ����
    //���������: ��� �����
    public static T LoadTextAssetJson<T>(string name)
    {
        var levelTextAsset = Resources.Load<TextAsset>(name);//�������� �������
        //��������� ������ �� ������� � ������������������ �� json � ������ ��� ������
        return JsonUtility.FromJson<T>(levelTextAsset.text);
    }

    //����� ��� ��������� ������ � ������� ����
    //���������: ������, ������� � �����, ��� �����
    public static void SaveTextAssetJson<T>(T saveData, string subpath, string name)
    {
        string path;//������ ���� � �����
        path = Path.Combine(Application.dataPath + subpath, name + ".json");//�������� ������ ����
        //��������� � ������ ����� ������ ����� ����� ������� jspn
        File.WriteAllText(path, JsonUtility.ToJson(saveData));
    }



    //����� ��� �������� ���������� ������� ����
    //���������: ��� �����
    public static string[][] LoadTextAssetCSV(string name)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(name);//�������� �������
        //��������� ������ �� ������� � ������������������ �� CSV � ������ ��� ������
        string[] lines = textAsset.text.Split('\n');
        string[][] resultTable = new string[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            resultTable[i] = new string[lines[i].Length];
            resultTable[i] = lines[i].Split(',');
        }
        return resultTable;
    }
}
