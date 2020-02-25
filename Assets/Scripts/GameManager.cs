using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region Unity_Functions
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_Transitions
    public void StartGame()
    {
        Debug.Log("game started");
        SceneManager.LoadScene("SampleScene");

    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void GoToMainMenue()
    {
        SceneManager.LoadScene("MainMenue");
    }

    #endregion
}
