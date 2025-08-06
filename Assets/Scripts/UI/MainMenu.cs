using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private Button startGameButton;
	[SerializeField]
	private TextMeshProUGUI balanceValueText;

	private void Start()
	{
		startGameButton.onClick.AddListener(OnStartGameButtonClicked);
		UpdateBalanceValue();
	}

	private void OnEnable()
	{
		UpdateBalanceValue();
	}

	private void UpdateBalanceValue()
	{
		balanceValueText.text = GameManager.GMInstance.GetMoneyString();
	}

	private static void OnStartGameButtonClicked()
	{
		GameManager.GMInstance.StartGame();
	}
}
