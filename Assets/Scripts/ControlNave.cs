using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlNave : MonoBehaviour
{

	// Velocidad a la que se desplaza la nave (medido en u/s)
	private float velocidad = 20f;

	// Fuerza de lanzamiento del disparo
	private float fuerza = 1f;
	private float fuerzaMisil = 0.5f;

	// Acceso al prefab del disparo
	public Rigidbody2D disparo;
	public Rigidbody2D misil;

    public float fireRate = 0.3F;
	private float nextFire = 0.0F;

<<<<<<< HEAD

	public float fireRateMisil = 2.0F;
	private float nextFireMisil = 0.0F;

	private int dainoMisil = 2;

=======
>>>>>>> Pruebas
    public int nivel = 1;
    // Use this for initialization
    void Start ()
	{
        if (nivel == 1)
        {
            PlayerPrefs.SetInt("puntosJugador1", 0);
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
		// Calculamos la anchura visible de la cámara en pantalla
		float distanciaHorizontal = Camera.main.orthographicSize * Screen.width / Screen.height;

		// Calculamos el límite izquierdo y el derecho de la pantalla
		float limiteIzq = -1.0f * distanciaHorizontal;
		float limiteDer = 1.0f * distanciaHorizontal;

		// Tecla: Izquierda
		if (Input.GetKey (KeyCode.LeftArrow)) {

			// Nos movemos a la izquierda hasta llegar al límite para entrar por el otro lado
			if (transform.position.x > limiteIzq) {
				transform.Translate (Vector2.left * velocidad * Time.deltaTime);
			} else {
				transform.position = new Vector2 (limiteDer, transform.position.y);			
			}
		}

		// Tecla: Derecha
		if (Input.GetKey (KeyCode.RightArrow)) {

			// Nos movemos a la derecha hasta llegar al límite para entrar por el otro lado
			if (transform.position.x < limiteDer) {
				transform.Translate (Vector2.right * velocidad * Time.deltaTime);
			} else {
				transform.position = new Vector2 (limiteIzq, transform.position.y);			
			}
		}

		// Disparo
<<<<<<< HEAD
		if (Input.GetKeyDown (KeyCode.Keypad0) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            disparar();
        }

		//Disparo Especial
		if (Input.GetKeyDown (KeyCode.KeypadEnter) && Time.time > nextFireMisil) {
			nextFireMisil = Time.time + fireRateMisil;
			disparoEspecial();
		}

=======
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            disparar();
        }
>>>>>>> Pruebas
	}

	void disparar ()
	{
		// Hacemos copias del prefab del disparo y las lanzamos
		Rigidbody2D d = (Rigidbody2D)Instantiate (disparo, transform.position, transform.rotation);

		// Desactivar la gravedad para este objeto, si no, ¡se cae!
		d.gravityScale = 0;

		// Posición de partida, en la punta de la nave
		d.transform.Translate (Vector2.up * 0.7f);

		// Lanzarlo
		d.AddForce (Vector2.up * fuerza, ForceMode2D.Impulse);	
	}


	void disparoEspecial ()
	{
		// Hacemos copias del prefab del disparo y las lanzamos
		Rigidbody2D d = (Rigidbody2D)Instantiate (misil, transform.position, transform.rotation);

		// Desactivar la gravedad para este objeto, si no, ¡se cae!
		d.gravityScale = 0;

		// Posición de partida, en la punta de la nave
		d.transform.Translate (Vector2.up * 1f);

		// Lanzarlo
		//d.AddForce (Vector2.up * fuerzaMisil, ForceMode2D.Impulse);	
	}

}

