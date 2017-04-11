using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMisil : MonoBehaviour {

	public int vidas = 2;
	private float fuerza = 0.5f;
	private float velocidad = 5.0f;



	public float fireRate = 1.0F;
	private float nextFire = 0.0F;

	private GameObject nave;
	private GameObject nave2;
	// Use this for initialization
	void Start () {
		nave = GameObject.Find ("Nave");
		Physics2D.IgnoreCollision (nave.gameObject.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
		var x = PlayerPrefs.GetInt ("numJug");
		if (x == 2) {
			nave2 = GameObject.Find ("Nave2");
			Physics2D.IgnoreCollision (nave2.gameObject.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.up * velocidad * Time.deltaTime);
		// Eliminamos el objeto si se sale de la pantalla
		if (transform.position.y > 10) {
			Destroy (gameObject);
		}

	}

}
