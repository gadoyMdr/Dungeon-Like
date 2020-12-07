using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void GoLevelScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void GoOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void GoLoadNewParty()
    {
        SceneManager.LoadScene("LoadNewParty");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
