using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;
using static SceneChangeInfo;
public class Leaderboard : MonoBehaviour {
	
	public TextMeshProUGUI playerNameText;
	public TextMeshProUGUI highScoreText;
	public GameObject newHighScoreText;
	
	private const string LeaderboardId = "Morally_Tainted";
	
	private async void Awake() {
		await UnityServices.InitializeAsync();
		if (!AuthenticationService.Instance.IsSignedIn) {
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
		}
		var scoresResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
		playerNameText.text = $"Player: <color=#97FF75>{scoresResponse.PlayerName}</color>";
		highScoreText.text = $"Highscore: <color=#97FF75>{scoresResponse.Score}</color>";
		if (!(Score > scoresResponse.Score)) return;
		SubmitScore(Score);
		newHighScoreText.SetActive(true);
	}

	public async void SetPlayerName(string playerName) {
		await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
		playerNameText.text = "Player: " + AuthenticationService.Instance.PlayerName;
	}

	public async void SubmitScore(int score) {
		await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
	}
}