using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class AnimatedDialog : MonoBehaviour
{
    public Text textoInicio;


    public Button jugador1;
    public Button jugador2;

    public string NumJug;

    public string stringTexto = "Enter To Start";
    public float speed = 0.1f;

    //int stringIndex = 0;
    int characterIndex = 0;

<<<<<<< HEAD


	private GameObject nave;
	private GameObject nave2;
=======
>>>>>>> Pruebas
    // Use this for initialization
    void Start()
    {
        jugador1.gameObject.SetActive(false);
        jugador2.gameObject.SetActive(false);
        StartCoroutine(DisplayTimer());
        //jugador1.onClick.AddListener(TaskOnClick);
        /*Button btn = jugador1.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);*/
    }

    IEnumerator DisplayTimer()
    {
        while (1 == 1)
        {
            yield return new WaitForSeconds(speed);
            if (characterIndex > stringTexto.Length)
            {
                continue;
            }
            //textoInicio.text = strings [stringIndex].Substring [0, characterIndex];
            //textoInicio.text = "hola";
            textoInicio.text = stringTexto.Substring(0, characterIndex);
            characterIndex++;
        }
        //jugador1.GetComponentInChildren<Text>().text = "la di da";

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            textoInicio.enabled = false;
            jugador1.gameObject.SetActive(true);
            jugador2.gameObject.SetActive(true);
        }


    }
    public void TaskOnClick(string name)
    {
        int numJugadores = 0;
        switch (name)
        {
            case "1":
                Debug.Log("jugador 1");
                numJugadores = 1;
                break;
            case "2":
                Debug.Log("jugador 2");
                numJugadores = 2;
                break;

        }

        Scenes.Load("Nivel1", "numJugadores", numJugadores + "");
        SceneManager.LoadScene("Nivel1");

        // Calculamos la anchura visible de la cámara en pantalla
        float distanciaHorizontal = Camera.main.orthographicSize * Screen.width / Screen.height;

        //Mover naves cuando sean 2
        //float limiteIzq = -1.0f * distanciaHorizontal;
        //float limiteDer = 1.0f * distanciaHorizontal;
        PlayerPrefs.SetInt("xNave", 0);
        PlayerPrefs.SetInt("numJug", numJugadores);
<<<<<<< HEAD


=======
>>>>>>> Pruebas
        //Dictionary <string, string> parametros = Scenes.getSceneParameters();
        //string numJugadores = Scenes.getParam("numJugadores");

        

    }



}
