using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;      //unityeditor는 에디터에서만 불러올수있음 독립 빌드에서 사용못함 #if문 써줘야함
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
            if (HighScore > dataOld.HighScore)                  //기존 스코어보다 새로운 스코어가 더높으면 높은값으로 데이터 변경
            {
                dataNew.HighScore = HighScore;
                dataNew.SavedHScoreUserName = SavedUserName;
                dataNew.SavedUserName = SavedUserName;

                json = JsonUtility.ToJson(dataNew);
                File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            }else if (HighScore <= dataOld.HighScore)          //하이스코어 갱신 실패시 이전 세이브에서 최고점,최고점이름 복사
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
        if (File.Exists(path))                                  //세이브파일이 존재하면
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            SavedUserName = data.SavedUserName;                 //이전 세션 입력된 닉네임 로드
            SavedHScoreUserName = data.SavedHScoreUserName;     //이전 세션 최고점 닉네임로드
            HighScore = data.HighScore;                         //이전 세션 최고점 로드
        }

    }
}
