using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	public void Unpause() {
		gameObject.SetActive(false);
		Time.timeScale = 1;
	}

	public void MainMenu() {
		SceneManager.LoadScene("TitleScreen");
	}
}