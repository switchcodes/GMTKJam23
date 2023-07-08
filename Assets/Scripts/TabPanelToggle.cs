using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TabPanelToggle : MonoBehaviour {
	
	public GameObject tab1Panel;
	public GameObject tab2Panel;
	public ToggleGroup toggleGroup;
	public Toggle tab1Toggle;
	public Toggle tab2Toggle;

	// Update is called once per frame
	private void Update() {
		if (toggleGroup.GetFirstActiveToggle() == tab1Toggle) {
			tab1Panel.SetActive(true);
			tab2Panel.SetActive(false);
		}
		else if (toggleGroup.GetFirstActiveToggle() == tab2Toggle) {
			tab1Panel.SetActive(false);
			tab2Panel.SetActive(true);
		}
	}
}