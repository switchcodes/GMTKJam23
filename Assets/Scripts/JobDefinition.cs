using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JobDefinition {
	public PrintJobEnum key;
	public Sprite value;
	public List<String> titles;
	public float minMoralModifier;
	public float maxMoralModifier;
	public int minPages;
	public int maxPages;
	public int minCyan;
	public int maxCyan;
	public int minMagenta;
	public int maxMagenta;
	public int minYellow;
	public int maxYellow;
	public int minBlack;
	public int maxBlack;
	public bool illegal;
}