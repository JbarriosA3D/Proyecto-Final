using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject settingsPanel;

    private void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("CharacterSelector");
    }
    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
    }
    public void QuitButton()
    {
        Debug.Log("Has salido");
        Application.Quit();
    }
    public void GoBack()
    {
        settingsPanel.SetActive(false);
    }
}
