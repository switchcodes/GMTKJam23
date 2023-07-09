using UnityEngine;
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
		_currentPart = tutorialParts[_currentPartIndex++];
		_image.sprite = _currentPart;
	}

	public void Back() {
		_currentPart = tutorialParts[_currentPartIndex--];
		_image.sprite = _currentPart;
	}
}