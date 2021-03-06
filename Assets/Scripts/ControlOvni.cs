﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlOvni : MonoBehaviour {

    private int puntos = 500;
    // Enumeración para expresar el sentido del movimiento
    private enum direccion { IZQ, DER };

    // Rumbo que lleva el pack de aliens
    private direccion rumbo = direccion.DER;

    // Límites de la pantalla
    private float limiteIzq;
    private float limiteDer;

    // Velocidad a la que se desplazan los aliens (medido en u/s)
    private float velocidad = 2f;
    private float fuerza = 0.03f;

    private GameObject ovni;

    private GameObject marcador;
    private GameObject efectoExplosion;

    public Rigidbody2D bala;

    private int contador = 0;
    // Use this for initialization
    void Start () {
        marcador = GameObject.Find("Marcador");
        
        efectoExplosion = GameObject.Find("EfectoExplosion");

        float distanciaHorizontal = Camera.main.orthographicSize * Screen.width / Screen.height;

        // Calculamos el límite izquierdo y el derecho de la pantalla (añadimos una unidad a cada lado como margen)
        limiteIzq = -1.0f * distanciaHorizontal*5 + 1;
        limiteDer = 1.0f * distanciaHorizontal*5 - 1;
    }
	
	// Update is called once per frame
	void Update () 

    {
            ovni = GameObject.Find("Ovni");   
       
            
            // Nos movemos a la derecha (todos los aliens que queden)
            ovni.transform.Translate(Vector2.right * velocidad * Time.deltaTime);

               

            // Nos movemos a la derecha hasta llegar al límite para entrar por el otro lado
            if (ovni.transform.position.x < limiteDer)
            {
                ovni.transform.Translate(Vector2.right * velocidad * Time.deltaTime);
            }
            else
            {
                ovni.transform.position = new Vector2(limiteIzq, transform.position.y);
            }
        
                

            contador++;

            if (contador % 100 == 0)
            {
                disparar();
            }
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        // Detectar la colisión entre el alien y otros elementos
        // Necesitamos saber contra qué hemos chocado
        if (coll.gameObject.tag == "disparo" || coll.gameObject.tag == "disparo2")
        {
            // Sonido de explosión
            GetComponent<AudioSource>().Play();

            // Sumar la puntuación al marcador
            if (coll.gameObject.tag == "disparo")
            {
                marcador.GetComponent<ControlMarcador>().puntos += puntos;
            }
			//si hay mas de dos jugadores:
            else
            {
                marcador.GetComponent<ControlMarcador>().puntos2 += puntos;
            }
            Destroy(coll.gameObject);

            efectoExplosion.GetComponent<AudioSource>().Play();
            Destroy(gameObject);

		}else if (coll.gameObject.tag == "misil" || coll.gameObject.tag == "misil2") {

			GetComponent<AudioSource> ().Play ();

			// Sumar la puntuación al marcador
			if (coll.gameObject.tag == "misil") {
				marcador.GetComponent<ControlMarcador> ().puntos += puntos;
				PlayerPrefs.SetInt ("puntosJugador1", marcador.GetComponent<ControlMarcador> ().puntos);
			} else {
				marcador.GetComponent<ControlMarcador> ().puntos2 += puntos;
				PlayerPrefs.SetInt ("puntosJugador2", marcador.GetComponent<ControlMarcador> ().puntos2);
			}
			Destroy (gameObject);
			coll.gameObject.GetComponent<ControlMisil> ().vidas--;

			if (coll.gameObject.GetComponent<ControlMisil> ().vidas == 0) {
				Destroy (coll.gameObject);
			}

		}
        
        // El disparo desaparece (cuidado, si tiene eventos no se ejecutan)
    }


    void disparar()
    {

        // Hacemos copias del prefab del disparo y las lanzamos
        Rigidbody2D d = (Rigidbody2D)Instantiate(bala, transform.position, transform.rotation);

        // Desactivar la gravedad para este objeto, si no, ¡se cae!
        d.gravityScale = 0;
        
        // Posición de partida, en la punta de la nave
        d.transform.Translate(Vector2.down * 0.7f);

        // Lanzarlo
        d.AddForce(Vector2.down * fuerza, ForceMode2D.Impulse);


    }

}
