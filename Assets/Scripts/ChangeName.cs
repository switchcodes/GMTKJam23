using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class ChangeName : MonoBehaviour {
	private async void Awake() {
		await UnityServices.InitializeAsync();
		if (!AuthenticationService.Instance.IsSignedIn) {
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
		}
	}

	public async void SetPlayerName(string playerName) {
		await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
	}
}