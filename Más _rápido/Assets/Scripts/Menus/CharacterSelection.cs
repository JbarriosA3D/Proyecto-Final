using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

//                                        ******IMPORTANTE****** 
//*******PONER DEBAJO DE ESTO FECHA Y HORA DE LA ÚLTIMA EDICIÓN, PARA EVITAR CONFUSIONES. GRACIAS :D*****
//                                       ******<27/11 - 3:47>******
//P.D: Si no lo poneis, ¡HAY TABLA!
public class CharacterSelection : MonoBehaviour
{
    //Rotación de los personajes
    Vector3 targetRot;
    Vector3 currentAngle;
    int currentCharacter;
    public float rotSpeed = 1.0f;

    //Escalar el personaje que esta delante
    public Transform[] charactersScale;
    public float selectedScale = 1.5f;
    public float normalScale = 0.7f;
    public float scaleSpeed = 5f;

    //Botónes
    bool pressRight = false;
    bool pressLeft = false;

    //Cambio de interfaces
    bool backIndicator = false;
    bool rotateIndicator = false;
    public Transform camera;
    public GameObject selectorCanvas;
    public GameObject infoPanel;
    Vector3 cameraTargetRot;
    Vector3 cameraOriginRot;
    public float cameraRotSpeed = 1.0f;

    private void Start()
    {
        //SI LLEGAMOS A AÑADIR MAS PERSONAJES,TENDREMOS QUE CAMBIAR ESTO POR UN BUCLE PORQUE ME DA 
        //MUUUUUUUUUUUUUUUUUCHA PEREZA HACERLO AHORA. QUE SE JODA QUIEN SEA QUE LO ESTE EDITANDO EN EL FUTURO ;)

        //Ponemos para que al empezar, solo el personaje 0, osea el primero, sea grande y el resto esten pequeños, es lo mismo de abajo pero sin bucle
        charactersScale[0].localScale = new Vector3(1, 1, 1) * selectedScale;
        charactersScale[1].localScale = new Vector3(1, 1, 1) * normalScale;
        charactersScale[2].localScale = new Vector3(1, 1, 1) * normalScale;
        charactersScale[3].localScale = new Vector3(1, 1, 1) * normalScale;

        //Ponemos que currentCharacter equivalga a 0, osea, el primero perosnaje
        currentCharacter = 0;
        //Solo para comprobar que se esta cambiando bien el currentCharacter
        Debug.Log(currentCharacter);

        //Guardar la posicion original de la cámara
        cameraOriginRot = camera.eulerAngles;
        //cameraTargetRot = new Vector3(0, 180, 0);
    }


    //Creamos las dos funciones de los botones para cambiar de personaje, Right & Left
    public void Right()
    {
        pressRight = true;
        currentCharacter++;
        if (currentCharacter > 3)
        {
            currentCharacter = 0;
        }
        Debug.Log(currentCharacter);
    }
    public void Left()
    {
        pressLeft = true;
        currentCharacter--;
        if (currentCharacter <= -1)
        {
            currentCharacter = 3;
        }
        Debug.Log(currentCharacter);
    }

    //Creamos la funcion para confirmar la selección de personaje
    public void Selection()
    {
        //comprobar
        Debug.Log(currentCharacter);

        //Desactivar canvas de selector
        selectorCanvas.SetActive(false);

        //Rotar cámara
        rotateIndicator = true;

        //Activar info
        infoPanel.SetActive(true);
        cameraTargetRot = new Vector3(0, 180, 0);


    }
    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt("selectedCharacter", currentCharacter);
        SceneManager.LoadScene("GAME");
    }

    public void ComeBack()
    {
        backIndicator = true;
        rotateIndicator = false;
        cameraTargetRot = cameraOriginRot;

        infoPanel.SetActive(false);
        selectorCanvas.SetActive(true);
    }

    //Para hacer el movimiento con las flechas en lugar de click remplazamos dentro del if lo que hay dentro y ponemos esto:
    //Input.GetKeyDown(KeyCode.LeftArrow) && currentCharacter>1
    //Input.GetKeyDown(KeyCode.RightArrow) && currentCharacter<totalCharacters
    private void Update()
    {
        //Rotar los personajes
        if (pressRight)
        {
            pressRight = false;
            currentAngle = transform.eulerAngles;
            targetRot = targetRot + new Vector3(0, 90, 0);
        }
        if (pressLeft)
        {
            pressLeft = false;
            currentAngle = transform.eulerAngles;
            targetRot = targetRot - new Vector3(0, 90, 0);
        }
        currentAngle = new Vector3(0, Mathf.LerpAngle(currentAngle.y, targetRot.y, Time.deltaTime * rotSpeed), 0);
        transform.eulerAngles = currentAngle;

        //Creamos un bucle para escalar o deescdalar los personajes. Esto comprobará personaje por personaje y verá si coincide con el current character
        //Si coincide, lo agrandará, si no, lo dejará en pequeño
        for (int i = 0; i < charactersScale.Length; i++)
        {
            if (i == currentCharacter) 
            {
                charactersScale[i].localScale = Vector3.Lerp(charactersScale[i].localScale, new Vector3(1, 1, 1) * selectedScale, scaleSpeed * Time.deltaTime);
            }
            else
            {
                charactersScale[i].localScale = Vector3.Lerp(charactersScale[i].localScale, new Vector3(1,1,1) * normalScale, scaleSpeed * Time.deltaTime);
            }
        }
        if (rotateIndicator || backIndicator)
        {
            camera.eulerAngles = Vector3.Lerp(camera.eulerAngles, cameraTargetRot, Time.deltaTime * cameraRotSpeed);
        }
        if (Quaternion.Angle(camera.rotation, Quaternion.Euler(cameraTargetRot)) < 0.1f)
        {
            rotateIndicator = false;
            backIndicator = false;
        }
    }
}
