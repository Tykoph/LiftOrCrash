using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GoalBar : MonoBehaviour
{
	[SerializeField]
	private Slider goalBar;

	[SerializeField]
	private StageScroll stageScroll;

	public void StartGame()
	{
		stageScroll.StartGame();
		goalBar.value = 0f;
	}

	public void UpdateGoalBar()
	{
		float newValue = GameManager.GMInstance.LiftCharacter.PercentageLifted;

		stageScroll.ScrollStages(newValue);

		switch (newValue)
		{
			case >= 0.5f and < 0.75f:
				goalBar.DOValue(0.5f, 0.5f);
				break;
			case < 0.5f:
				goalBar.DOValue(newValue, 0.5f);
				break;
			case >= 0.75f:
				float customValue = (newValue - 0.5f) * 2f;
				goalBar.DOValue(customValue, 0.5f);
				break;
		}

	}
}
