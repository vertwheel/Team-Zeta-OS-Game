using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene1 1");
    }

    public void PlayGame2()
    {
        SceneManager.LoadScene("GameScene1 2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
