using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlNave2 : MonoBehaviour
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


	public float fireRateMisil = 2.0F;
	private float nextFireMisil = 0.0F;

    public int nivel = 1;
    private GameObject nave;
    // Use this for initialization
    void Start ()
	{
        int numJugadores = PlayerPrefs.GetInt("numJug");
        if (numJugadores == 1)
        {
            nave = GameObject.Find("Nave2");
            Destroy(nave);
        }
        else
        {
            if (nivel == 1)
            {
                PlayerPrefs.SetInt("puntosJugador2", 0);
            }

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
		if (Input.GetKey (KeyCode.A)) {

			// Nos movemos a la izquierda hasta llegar al límite para entrar por el otro lado
			if (transform.position.x > limiteIzq) {
				transform.Translate (Vector2.left * velocidad * Time.deltaTime);
			} else {
				transform.position = new Vector2 (limiteDer, transform.position.y);			
			}
		}

		// Tecla: Derecha
		if (Input.GetKey (KeyCode.D)) {

			// Nos movemos a la derecha hasta llegar al límite para entrar por el otro lado
			if (transform.position.x < limiteDer) {
				transform.Translate (Vector2.right * velocidad * Time.deltaTime);
			} else {
				transform.position = new Vector2 (limiteIzq, transform.position.y);			
			}
		}

		// Disparo
		if (Input.GetKeyDown (KeyCode.LeftControl) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            disparar ();
		}

		//Disparo Especial
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextFireMisil) {
			nextFireMisil = Time.time + fireRateMisil;
			disparoEspecial();
		}

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
		d.transform.Translate (Vector2.up * 1.5f);

		// Lanzarlo
		//d.AddForce (Vector2.up * fuerzaMisil, ForceMode2D.Impulse);	
	}

}

