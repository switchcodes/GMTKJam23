using System;
using UnityEngine;

public class MoralMeter : MonoBehaviour {
		public float moralLevel = 50;
		public float satisfactionLevel = 50;

		public RectTransform moralCompassContainer;
		public RectTransform moralCompassNeedle;

		public Logger logger;
		
		private float _previousMoralLevel;
		private float _previousSatisfactionLevel;

		private const string SatisfactionDownUnder10 = "<color=red>Warn:</color> SFN < 10%";
		private const string SatisfactionDownUnder25 = "<color=red>Warn:</color> SFN < 25%";
		private const string SatisfactionDownUnder50 = "<color=red>Warn:</color> SFN < 25%";

		private const string SatisfactionUpOver90 = "<color=green>Note:</color> SFN > 90%";
		private const string SatisfactionUpOver50 = "<color=green>Note:</color> SFN > 50%";
		private const string SatisfactionUpOver25 = "<color=green>Note:</color> SFN > 25%";

		private const string MoralDownUnder10 = "<color=red>Warn:</color> MRL < 10%";
		private const string MoralDownUnder25 = "<color=red>Warn:</color> MRL < 25%";
		private const string MoralDownUnder50 = "<color=red>Warn:</color> MRL < 50%";

		private const string MoralUpOver90 = "<color=green>Note:</color> MRL > 90%";
		private const string MoralUpOver50 = "<color=green>Note:</color> MRL > 50%";
		private const string MoralUpOver25 = "<color=green>Note:</color> MRL > 25%";

		private string _lastMoralMessage = "";
		private string _lastSatisfactionMessage = "";
		
		private void Update() {
			var rect = moralCompassContainer.rect;
			var posY = rect.height / 100f * (moralLevel - 50);
			var posX = rect.width / 100f * (satisfactionLevel - 50);

			var needleRect = moralCompassNeedle.rect;
			
			moralCompassNeedle.localPosition = new Vector3(posX, posY, 0);
			
			
			if (moralLevel < 0) {
				moralLevel = 0;
			}
			if (moralLevel > 100) {
				moralLevel = 100;
			}
			if (satisfactionLevel < 0) {
				satisfactionLevel = 0;
			}
			if (satisfactionLevel > 100) {
				satisfactionLevel = 100;
			}

			if (_previousMoralLevel > moralLevel) {
				switch (moralLevel) {
					case < 10:
						if (_lastMoralMessage != MoralDownUnder10) {
							logger.Log(MoralDownUnder10);
							_lastMoralMessage = MoralDownUnder10;
						}
						break;
					case < 25:
						if (_lastMoralMessage != MoralDownUnder25) {
							logger.Log(MoralDownUnder25);
							_lastMoralMessage = MoralDownUnder25;
						}
						break;
					case < 50:
						if (_lastMoralMessage != MoralDownUnder50) {
							logger.Log(MoralDownUnder50);
							_lastMoralMessage = MoralDownUnder50;
						}
						break;
				}
			}
			else if (_previousMoralLevel < moralLevel) {
				switch (moralLevel) {
					case > 90:
						if (_lastMoralMessage != MoralUpOver90) {
							logger.Log(MoralUpOver90);
							_lastMoralMessage = MoralUpOver90;
						}
						break;
					case > 50:
						if (_lastMoralMessage != MoralUpOver50) {
							logger.Log(MoralUpOver50);
							_lastMoralMessage = MoralUpOver50;
						}
						break;
					case > 25:
						if (_lastMoralMessage != MoralUpOver25) {
							logger.Log(MoralUpOver25);
							_lastMoralMessage = MoralUpOver25;
						}
						break;
				}
			}

			if (_previousSatisfactionLevel > satisfactionLevel) {
				switch (satisfactionLevel) {
					case < 10:
						if (_lastSatisfactionMessage != SatisfactionDownUnder10) {
							logger.Log(SatisfactionDownUnder10);
							_lastSatisfactionMessage = SatisfactionDownUnder10;
						}
						break;
					case < 25:
						if (_lastSatisfactionMessage != SatisfactionDownUnder25) {
							logger.Log(SatisfactionDownUnder25);
							_lastSatisfactionMessage = SatisfactionDownUnder25;
						}
						break;
					case < 50:
						if (_lastSatisfactionMessage != SatisfactionDownUnder50) {
							logger.Log(SatisfactionDownUnder50);
							_lastSatisfactionMessage = SatisfactionDownUnder50;
						}
						break;
				}
			}
			else if (_previousSatisfactionLevel < satisfactionLevel) {
				switch (satisfactionLevel) {
					case > 90:
						if (_lastSatisfactionMessage != SatisfactionUpOver90) {
							logger.Log(SatisfactionUpOver90);
							_lastSatisfactionMessage = SatisfactionUpOver90;
						}
						break;
					case > 50:
						if (_lastSatisfactionMessage != SatisfactionUpOver50) {
							logger.Log(SatisfactionUpOver50);
							_lastSatisfactionMessage = SatisfactionUpOver50;
						}
						break;
					case > 25:
						if (_lastSatisfactionMessage != SatisfactionUpOver25) {
							logger.Log(SatisfactionUpOver25);
							_lastSatisfactionMessage = SatisfactionUpOver25;
						}
						break;
				}
			}
			
			_previousMoralLevel = moralLevel;
			_previousSatisfactionLevel = satisfactionLevel;
		}
}