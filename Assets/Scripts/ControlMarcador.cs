using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlMarcador : MonoBehaviour
{

    // Puntos ganados en la partida
    public int puntos = 0;
    public int puntos2 = 0;

    // Objeto donde mostramos el texto
    public GameObject puntuacion;
    public GameObject puntuacion2;
    public int numJugadores;

    private Text t;
    private Text t2;

    // Use this for initialization
    void Start()
    {
        numJugadores = PlayerPrefs.GetInt("numJug");
        // Localizamos el componente del UI
        t = puntuacion.GetComponent<Text>();
        t2 = puntuacion2.GetComponent<Text>();
        if (numJugadores != 2)
        {
            t2.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Actualizamos el marcador
        string texto = "Puntos:";

        if (numJugadores == 2)
        {
            t2.text = "Player 2: " + puntos2.ToString() + "\n";
            texto = "Player 1:";
        }
        t.text = texto + " " + puntos.ToString() + "\n";
    }

}
