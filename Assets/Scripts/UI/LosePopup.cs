using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class losePopup : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI weightText;
	[SerializeField]
	private Button continueButton;

	private void Start()
	{
		continueButton.onClick.AddListener(OnContinueButtonClicked);
	}

	private void OnEnable()
	{
		weightText.text = (GameManager.GMInstance.liftCharacter.CurrentLiftWeight -
		                   GameManager.GMInstance.liftCharacter.MaxLiftWeight) + " kg";
	}

	private void OnContinueButtonClicked()
	{
		GameManager.GMInstance.ReturnToMainMenu(false);
	}
}
