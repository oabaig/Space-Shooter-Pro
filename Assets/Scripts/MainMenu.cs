using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayer()
    {
        SceneManager.LoadScene((int)Scenes.SinglePlayer); // Load Game Scene
    }

    public void LoadMultiplayer()
    {
        SceneManager.LoadScene((int)Scenes.Multiplayer);
    }
}
