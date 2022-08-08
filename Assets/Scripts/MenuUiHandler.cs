using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;      //unityeditor는 에디터에서만 불러올수있음 독립 빌드에서 사용못함 #if문 써줘야함
#endif
using TMPro;

public class MenuUiHandler : MonoBehaviour
{
    public int HighScore;
    public TextMeshProUGUI HighScoreUI;
    public TMP_InputField InputNameUI;
    private string UserName;
    private string HighscoreUserName;

    private void Start()
    {
        MenuManager.Instance.LoadHighScore();
        UserName = MenuManager.Instance.SavedUserName;
        HighScore = MenuManager.Instance.HighScore;
        HighscoreUserName = MenuManager.Instance.SavedHScoreUserName;
        Debug.Log("SavedHScoreUserName :/"+MenuManager.Instance.SavedHScoreUserName+"/");
        HighScoreUI.text = $"HighScore : {HighscoreUserName} : {HighScore}";

        Debug.Log("before InputNameUI.text :/" + InputNameUI.text + "/");
        InputNameUI.text = $"{UserName}";
        Debug.Log("after InputNameUI.text :/" + InputNameUI.text + "/");
    }

    public void StartButtonClicked()
    {
        MenuManager.Instance.SavedUserName = InputNameUI.text;
        Debug.Log("StartButtonClicked InputNameUI.text :/" + InputNameUI.text + "/");
        SceneManager.LoadScene(1);
    }
    public void QuitButtonClicked()
    {
        MenuManager.Instance.SavedUserName = InputNameUI.text;
        MenuManager.Instance.SaveHighScore(true);
        Debug.Log("QuitButtonClicked InputNameUI.text :/" + InputNameUI.text + "/");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); //original code
#endif
    }
}
