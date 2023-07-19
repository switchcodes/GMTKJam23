using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using static System.String;

public class ChangeName : MonoBehaviour {

	public TextMeshProUGUI playerNameText;
	private TouchScreenKeyboard _keyboard;
	
	private async void Awake() {
		await UnityServices.InitializeAsync();
		if (!AuthenticationService.Instance.IsSignedIn) {
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
		}
		playerNameText.text = AuthenticationService.Instance.PlayerName;
	}

	public async void SetPlayerName(string playerName) {
		if (IsNullOrEmpty(playerName)) return;
		await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
	}

	public void OpenKeyboard() {
		Debug.Log("Open keyboard");
		TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
	}
}