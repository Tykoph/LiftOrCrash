using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[SerializeField]
	public ChoiceMenu choiceMenu;
	[SerializeField]
	public GoalBar goalBar;
	[SerializeField]
	public TextMeshProUGUI currentGainText;
	[SerializeField]
	private Button addBetButton;
	[SerializeField]
	private TextMeshProUGUI betAmountText;
	[SerializeField]
	private TextMeshProUGUI moneyText;
	[SerializeField]
	private Button wardrobeButton;

	private void Start()
	{
		addBetButton.onClick.AddListener(AddBetAmount);
		wardrobeButton.onClick.AddListener(() => UIManager.UIInstance.OpenWardrobeMenu());
		GameManager.GMInstance.OnMoneyUpdate += UpdateMoney;
		GameManager.GMInstance.OnGainUpdate += SetCurrentGainText;
		UpdateMoney();
		SetCurrentGainText();
		betAmountText.text = GameManager.GMInstance.GetBetAmountString();
	}

	public void StartGame()
	{
		SetCurrentGainText();
		choiceMenu.gameObject.SetActive(true);
		goalBar.gameObject.SetActive(true);
		goalBar.StartGame();
		NextRound();
		betAmountText.text = GameManager.GMInstance.GetBetAmountString();
	}

	public void NextRound()
	{
		choiceMenu.NextRound();
	}

	private void AddBetAmount()
	{
		GameManager.GMInstance.AddBetAmount(1);
		betAmountText.text = GameManager.GMInstance.GetBetAmountString();
	}

	private void UpdateMoney()
	{
		moneyText.text = GameManager.GMInstance.GetMoneyString();
	}

	private void SetCurrentGainText()
	{
		currentGainText.text = GameManager.GMInstance.GetCurrentGainString();
	}
}
