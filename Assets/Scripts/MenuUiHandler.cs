using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;      //unityeditor�� �����Ϳ����� �ҷ��ü����� ���� ���忡�� ������ #if�� �������
#endif
using TMPro;

public class MenuUiHandler : MonoBehaviour
{
    public int HighScore;
    public TextMeshProUGUI HighScoreUI;
    public TextMeshProUGUI InputNameUI;
    private string UserName;
    private string HighscoreUserName;

    private void Start()
    {
        MenuManager.Instance.LoadHighScore();
        UserName = MenuManager.Instance.SavedUserName;
        HighScore = MenuManager.Instance.HighScore;
        HighscoreUserName = MenuManager.Instance.SavedHScoreUserName;
        HighScoreUI.text = $"HighScore : {HighscoreUserName} : {HighScore}";

        InputNameUI.text = $"{UserName}";
    }

    public void StartButtonClicked()
    {
        MenuManager.Instance.SavedUserName = UserName;
        SceneManager.LoadScene(1);
    }
    public void QuitButtonClicked()
    {
        MenuManager.Instance.SavedUserName = UserName;
        MenuManager.Instance.SaveHighScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); //original code
#endif
    }
}
