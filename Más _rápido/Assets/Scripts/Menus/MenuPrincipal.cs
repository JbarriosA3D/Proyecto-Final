using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//                                        ******IMPORTANTE****** 
//*******PONER DEBAJO DE ESTO FECHA Y HORA DE LA ÚLTIMA EDICIÓN, PARA EVITAR CONFUSIONES. GRACIAS :D*****
//                                       ******<25/11 - 2:09>******
//P.D: Si no lo poneis, ¡HAY TABLA!

public class MenuPrincipal : MonoBehaviour
{

    //Esta es una variable para meter dentro el panel que contiene toda la informacion de los ajustes
    public GameObject settingsPanel;

    private void Start()
    {
        //Le decimos que empiece con el panel de ajsutes oculto porque solo queremos que se vea cuando el jugador le de al botón de ajustes
        settingsPanel.SetActive(false);
    }

    //Función para el botón de Jugar
    public void PlayButton()
    {
        SceneManager.LoadScene("CharacterSelector");
    }

    //Función para el botón de ajustes
    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
    }

    //Función para el botón de salir del juego
    public void QuitButton()
    {
        Debug.Log("Has salido");
        Application.Quit();
    }

    //Función para el botón de salir de los ajustes
    public void GoBack()
    {
        settingsPanel.SetActive(false);
    }
}
