using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMaster : MonoBehaviour
{
    #region Singleton
    public static GameMaster instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one instance of GameMaster!");
            return;
        }

        instance = this;
    }
    #endregion

    private bool _isGameOver;
    private bool _isSinglePlayer;

    private void Start()
    {
        _isGameOver = false;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex == (int)Scenes.SinglePlayer)
        {
            _isSinglePlayer = true;
        }
        else
        {
            _isSinglePlayer = false;
        }
    }

    private void Update()
    {
        if (_isGameOver)
        {
            CheckRestartLevelKey();
        }

        CheckApplicationQuit();
    }

    private void CheckRestartLevelKey()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }

    private void CheckApplicationQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SetIsGameOver(bool isGameOver)
    {
        _isGameOver = isGameOver;
    }

    public bool GetIsSinglePlayer()
    {
        return _isSinglePlayer;
    }
}
