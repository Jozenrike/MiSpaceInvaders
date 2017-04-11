using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
	public Transform canvas;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}
	public void Pause()
	{
		if (canvas.gameObject.activeInHierarchy == false)
		{
			canvas.gameObject.SetActive(true);
			Time.timeScale = 0;
		}
		else
		{
			canvas.gameObject.SetActive(false);
			Time.timeScale = 1; 
		}
	}

	public void reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1; 
	}

	public void mainMenu(){
		SceneManager.LoadScene("Inicio");
		Time.timeScale = 1;
	}

}
