using System;
using TMPro;
using UnityEngine;

public class PrinterStatus : MonoBehaviour {
	public int paperLevel = 100;
	public int cyanLevel = 100;
	public int magentaLevel = 100;
	public int yellowLevel = 100;
	public int blackLevel = 100;
	
	public TextMeshProUGUI paperLevelText;
	public RectTransform cyanLevelBar;
	public RectTransform magentaLevelBar;
	public RectTransform yellowLevelBar;
	public RectTransform blackLevelBar;

	private void Update() {
		paperLevelText.text = paperLevel.ToString() + "/100";
		cyanLevelBar.localScale = new Vector3(1, cyanLevel / 100f, 1);
		magentaLevelBar.localScale = new Vector3(1, magentaLevel / 100f, 1);
		yellowLevelBar.localScale = new Vector3(1, yellowLevel / 100f, 1);
		blackLevelBar.localScale = new Vector3(1, blackLevel / 100f, 1);
	}
}