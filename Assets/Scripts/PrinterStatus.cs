using System;
using TMPro;
using UnityEngine;

public class PrinterStatus : MonoBehaviour {
	public int paperLevel = 500;
	public int cyanLevel = 100;
	public int magentaLevel = 100;
	public int yellowLevel = 100;
	public int blackLevel = 150;
	
	public int maxPaper = 500;
	
	public int inkThreshold = 10;

	public int printCount = 0;
	
	public TextMeshProUGUI printCountText;
	
	public TextMeshProUGUI paperLevelText;
	public RectTransform cyanLevelBar;
	public RectTransform magentaLevelBar;
	public RectTransform yellowLevelBar;
	public RectTransform blackLevelBar;

	private void Update() {
		cyanLevel = Math.Clamp(cyanLevel, 0, 100);
		magentaLevel = Math.Clamp(magentaLevel, 0, 100);
		yellowLevel = Math.Clamp(yellowLevel, 0, 100);
		blackLevel = Math.Clamp(blackLevel, 0, 150);
		paperLevelText.text = paperLevel + "/" + maxPaper;
		cyanLevelBar.localScale = new Vector3(1, cyanLevel / 100f, 1);
		magentaLevelBar.localScale = new Vector3(1, magentaLevel / 100f, 1);
		yellowLevelBar.localScale = new Vector3(1, yellowLevel / 100f, 1);
		blackLevelBar.localScale = new Vector3(1, blackLevel / 100f, 1);
		
		printCountText.text = "Job count: " + printCount;
	}

	public bool IsOperational() {
		return blackLevel > inkThreshold && cyanLevel > inkThreshold && magentaLevel > inkThreshold && yellowLevel > inkThreshold && paperLevel > 0;
	}
}