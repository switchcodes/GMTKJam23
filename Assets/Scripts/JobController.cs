using UnityEngine;

public class JobController : MonoBehaviour {
	[Header("Components")]
	public Logger logger;
	public PrinterStatus printerStatus;
	public MoralMeter moralMeter;
	public GameObject notificationPanel;

	[Header("Modifiers")] 
	public float difficultyModifier = 1f;
	public float complaintThreshold = 4f;

	[Header("Debug")]
	public bool overrideDifficulty = false;
	public bool ignoreJobPresence = false;

	private int _complainedAboutPaper = 0;
	private int _complainedAboutCyan = 0;
	private int _complainedAboutMagenta = 0;
	private int _complainedAboutYellow = 0;
	private int _complainedAboutBlack = 0;

	public PrintJob currentJob;

	// Start is called before the first frame update
	private void Awake() {
		if (overrideDifficulty) return;
		switch (SceneChangeInfo.Information) {
			case SceneChangeInfo.Difficulty.Easy:
				difficultyModifier = 0.8f;
				complaintThreshold = 3f;
				printerStatus.maxPaper = 750;
				printerStatus.paperLevel = 750;
				break;
			case SceneChangeInfo.Difficulty.Hard:
				difficultyModifier = 1.2f;
				complaintThreshold = 5f;
				printerStatus.maxPaper = 250;
				printerStatus.paperLevel = 250;
				break;
			case SceneChangeInfo.Difficulty.Normal:
			default:
				break;
		}

		if (ignoreJobPresence) {
			currentJob = new PrintJob(
				"Test Job",
				3,
				1,
				0,
				1,
				2,
				10,
				10,
				60);
		}
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
		
		var inkSatisfactionModifier = 1f;
		var pagesPrinted = 0f;

		for (var i = 0; i < currentJob.paper; i++) {
			if (printerStatus.paperLevel <= 0) continue;
			printerStatus.blackLevel -= currentJob.black;
			if (printerStatus.blackLevel <= printerStatus.inkThreshold) inkSatisfactionModifier -= 0.3f;
			printerStatus.cyanLevel -= currentJob.cyan;
			if (printerStatus.cyanLevel <= printerStatus.inkThreshold) inkSatisfactionModifier -= 0.1f;
			printerStatus.magentaLevel -= currentJob.magenta;
			if (printerStatus.magentaLevel <= printerStatus.inkThreshold) inkSatisfactionModifier -= 0.1f;
			printerStatus.yellowLevel -= currentJob.yellow;
			if (printerStatus.yellowLevel <= printerStatus.inkThreshold) inkSatisfactionModifier -= 0.1f;
			printerStatus.paperLevel--;
			pagesPrinted++;
		}
        
		moralMeter.moralLevel += currentJob.moralityModifier * difficultyModifier * (pagesPrinted / currentJob.paper);
		moralMeter.satisfactionLevel += currentJob.satisfactionModifier * difficultyModifier * (pagesPrinted / currentJob.paper) * inkSatisfactionModifier;
		completeJob();
		logger.Log("Printed");
	}

	public void HandlePaperJam() {
		printerStatus.paperLevel -= Random.Range(1, 10);
		if (currentJob == null) {
			logger.Log("No job selected");
			logger.Log("Caused paper jam anyways");
			moralMeter.satisfactionLevel -= 0.5f * difficultyModifier;
			return;
		}

		moralMeter.moralLevel -= 0.5f * currentJob.moralityModifier * difficultyModifier;
		moralMeter.satisfactionLevel -= 0.5f * currentJob.satisfactionModifier * difficultyModifier;
		completeJob();
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
		completeJob();
		logger.Log("Refused to print");
	}

	public void HandleOutOfPaperBtn() {
		_complainedAboutPaper++;
		if (printerStatus.paperLevel / (printerStatus.maxPaper * 1f) >= 0.1) {
			moralMeter.satisfactionLevel -= difficultyModifier;
			logger.Log("There's enough paper left...");
		}
		else {
			logger.Log("Out of paper");
		}
		
		if (Random.Range(0, 1) >= 0.5 || _complainedAboutPaper >= complaintThreshold) {
			printerStatus.paperLevel = printerStatus.maxPaper;
			_complainedAboutPaper = 0;
			logger.Log("Paper refilled");
		}

		CloseNotifications();
	}

	public void HandleOutOfCyanBtn() {
		_complainedAboutCyan++;
		if (printerStatus.cyanLevel / 100f >= 0.1 || currentJob.cyan == 0) {
			moralMeter.satisfactionLevel -= difficultyModifier;
			logger.Log("There's enough cyan left...");
		}
		else {
			logger.Log("Out of cyan");
		}
		if (Random.Range(0, 1) >= 0.5 || _complainedAboutCyan >= complaintThreshold) {
			printerStatus.cyanLevel = 100;
			_complainedAboutCyan = 0;
			logger.Log("Cyan refilled");
		}

		CloseNotifications();
	}

	public void HandleOutOfMagentaBtn() {
		_complainedAboutMagenta++;
		if (printerStatus.magentaLevel / 100f >= 0.1 || currentJob.magenta == 0) {
			moralMeter.satisfactionLevel -= difficultyModifier;
			logger.Log("There's enough magenta left...");
		}
		else {
			logger.Log("Out of magenta");
		}
		if (Random.Range(0, 1) >= 0.5 || _complainedAboutMagenta >= complaintThreshold) {
			printerStatus.magentaLevel = 100;
			_complainedAboutMagenta = 0;
			logger.Log("Magenta refilled");
		}

		CloseNotifications();
	}

	public void HandleOutOfYellowBtn() {
		_complainedAboutYellow++;
		if (printerStatus.yellowLevel / 100f >= 0.1 || currentJob.yellow == 0) {
			moralMeter.satisfactionLevel -= difficultyModifier;
			logger.Log("There's enough yellow left...");
		}
		else {
			logger.Log("Out of yellow");
		}
		if (Random.Range(0, 1) >= 0.5 || _complainedAboutYellow >= complaintThreshold) {
			printerStatus.yellowLevel = 100;
			_complainedAboutYellow = 0;
			logger.Log("Yellow refilled");
		}

		CloseNotifications();
	}

	public void HandleOutOfBlackBtn() {
		_complainedAboutBlack++;
		if (printerStatus.blackLevel / 150f >= 0.1 || currentJob.black == 0) {
			moralMeter.satisfactionLevel -= difficultyModifier;
			logger.Log("There's enough black left...");
		}
		else {
			logger.Log("Out of black");
		}
		if (Random.Range(0, 1) >= 0.5 || _complainedAboutBlack >= complaintThreshold) {
			printerStatus.blackLevel = 100;
			_complainedAboutBlack = 0;
			logger.Log("Black refilled");
		}

		CloseNotifications();
	}

	public void CloseNotifications() {
		notificationPanel.SetActive(false);
	}

	private void completeJob() {
		if (!ignoreJobPresence) currentJob = null;
		printerStatus.printCount++;
	}

	public void TriggerOutOfMoral() {
	}

	public void TriggerOutOfSatisfaction() {
	}
}