using TMPro;
using UnityEngine;
using static SceneChangeInfo;
using static SceneChangeInfo.DifficultyEnum;

public class ShowScores : MonoBehaviour {
	public TextMeshProUGUI scoreTMP;
	public TextMeshProUGUI statsTMP;

	// Start is called before the first frame update
	void Start() {
		var moral = Moral switch {
			> 75 => "<color=green>Ethical</color>",
			> 25 => "<color=yellow>Neutral</color>",
			_ => "<color=red>Unethical</color>"
		};

		var satisfaction = Satisfaction switch {
			> 75 => "<color=green>Happy</color>",
			> 25 => "<color=yellow>Neutral</color>",
			_ => "<color=red>Annoyed</color>"
		};

		var difficulty = Difficulty switch {
			Easy => "<color=green>Easy</color>",
			Normal => "<color=yellow>Normal</color>",
			Hard => "<color=red>Hard</color>",
			_ => "<color=red>Unknown</color>"
		};
		
		scoreTMP.text = $"You printed <color=green>{Score}</color> pages!";
		statsTMP.text =
			$"Morality: {moral}\nSatisfaction: {satisfaction}\nDifficulty: {difficulty}";
	}

	// Update is called once per frame
	void Update() {
	}
}