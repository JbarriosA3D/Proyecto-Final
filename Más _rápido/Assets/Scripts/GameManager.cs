using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int selectedCharacter;
    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        Debug.Log("Personaje elegido: "+selectedCharacter);
    }
}