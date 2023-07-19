using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Exceptions;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using static SceneChangeInfo;
public class Leaderboard : MonoBehaviour {
	
	public TextMeshProUGUI playerNameText;
	public TextMeshProUGUI highScoreText;
	public GameObject newHighScoreText;
	
	public LeaderboardTable leaderboardTable;
	
	private const string LeaderboardId = "Morally_Tainted";
	
	private async void Awake() {
		await UnityServices.InitializeAsync();
		if (!AuthenticationService.Instance.IsSignedIn) {
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
		}

		try {
			var scoresResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
			SetTexts(scoresResponse);
		}
		catch (LeaderboardsException) {
			// user does not have a score yet
			await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, Score);
			var scoresResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
			SetTexts(scoresResponse);
		}
	}

	private void SetTexts(LeaderboardEntry scoresResponse) {
		playerNameText.text = $"Player: <color=#97FF75>{scoresResponse.PlayerName}</color>";
		highScoreText.text = $"Highscore: <color=#97FF75>{scoresResponse.Score}</color>";
		if (!(Score < scoresResponse.Score)) {
			SubmitScore(Score);
			newHighScoreText.SetActive(true);
			highScoreText.text = $"Highscore: <color=#97FF75>{Score}</color>";
		}
		leaderboardTable.StopWaiting();
	}

	public async void SetPlayerName(string playerName) {
		await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
		playerNameText.text = "Player: " + AuthenticationService.Instance.PlayerName;
	}

	public async void SubmitScore(int score) {
		await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
	}
}