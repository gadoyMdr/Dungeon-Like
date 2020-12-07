using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI musicVolumeText;

    [SerializeField]
    private TextMeshProUGUI otherVolumeText;
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnMusicVolumeChanged(float value)
    {
        musicVolumeText.text = "music volume : " + value;
    }

    public void OnOtherVolumeChanged(float value)
    {
        otherVolumeText.text = "music volume : " + value;
    }
}
