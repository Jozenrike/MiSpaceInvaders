using UnityEngine;
using System.Collections;

public class ControlDisparo : MonoBehaviour
{
    public Transform bala;
	// Use this for initialization
	void Start ()
	{
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Empieza");
        if (other.gameObject.tag == "alien")
        {
            Debug.Log("af");
            Physics.IgnoreCollision(GetComponent<CharacterController>(), other.transform.parent.GetComponent<Collider>(), true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "alien")
        {
            Debug.Log("ete");
            Physics.IgnoreCollision(GetComponent<CharacterController>(), other.transform.parent.GetComponent<Collider>(), false);
        }


    }

    // Update is called once per frame
    void Update ()
	{
		// Eliminamos el objeto si se sale de la pantalla
		if (transform.position.y > 10) {
			Destroy (gameObject);
		}	
	}
}
