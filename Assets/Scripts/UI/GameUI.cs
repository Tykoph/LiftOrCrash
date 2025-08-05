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

	public void SetCurrentGainText()
	{
		currentGainText.text = GameManager.GMInstance.CurrentGain.ToString("F2");
	}

	public void StartGame()
	{
		SetCurrentGainText();
		cashoutMenu.SetActive(false);
		choiceMenu.gameObject.SetActive(true);
		goalBar.gameObject.SetActive(true);
		goalBar.StartGame();
		NextRound();
	}

	public void NextRound()
	{
		choiceMenu.NextRound();
	}
}
