using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;      //unityeditor�� �����Ϳ����� �ҷ��ü����� ���� ���忡�� ������ #if�� �������
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;         //singleton

    public int HighScore;

    public TMPro.TMP_Text HighScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
        HighScoreText.text = "High Score : " + HighScore; 
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;

    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.HighScore = HighScore;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
        }

    }

    public void StartButtonClicked()
    {
        SceneManager.LoadScene("main");
    }
    public void QuitButtonClicked()
    {
        SaveHighScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); //original code
#endif
    }
}
