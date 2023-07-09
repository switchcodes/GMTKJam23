using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class LeaderboardRow : MonoBehaviour {
	public TextMeshProUGUI rankTMP;
	public TextMeshProUGUI usernameTMP;
	public TextMeshProUGUI scoreTMP;

	public void SetEntry(LeaderboardEntry leaderboardEntry) {
		rankTMP.text = leaderboardEntry.Rank + "";
		usernameTMP.text = leaderboardEntry.PlayerName;
		scoreTMP.text = leaderboardEntry.Score + "";
	}
}