using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public Text textoInicio;
	public Text textoInicio1;
	public Text textoInicio2;
	public Text textoInicio3;

	public Button jugador1;
	public Button jugador2;

	public int numJugadores = 1;
	// Use this for initialization
	void Start () {
		numJugadores = PlayerPrefs.GetInt ("numJug");
		textoInicio2.text = PlayerPrefs.GetInt ("puntosJugador1")+"";
		if (numJugadores == 1) {
			textoInicio.text = "Total Puntos";
			textoInicio1.enabled = false;
			textoInicio3.enabled = false;
		} 
		else {
			textoInicio3.text = PlayerPrefs.GetInt ("puntosJugador2")+"";
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public void TaskOnClick(string name){
		string numJugadores = "0";
		string nivel = "";
		switch(name){
		case "1":
			Debug.Log ("new game");
			numJugadores = "1";
			nivel = "Nivel1";
			break;
		case "2":
			Debug.Log ("home");
			numJugadores = "2";
			nivel = "Inicio";
			break;

		}
		Scenes.Load(nivel, "numJugadores", numJugadores );
		//SceneManager.LoadScene("Nivel1");

	}
}
