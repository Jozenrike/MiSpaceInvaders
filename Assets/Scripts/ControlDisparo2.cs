using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlDisparo2 : MonoBehaviour {
    
    private int numJugadores = 0;
    // Use this for initialization
    void Start () {
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        int x = PlayerPrefs.GetInt("xNave");
        numJugadores = PlayerPrefs.GetInt("numJug");

        // Necesitamos saber contra qué hemos chocado
        if (coll.gameObject.tag == "disparo" || coll.gameObject.tag == "disparo2")
        {
            Destroy(gameObject);
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.tag == "nave" || coll.gameObject.tag == "nave2")
        {
            
            if (numJugadores.Equals("2"))
            {
                Destroy(gameObject);
                Destroy(coll.gameObject);
                if (x == 1) {
                    SceneManager.LoadScene("GameOver");
                }
                PlayerPrefs.SetInt("xNave", 1);
            }
            else
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        else if (coll.gameObject.tag == "alien")
        {
                
        }

        // El disparo desaparece (cuidado, si tiene eventos no se ejecutan)
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    public static void IgnoreCollision(Collider2D collider1, Collider2D collider2, bool ignore = true)
    {

    }

    // Update is called once per frame
    void Update () {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
