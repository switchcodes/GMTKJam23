using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using static GameOverConditions;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class JobController : MonoBehaviour {
	[Header("Components")] public Logger logger;
	public PrinterStatus printerStatus;
	public MoralMeter moralMeter;
	public GameObject notificationPanel;
	public GameObject queueFilePrefab;
	public GameObject queueFileParent;
	public GameObject camImageDefault;
	public GameObject camImagePolice;
	public VolumeProfile postProcessing;
	public Animation screenShake;
	public GameObject timerBar;
	public TextMeshProUGUI timerText;
	public GameObject pausePanel;
	
	private RectTransform _timerBarTransform;
	private Image _timerBarImage;
	

	private float _timer;

	private Vignette _vignette;
	private ChromaticAberration _chromaticAberration;
	private FilmGrain _filmGrain;
	private LensDistortion _lensDistortion;

	[Header("Modifiers")] public float difficultyModifier = 5f;
	public float complaintThreshold = 4f;
	public int maxActiveJobs = 10;
	public int maxTime = 30;

	[Header("Debug")] public bool overrideDifficulty = false;

	private int _complainedAboutPaper = 0;
	private int _complainedAboutCyan = 0;
	private int _complainedAboutMagenta = 0;
	private int _complainedAboutYellow = 0;
	private int _complainedAboutBlack = 0;


	public readonly Dictionary<string, PrintJob> jobs = new();
	public readonly Dictionary<string, QueueFile> queueFiles = new();
	public PrintJob currentJob;
	private bool _policeSpawned;
	public int maxHealth = 4;

	private float _health;

	// Start is called before the first frame update
	private void Awake() {
		_vignette = postProcessing.components.FirstOrDefault(x => x is Vignette) as Vignette;
		_lensDistortion = postProcessing.components.FirstOrDefault(x => x is LensDistortion) as LensDistortion;
		_chromaticAberration =
			postProcessing.components.FirstOrDefault(x => x is ChromaticAberration) as ChromaticAberration;
		_filmGrain = postProcessing.components.FirstOrDefault(x => x is FilmGrain) as FilmGrain;
		_timerBarImage = timerBar.GetComponent<Image>();
		_timerBarTransform = timerBar.GetComponent<RectTransform>();

		if (overrideDifficulty) return;
		switch (SceneChangeInfo.Difficulty) {
			case SceneChangeInfo.DifficultyEnum.Easy:
				difficultyModifier = 4f;
				complaintThreshold = 3f;
				printerStatus.maxPaper = (int)Math.Floor(printerStatus.maxPaper * 1.5);
				maxHealth *= 2;
				maxTime *= 2;
				break;
			case SceneChangeInfo.DifficultyEnum.Hard:
				difficultyModifier = 6f;
				complaintThreshold = 5f;
				printerStatus.maxPaper = (int)Math.Floor(printerStatus.maxPaper * 0.5);
				printerStatus.paperLevel = (int)Math.Floor(printerStatus.maxPaper * 0.5);
				maxHealth /= 2;
				maxTime /= 2;
				break;
			case SceneChangeInfo.DifficultyEnum.Normal:
			default:
				break;
		}

		_health = maxHealth;
		_timer = maxTime;
		printerStatus.paperLevel = printerStatus.maxPaper;

		if (_chromaticAberration) _chromaticAberration.intensity.value = 0.05f;
	}

	// Update is called once per frame
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			pausePanel.SetActive(true);
			Time.timeScale = 0;
		}
		var currentJobsInQueue = queueFiles.Count;
		var isQueueAlmostFull = currentJobsInQueue > maxActiveJobs * 0.75f;
		if (isQueueAlmostFull) {
			var timeModifier = (currentJobsInQueue / (maxActiveJobs * 0.75f)) * 2;
			_timer -= Time.deltaTime * timeModifier;
			if (_timer <= 0) {
				TriggerGameOver(OUT_OF_TIME);
			}
			timerText.text = "OVERLOAD";
			timerText.color = Color.red;
		}
		else {
			var timeModifier = (1 - currentJobsInQueue / (maxActiveJobs * 0.75f)) * 2;
			_timer = Math.Min(_timer + Time.deltaTime * timeModifier, maxTime);
			timerText.text = "NOMINAL";
			timerText.color = Color.green;
		}
		_timerBarTransform.localScale = new Vector3(1 - _timer / maxTime, 1f, 1f);
		_timerBarImage.color = Color.Lerp(Color.red, Color.green, _timer / maxTime);
		moralMeter.moralLevel = moralMeter.moralLevel switch {
			>= 100 => 100,
			<= 0 => 0,
			_ => moralMeter.moralLevel
		};

		camImagePolice.SetActive(_policeSpawned);
		camImageDefault.SetActive(!_policeSpawned);

		if (Time.frameCount % (5 * 60) == 0) {
			if (moralMeter.moralLevel < 25) {
				TriggerPoliceSpawnChance();
			}

			if (moralMeter.satisfactionLevel < 25) {
				TriggerDestructionChance();
			}

			if (moralMeter.satisfactionLevel > 75) {
				TriggerRegenChance();
			}
		}


		moralMeter.satisfactionLevel = moralMeter.satisfactionLevel switch {
			>= 100 => 100,
			<= 0 => 0,
			_ => moralMeter.satisfactionLevel
		};
	}

	public void AddJobToQueue(PrintJob job) {
		jobs.Add(job.fileName, job);
		var queueFileInstance = Instantiate(queueFilePrefab, queueFileParent.transform);
		var file = queueFileInstance.GetComponent<QueueFile>();
		file.fileName = job.fileName;
		file.SetIsActive(false);
		file.jobController = this;
		queueFiles.Add(job.fileName, file);
		if (!currentJob) SelectJob(file);
	}

	public void SelectJob(QueueFile job) {
		currentJob = jobs.TryGetValue(job.fileName, out var job1) ? job1 : null;
		foreach (var (_, printJob) in jobs) {
			printJob.gameObject.SetActive(false);
		}

		foreach (var (_, queueFile) in queueFiles) {
			queueFile.SetIsActive(false);
		}

		if (!currentJob) return;
		{
			currentJob.gameObject.SetActive(true);
			queueFiles.TryGetValue(currentJob.fileName, out var queueFile);
			if (queueFile) queueFile.SetIsActive(true);
		}
	}

	public void HandlePrint() {
		if (currentJob == null) {
			logger.Log("No job selected");
			return;
		}

		if (currentJob.illegal && _policeSpawned) {
			TriggerGameOver(CAUGHT);
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
		moralMeter.satisfactionLevel += currentJob.satisfactionModifier * difficultyModifier *
		                                (pagesPrinted / currentJob.paper) * inkSatisfactionModifier;
		CompleteJob();
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
		CompleteJob();
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
		CompleteJob();
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

	private void CompleteJob() {
		// remove print job object
		jobs.Remove(currentJob.fileName);
		Destroy(currentJob.gameObject);

		// remove print job from queue
		queueFiles.TryGetValue(currentJob.fileName, out var file);
		if (file) {
			Destroy(file.gameObject);
			queueFiles.Remove(currentJob.fileName);
		}

		if (queueFiles.Values.Count > 0) {
			// select the next job in the queue
			var newJob = queueFiles.Values.First();
			SelectJob(newJob);
		}

		printerStatus.printCount++;
	}

	private void TriggerGameOver(GameOverConditions condition) {
		_chromaticAberration.intensity.value = 0.05f;
		SceneChangeInfo.GameOverCondition = condition;
		SceneChangeInfo.Score = printerStatus.printCount;
		SceneChangeInfo.Moral = moralMeter.moralLevel;
		SceneChangeInfo.Satisfaction = moralMeter.satisfactionLevel;
		SceneManager.LoadScene("GameOver");
	}

	private void TriggerPoliceSpawnChance() {
		var rand = Random.Range(0f, 1f);
		if (!(rand > 0.50)) return;
		camImageDefault.SetActive(false);
		camImagePolice.SetActive(true);
		_policeSpawned = true;
	}

	private void TriggerDestructionChance() {
		var rand = Random.Range(0f, 1f);
		if (!(rand > 0.50)) return;
		_health--;
		screenShake.Play();
		_chromaticAberration.intensity.value = Math.Min(1 - _health / maxHealth, 0.05f);
		if (_health <= 0) TriggerGameOver(DESTROYED);
	}

	private void TriggerRegenChance() {
		var rand = Random.Range(0f, 1f);
		if (!(rand > 0.50)) return;
		_health = Math.Min(_health + 1, maxHealth);
		_chromaticAberration.intensity.value = Math.Max(1 - _health / maxHealth, 0.05f);
	}
}