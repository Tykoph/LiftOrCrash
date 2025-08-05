using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CashoutMenu : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI levelValue;

	[SerializeField]
	private Button continueButton;

	[SerializeField]
	private Button cashoutButton;

	[SerializeField]
	private TextMeshProUGUI cashoutValueText;

	private void Start()
	{
		continueButton.onClick.AddListener(OnContinueButtonClicked);
		cashoutButton.onClick.AddListener(OnCashoutButtonClicked);
	}

	private void OnEnable()
	{
		levelValue.text = GameManager.GMInstance.StagePassed.ToString();
		cashoutValueText.text = GameManager.GMInstance.CurrentGain.ToString("F2");
	}

	private void OnContinueButtonClicked()
	{
		gameObject.SetActive(false);
	}

	private void OnCashoutButtonClicked()
	{
		GameManager.GMInstance.Cashout();
		gameObject.SetActive(false);
	}
}
