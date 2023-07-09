using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	public Sprite[] tutorialParts;
	private Sprite _currentPart;
	private int _currentPartIndex = 0;

	private Image _image;

	private void Start() {
		_image = GetComponent<Image>();
		_currentPart = tutorialParts[_currentPartIndex];
		_image.sprite = _currentPart;
	}

	public void Next() {
		_currentPartIndex++;
		Debug.Log(_currentPartIndex);
		if (_currentPartIndex >= tutorialParts.Length) {
			SceneManager.LoadScene("SampleScene");
		}
		else {
			_currentPart = tutorialParts[_currentPartIndex];
			_image.sprite = _currentPart;	
		}
	}

	public void Back() {
		_currentPartIndex--;
		if (_currentPartIndex <= 0) {
			SceneManager.LoadScene("TitleScreen");
		}
		else {
			_currentPart = tutorialParts[_currentPartIndex];
			_image.sprite = _currentPart;	
		}
	}
}