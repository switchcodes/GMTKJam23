using TMPro;
using UnityEngine;

public class Logger : MonoBehaviour {
	// Start is called before the first frame update
	
	public TextMeshProUGUI logText;
	
	public void Log(string message) {
		var oldText = logText.text;
		logText.text = message + "\n" + oldText;
	}
}