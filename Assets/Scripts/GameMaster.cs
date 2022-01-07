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

    private void Start()
    {
        _isGameOver = false;
    }

    private void Update()
    {
        if (_isGameOver)
        {
            CheckRestartLevelKey();
        }
    }

    private void CheckRestartLevelKey()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(((int)Scenes.Game));
        }
    }

    public void SetIsGameOver(bool isGameOver)
    {
        _isGameOver = isGameOver;
    }
}
