using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PrintJob: MonoBehaviour {
	
	public string jobName;
	public string fileName;
	public int paper;
	public int cyan;
	public int magenta;
	public int yellow;
	public int black;
	public float moralityModifier;
	public float satisfactionModifier;
	public int timeToComplete;
	public bool illegal;
	public Sprite sprite;
	public Image imageComponent;

	public TextMeshProUGUI title;
    
	public void Start() {
		if (imageComponent && sprite) imageComponent.sprite = sprite;
		if (title) title.text = jobName;
	}
	
	public bool IsBlackAndWhite() {
		return cyan == 0 && magenta == 0 && yellow == 0;
	}
}