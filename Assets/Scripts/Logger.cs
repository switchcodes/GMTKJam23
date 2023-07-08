using TMPro;
using UnityEngine;

public class Logger : MonoBehaviour {
	// Start is called before the first frame update
	
	public TextMeshProUGUI logText;
	
	public void Log(string message) {
		logText.text += message + "\n";
	}
}