using UnityEngine;

public class JobController : MonoBehaviour {

	public Logger logger;
	public PrinterStatus printerStatus;
	public MoralMeter moralMeter;

	public GameObject notificationPanel;

	public int difficultyModifier = 1;

	public PrintJob currentJob;

	private float _complaintThreshold = 4f;

	private int _complainedAboutPaper = 0;
	private int _complainedAboutCyan = 0;
	private int _complainedAboutMagenta = 0;
	private int _complainedAboutYellow = 0;
	private int _complainedAboutBlack = 0;
	
	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		switch (moralMeter.moralLevel) {
			case >= 100:
				moralMeter.moralLevel = 100;
				break;
			case <= 0:
				TriggerOutOfMoral();
				break;
		}

		switch (moralMeter.satisfactionLevel) {
			case >= 100:
				moralMeter.satisfactionLevel = 100;
				break;
			case <= 0:
				TriggerOutOfSatisfaction();
				break;
		}
	}

	public void HandlePrint() {
		if (currentJob == null) {
			logger.Log("No job selected");
			return;
		}
		printerStatus.blackLevel -= currentJob.black;
		printerStatus.cyanLevel -= currentJob.cyan;
		printerStatus.magentaLevel -= currentJob.magenta;
		printerStatus.yellowLevel -= currentJob.yellow;
		printerStatus.paperLevel -= currentJob.paper;
		moralMeter.moralLevel += currentJob.moralityModifier * difficultyModifier;
		moralMeter.satisfactionLevel += currentJob.satisfactionModifier * difficultyModifier;
		currentJob = null;
		logger.Log("Printed");
	}

	public void HandlePaperJam() {
		printerStatus.paperLevel -= Random.Range(1, 10);
		if (currentJob == null) {
			logger.Log("No job selected");
			logger.Log("Caused paper jam anyways");
			return;
		}
		moralMeter.moralLevel -= 0.5f * currentJob.moralityModifier * difficultyModifier;
		moralMeter.satisfactionLevel -= 0.5f * currentJob.satisfactionModifier * difficultyModifier;
		currentJob = null;
		logger.Log("Caused paper jam");
	}
	
	public void HandleNotifyUser() {
		if (currentJob == null) {
			logger.Log("No job selected");
			return;
		}
		notificationPanel.SetActive(true);
	}

	public void HandleRefuse() {
		if (currentJob == null) {
			logger.Log("No Job selected");
			return;
		}
		moralMeter.moralLevel -= currentJob.moralityModifier * difficultyModifier;
		moralMeter.satisfactionLevel -= currentJob.satisfactionModifier * difficultyModifier;
		currentJob = null;
		logger.Log("Refused to print");
	}

	public void HandleOutOfPaperBtn() {
		_complainedAboutPaper++;
		logger.Log("Out of paper");
		if (Random.Range(0, 1) >= 0.5 || _complainedAboutPaper >= _complaintThreshold) {
			printerStatus.paperLevel = 100;
			_complainedAboutPaper = 0;
		}
		CloseNotifications();
	}
	
	public void HandleOutOfCyanBtn() {
		_complainedAboutCyan++;
		logger.Log("Out of cyan");
		if (Random.Range(0, 1) >= 0.5 || _complainedAboutCyan >= _complaintThreshold) {
			printerStatus.cyanLevel = 100;
			_complainedAboutCyan = 0;
		}
		CloseNotifications();
	}
	
	public void HandleOutOfMagentaBtn() {
		_complainedAboutMagenta++;
		logger.Log("Out of magenta");
		if (Random.Range(0, 1)  >= 0.5 || _complainedAboutMagenta >= _complaintThreshold) {
			printerStatus.magentaLevel = 100;
			_complainedAboutMagenta = 0;
		}
		CloseNotifications();
	}
	
	public void HandleOutOfYellowBtn() {
		_complainedAboutYellow++;
		logger.Log("Out of yellow");
		if (Random.Range(0, 1)  >= 0.5 || _complainedAboutYellow >= _complaintThreshold) {
			printerStatus.yellowLevel = 100;
			_complainedAboutYellow = 0;
		}
		CloseNotifications();
	}

	public void HandleOutOfBlackBtn() {
		_complainedAboutBlack++;
		logger.Log("Out of black");
		if (Random.Range(0, 1) >= 0.5 || _complainedAboutBlack >= _complaintThreshold) {
			printerStatus.blackLevel = 100;
			_complainedAboutBlack = 0;
		}
		CloseNotifications();
	}

	public void CloseNotifications() {
		notificationPanel.SetActive(false);
	}

	public void TriggerOutOfMoral() {
		
	}
	
	public void TriggerOutOfSatisfaction() {
		
	}
}