using System;
using TMPro;
using UnityEngine;

public class Logger : MonoBehaviour {
	// Start is called before the first frame update
	
	public TextMeshProUGUI logText;
	public TextMeshProUGUI warnErrorText;
	public int warnErrorTimeout = 5;
	
	private float _warnErrorTimer = 0;
	
	private void Start() {
		warnErrorText.text = "";
	}

	private void Update() {
		if (!(_warnErrorTimer > 0)) return;
		_warnErrorTimer -= Time.deltaTime;
		if (_warnErrorTimer <= 0) {
			warnErrorText.text = "";
		}
	}

	public void Error(string message) {
		warnErrorText.text = "<color=#FF5859>ERROR: " + message + "</color>\n";
		_warnErrorTimer = warnErrorTimeout;
	}
	
	public void Warn(string message) {
		warnErrorText.text = "<color=#FAFF53>WARNING: " + message + "</color>\n";
		_warnErrorTimer = warnErrorTimeout;
	}
	
	public void Log(string message) {
		var oldText = logText.text;
		logText.text = message + "\n" + oldText;
	}
}