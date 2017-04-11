using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlAlien1 : MonoBehaviour
{
	// Conexión al marcador, para poder actualizarlo
	private GameObject marcador;

	// Por defecto, 100 puntos por cada alien
	private int puntos = 200;

	private int vidas = 2;
	// Objeto para reproducir la explosión de un alien
	private GameObject efectoExplosion;

	//AGREGAR///////
	private GameObject ga;
	private int filas, columnas;
	private Vector2[,] array;
	private Vector2 bueno;
    // Use this for initialization
    void Start ()
	{
		ga = GameObject.Find ("GeneradorAliens");
		// Localizamos el objeto que contiene el marcador
		marcador = GameObject.Find ("Marcador");

		// Objeto para reproducir la explosión de un alien
		efectoExplosion = GameObject.Find ("EfectoExplosion");

		//AGREGAR///////
		array = ga.GetComponent<GeneradorAliens>().posiciones;
		filas = ga.GetComponent<GeneradorAliens>().FILAS;
		columnas = ga.GetComponent<GeneradorAliens>().COLUMNAS;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		// Detectar la colisión entre el alien y otros elementos

		// Necesitamos saber contra qué hemos chocado
		if (coll.gameObject.tag == "disparo" || coll.gameObject.tag == "disparo2") {

			// Sonido de explosión
			GetComponent<AudioSource> ().Play ();

			// Sumar la puntuación al marcador
			if (coll.gameObject.tag == "disparo") {
				marcador.GetComponent<ControlMarcador> ().puntos += puntos / 2;
				PlayerPrefs.SetInt ("puntosJugador1", marcador.GetComponent<ControlMarcador> ().puntos);
			} else {
				marcador.GetComponent<ControlMarcador> ().puntos2 += puntos / 2;
				PlayerPrefs.SetInt ("puntosJugador2", marcador.GetComponent<ControlMarcador> ().puntos2);
			}


			vidas--;


			// El disparo desaparece (cuidado, si tiene eventos no se ejecutan)
			Destroy (coll.gameObject);

			// El alien desaparece (no hace falta retraso para la explosión, está en otro objeto)
			if (vidas == 0) {
				efectoExplosion.GetComponent<AudioSource> ().Play ();
				Destroy (gameObject);
			}


		} else if (coll.gameObject.tag == "nave" || coll.gameObject.tag == "nave2") {			
			SceneManager.LoadScene ("GameOver");
		}  else if (coll.gameObject.tag == "misil" || coll.gameObject.tag == "misil2") {

			// Sonido de explosión
			GetComponent<AudioSource> ().Play ();

			// Sumar la puntuación al marcador
			if (coll.gameObject.tag == "misil") {
				marcador.GetComponent<ControlMarcador> ().puntos += puntos / 2;
				PlayerPrefs.SetInt ("puntosJugador1", marcador.GetComponent<ControlMarcador> ().puntos);
			} else {
				marcador.GetComponent<ControlMarcador> ().puntos2 += puntos / 2;
				PlayerPrefs.SetInt ("puntosJugador2", marcador.GetComponent<ControlMarcador> ().puntos2);
			}

			coll.gameObject.GetComponent<ControlMisil> ().vidas--;
			vidas--;

			if (coll.gameObject.GetComponent<ControlMisil> ().vidas == 0) {
				Destroy (coll.gameObject);
			}
			if (vidas == 0) {
				efectoExplosion.GetComponent<AudioSource> ().Play ();
				Destroy (gameObject);
			}
		}
		//AGREGAR///////
		var vec = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
		for (int i = 0; i < filas; i++)
		{
			for (int j = 0; j < columnas; j++)
			{

				if (array[i, j] == vec)
				{
					array[i, j] = new Vector2();
				}
			}
		}
	}
}
