using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine;

public class LeaderboardTable : MonoBehaviour {
	public GameObject leaderboardRowPrefab;
	public GameObject leaderboardRowContainer;
	public GameObject loadingText;
	public string leaderboardId = "Morally_Tainted";

	private bool _isLoaded;

	private void Update() {
		if (_isLoaded || !AuthenticationService.Instance.IsSignedIn) return;
		_isLoaded = true;
		loadingText.SetActive(false);
		LoadLeaderboard();
	}

	private async void LoadLeaderboard() {
		var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId);
		scoresResponse.Results.ForEach(entry => {
			Instantiate(leaderboardRowPrefab, leaderboardRowContainer.transform).GetComponent<LeaderboardRow>()
				.SetEntry(entry);
		});
	}
}