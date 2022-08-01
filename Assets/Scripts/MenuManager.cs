using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;      //unityeditor�� �����Ϳ����� �ҷ��ü����� ���� ���忡�� ������ #if�� �������
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;         //singleton

    public int HighScore;
    public string SavedUserName;
    public string SavedHScoreUserName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LoadHighScore();

        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string SavedHScoreUserName;
        public string SavedUserName;
    }

    public void SaveHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (HighScore > 0)
        {
            string json = File.ReadAllText(path);
            SaveData dataOld = JsonUtility.FromJson<SaveData>(json);
            SaveData dataNew = new SaveData();
            if (HighScore > dataOld.HighScore)                  //���� ���ھ�� ���ο� ���ھ �������� ���������� ������ ����
            {
                dataNew.HighScore = HighScore;
                dataNew.SavedHScoreUserName = SavedUserName;
                dataNew.SavedUserName = SavedUserName;

                json = JsonUtility.ToJson(dataNew);
                File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            }else if (HighScore <= dataOld.HighScore)          //���̽��ھ� ���� ���н� ���� ���̺꿡�� �ְ���,�ְ����̸� ����
            {
                dataNew.SavedHScoreUserName = dataOld.SavedHScoreUserName;
                dataNew.SavedUserName = SavedUserName;
                dataNew.HighScore = dataOld.HighScore;
                json = JsonUtility.ToJson(dataNew);
                File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            }
        }
        
        else { return; }
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))                                  //���̺������� �����ϸ�
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            SavedUserName = data.SavedUserName;                 //���� ���� �Էµ� �г��� �ε�
            SavedHScoreUserName = data.SavedHScoreUserName;     //���� ���� �ְ��� �г��ӷε�
            HighScore = data.HighScore;                         //���� ���� �ְ��� �ε�
        }

    }
}
