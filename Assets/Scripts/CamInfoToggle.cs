using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamInfoToggle : MonoBehaviour {
	// Start is called before the first frame update
	public GameObject camTab;
	public GameObject infoTab;
	public ToggleGroup toggleGroup;
	public Toggle camToggle;
	public Toggle infoToggle;

	// Update is called once per frame
	private void Update() {
		if (toggleGroup.GetFirstActiveToggle() == camToggle) {
			camTab.SetActive(true);
			infoTab.SetActive(false);
		}
		else if (toggleGroup.GetFirstActiveToggle() == infoToggle) {
			camTab.SetActive(false);
			infoTab.SetActive(true);
		}
	}
}