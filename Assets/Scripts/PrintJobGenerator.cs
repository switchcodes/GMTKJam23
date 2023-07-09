using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using SysRandom = System.Random;

public class PrintJobGenerator : MonoBehaviour {
	// Start is called before the first frame update
	
	public SpriteDictionary spriteDictionary;
	public GameObject printJobPrefab;
	public GameObject printJobContainer;
	public JobController jobController;

	public float minTimeToNextJob = 5;
	public float maxTimeToNextJob = 10;
	public int maxActiveJobs = 10;
	private float _timer;
	private float _timeToNextJob;

	private int _jobCount = 0;

	private readonly string[] _fileExtensions = {
		"txt",
		"doc",
		"pdf"
	};

	private void Start() {
		_timer = 0;
		_timeToNextJob = Random.Range(minTimeToNextJob, maxTimeToNextJob);
	}

	// Update is called once per frame
	private void Update() {
		_timer += Time.deltaTime;
		if (jobController.queueFiles.Count == 0) _timeToNextJob = 1;
		if (_timer < _timeToNextJob || jobController.jobs.Count > maxActiveJobs) return;
		var jobs = Enum.GetValues(typeof(PrintJobEnum));
		GenerateJob((PrintJobEnum)jobs.GetValue(Random.Range(0, jobs.Length - 1)));
		_timer = 0;
		var timeModifier = jobController.printerStatus.printCount / 100f;
		_timeToNextJob = Random.Range(Math.Min(minTimeToNextJob - timeModifier, 1), Math.Min(maxTimeToNextJob - timeModifier, 2));
	}

	private void GenerateJob(PrintJobEnum jobType) {
		_jobCount++;
		var printJobGameObject = Instantiate(printJobPrefab, printJobContainer.transform);
		printJobGameObject.SetActive(false);
		var printJob = printJobGameObject.GetComponent<PrintJob>();
		var jobDefinition = spriteDictionary.GetJob(jobType);
		printJob.sprite = jobDefinition.value;
		var titles = jobDefinition.titles;
		printJob.jobName = titles[Random.Range(0, titles.Count)];
		printJob.fileName = $"{jobType.ToString()}{_jobCount}.{_fileExtensions[Random.Range(0, _fileExtensions.Length - 1)]}";
		printJob.moralityModifier = Random.Range(jobDefinition.minMoralModifier, jobDefinition.maxMoralModifier);
		printJob.satisfactionModifier = 1f;
		printJob.cyan = Random.Range(jobDefinition.minCyan, jobDefinition.maxCyan);
		printJob.magenta = Random.Range(jobDefinition.minMagenta, jobDefinition.maxMagenta);
		printJob.yellow = Random.Range(jobDefinition.minYellow, jobDefinition.maxYellow);
		printJob.black = Random.Range(jobDefinition.minBlack, jobDefinition.maxBlack);
		printJob.paper = Random.Range(jobDefinition.minPages, jobDefinition.maxPages);
		printJob.illegal = jobDefinition.illegal;
		jobController.AddJobToQueue(printJob);
	}
}