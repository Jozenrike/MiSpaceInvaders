using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GeneradorAliens : MonoBehaviour
{

    public int nivel = 1;
    // Publicamos la variable para conectarla desde el editor
    public Rigidbody2D prefabAlien1;
    public Rigidbody2D prefabAlien2;

    // Referencia para guardar una matriz de objetos
    public Rigidbody2D[,] aliens;

	// Tamaño de la invasión alienígena
	public int FILAS = 4;
    public int COLUMNAS = 7;

	// Enumeración para expresar el sentido del movimiento
	private enum direccion { IZQ, DER };

	// Rumbo que lleva el pack de aliens
	private direccion rumbo = direccion.DER;

	// Posición vertical de la horda (lo iremos restando de la .y de cada alien)
	private float altura = 0.2f;
    private float alturaTotal = 0.0f;

	// Límites de la pantalla
	private float limiteIzq;
	private float limiteDer;

	// Velocidad a la que se desplazan los aliens (medido en u/s)
	private float velocidad = 5.0f;

    private float fuerza = 1.0f;

    private int contador = 0;

    private GameObject marcador;
    public Rigidbody2D bala;

	private Vector2 posicion;
    public Vector2[,] posiciones;

    // Use this for initialization
    void Start ()
	{
        if (nivel > 1)
        {
            FILAS = FILAS + 1;
            COLUMNAS = COLUMNAS + 1;
            velocidad = velocidad + 1.0f;
        }
        // Rejilla de 4x7 aliens
        posiciones = new Vector2[FILAS, COLUMNAS];
        generarAliens (FILAS, COLUMNAS, 1.5f, 1.0f);

        if (nivel > 1)
        {
            marcador = GameObject.Find("Marcador");
            marcador.GetComponent<ControlMarcador>().puntos = PlayerPrefs.GetInt("puntosJugador1");
            if (PlayerPrefs.GetInt("numJug") == 2)
            {
                marcador.GetComponent<ControlMarcador>().puntos2 = PlayerPrefs.GetInt("puntosJugador2");
            }
        }
        // Calculamos la anchura visible de la cámara en pantalla
        float distanciaHorizontal = Camera.main.orthographicSize * Screen.width / Screen.height;

		// Calculamos el límite izquierdo y el derecho de la pantalla (añadimos una unidad a cada lado como margen)
		limiteIzq = -1.0f * distanciaHorizontal + 1;
		limiteDer = 1.0f * distanciaHorizontal - 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Contador para saber si hemos terminado
		int numAliens = 0;

        // Variable para saber si al menos un alien ha llegado al borde
        bool limiteAlcanzado = false;

		// Recorremos la horda alienígena
		for (int i = 0; i < FILAS; i++) {
			for (int j = 0; j < COLUMNAS; j++) {

				// Comprobamos que haya objeto, para cuando nos empiecen a disparar
				if (aliens [i, j] != null) {

					// Un alien más
					numAliens += 1;

					// ¿Vamos a izquierda o derecha?
					if (rumbo == direccion.DER) {

						// Nos movemos a la derecha (todos los aliens que queden)
						aliens [i, j].transform.Translate (Vector2.right * velocidad * Time.deltaTime);
                        //Debug.Log("Derecha Viejo"+posiciones[i, j]);
                        Vector2 suma = Vector2.right * velocidad * Time.deltaTime;
                        posiciones[i, j] = posiciones[i, j] + suma;
                        //Debug.Log("Derecha Nuevo" + posiciones[i, j]);

                        // Comprobamos si hemos tocado el borde
                        if (aliens [i, j].transform.position.x > limiteDer) {
							limiteAlcanzado = true;
						}
					} else {

						// Nos movemos a la derecha (todos los aliens que queden)
						aliens [i, j].transform.Translate (Vector2.left * velocidad * Time.deltaTime);
                        //Debug.Log("Izquierdo viejo" + posiciones[i, j]);
                        //Guardo las posiciones y las modifico para disparar desde ellas
                        Vector2 suma = Vector2.left * velocidad * Time.deltaTime;
                        posiciones[i, j] = posiciones[i, j] + suma;
                        //Debug.Log("Izquierdo nuevo" + posiciones[i, j]);

                        // Comprobamos si hemos tocado el borde
                        if (aliens [i, j].transform.position.x < limiteIzq) {
							limiteAlcanzado = true;
						}
					}
				}
			}

        }

        // Si no quedan aliens, hemos terminado
        if (numAliens == 0) 
        {
            int nuevoNivel = nivel + 1;
            string lvl = "Nivel"+nuevoNivel;
            Debug.Log(lvl);
            if (nivel > 0)
            {
                SceneManager.LoadScene(lvl);
                nivel = nuevoNivel;
            }
            else
            {
                SceneManager.LoadScene(lvl);
                nivel = nuevoNivel;
            }
        }

        // Si al menos un alien ha tocado el borde, todo el pack cambia de rumbo
        if (limiteAlcanzado == true)
        {
            //aumento velocidad cuando llegan a cierta distancia de la nave
            alturaTotal = alturaTotal + altura;
            if (alturaTotal > 2.0f)
            {
                velocidad = velocidad + 0.2f;
            }
            for (int i = 0; i < FILAS; i++) {
				for (int j = 0; j < COLUMNAS; j++) {

					// Comprobamos que haya objeto, para cuando nos empiecen a disparar
					if (aliens [i, j] != null)
                    {
                        aliens[i,j].transform.Translate (Vector2.down * altura);
                       
                        Vector2 suma = Vector2.down * altura;
                        posiciones[i, j] = posiciones[i, j] + suma;
                        
                    }
				}
			}


			if (rumbo == direccion.DER) {
				rumbo = direccion.IZQ;
			} else {
				rumbo = direccion.DER;
			}
		}

        contador++;

        if (contador % 100 == 0)
        {
            disparar();
        }
    }

	void generarAliens (int filas, int columnas, float espacioH, float espacioV, float escala = 1.0f)
	{
        /* Creamos una rejilla de aliens a partir del punto de origen
		 * 
		 * Ejemplo (2,5):
		 *   A A A A A
		 *   A A O A A
		 */
        // Calculamos el punto de origen de la rejilla
        Vector2 origen = new Vector2 (transform.position.x - (columnas / 2.0f) * espacioH + (espacioH / 2), transform.position.y);
        // Instanciamos el array de referencias
        aliens = new Rigidbody2D[filas, columnas];
        //reescalado de aliens por nivel
        if (nivel > 1)
        {
            espacioH = espacioH * 0.8f;
            espacioV = espacioV * 0.8f;
        }
        // Fabricamos un alien en cada posición del array
        for (int i = 0; i < filas; i++) {
			for (int j = 0; j < columnas; j++) {
				// Posición de cada alien
				posicion = new Vector2 (origen.x + (espacioH * j), origen.y + (espacioV * i));
                posiciones[i,j] = posicion;
                // Instanciamos el objeto partiendo del prefab
                Rigidbody2D alien = null;

                //arrayAliens[cont] = alien;
                if (nivel == 1)
                {
                    alien = (Rigidbody2D)Instantiate(prefabAlien1, posicion, transform.rotation);
                }
                else if (nivel == 2)
                {
                    if (i > 3)
                    {
                        alien = (Rigidbody2D)Instantiate(prefabAlien2, posicion, transform.rotation);
                        alien.transform.localScale = prefabAlien1.transform.localScale * 0.8f;
                    }
                    else
                    {
                        alien = (Rigidbody2D)Instantiate(prefabAlien1, posicion, transform.rotation);
                        alien.transform.localScale = prefabAlien1.transform.localScale * 0.8f;
                    }

                }

                // Guardamos el alien en el array
                aliens [i, j] = alien;

				// Escala opcional, por defecto 1.0f (sin escala)
				// Nota: El prefab original ya está escalado a 0.2f
				//alien.transform.localScale = new Vector2 (0.2f * escala, 0.2f * escala);
			}
		}

	}

    void disparar()
    {

        // Hacemos copias del prefab del disparo y las lanzamos
        Rigidbody2D d = (Rigidbody2D)Instantiate(bala, transform.position, transform.rotation);

        // Desactivar la gravedad para este objeto, si no, ¡se cae!
        d.gravityScale = 0;
        int numero = Random.Range(0, COLUMNAS-1);
        int numero2 = Random.Range(0, FILAS-1);
        // Posición de partida, en la punta de la nave
            //derecha
         d.transform.Translate(posiciones[numero2, numero]);
        


        // Lanzarlo
        d.AddForce(Vector2.down * fuerza, ForceMode2D.Impulse);
                
   
    }

}
