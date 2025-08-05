using System.Globalization;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	[SerializeField]
	public ChoiceMenu choiceMenu;
	[SerializeField]
	public GoalBar goalBar;
	[SerializeField]
	public GameObject cashoutMenu;
	[SerializeField]
	public TextMeshProUGUI currentGainText;

	private void SetCurrentGainText()
	{
		currentGainText.text = GameManager.GMInstance.CurrentGain.ToString(CultureInfo.InvariantCulture);
	}

	public void StartGame()
	{
		SetCurrentGainText();
		cashoutMenu.SetActive(false);
		choiceMenu.gameObject.SetActive(true);
		goalBar.gameObject.SetActive(true);
		NextRound();
	}

	public void NextRound()
	{
		choiceMenu.NextRound();
	}
}
