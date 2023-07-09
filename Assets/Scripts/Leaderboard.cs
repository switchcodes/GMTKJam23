using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

public class Leaderboard : MonoBehaviour {
	
	public TextMeshProUGUI playerNameText;
	public TextMeshProUGUI highScoreText;
	
	private const string LeaderboardId = "Morally_Tainted";
	
	private async void Awake() {
		await UnityServices.InitializeAsync();
		if (!AuthenticationService.Instance.IsSignedIn) {
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
		}
		playerNameText.text = "Player: " + AuthenticationService.Instance.PlayerName;
		var scoresResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
		highScoreText.text = $"Highscore: <color=green>{scoresResponse.Score}</color>";
	}

	public async void SetPlayerName(string playerName) {
		await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
		playerNameText.text = "Player: " + AuthenticationService.Instance.PlayerName;
	}

	public async void SubmitScore(int score) {
		await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
	}
}