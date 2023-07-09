using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QueueFile : MonoBehaviour {
	public TextMeshProUGUI fileNameTMP;
	public string fileName;
	public JobController jobController;

	public bool isActive;

	public void SelectJob() {
		jobController.SelectJob(this);
	}

	public void SetIsActive(bool active) {
		isActive = active;
		UpdateFileName();
	}

	public void OnMouseEnter() {
		UpdateFileName(true);
	}

	public void OnMouseExit() {
		UpdateFileName();
	}

	private void UpdateFileName(bool isHover = false) {
		var newName = fileName;
		if (isActive) {
			newName = "<color=green>> </color>" + newName;
		}
		if (isHover && !isActive) {
			newName = "<color=blue><u>" + newName + "</u></color>";
		}
		fileNameTMP.text = newName;
	}
}