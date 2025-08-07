using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI gainText;
	[SerializeField]
	private TextMeshProUGUI weightText;
	[SerializeField]
	private Button continueButton;
	[SerializeField]
	private Image unlockedItemImage;

	private void Start()
	{
		continueButton.onClick.AddListener(OnContinueButtonClicked);
	}

	private void OnEnable()
	{
		gainText.text = GameManager.GMInstance.GetCurrentGainString();
		weightText.text = GameManager.GMInstance.liftCharacter.CurrentLiftWeight + " kg";
		unlockedItemImage.sprite = GameManager.GMInstance.UnlockedItemIcon;
	}

	private void OnContinueButtonClicked()
	{
		GameManager.GMInstance.ReturnToMainMenu();
	}
}
