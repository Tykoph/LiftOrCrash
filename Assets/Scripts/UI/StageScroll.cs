using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageScroll : MonoBehaviour
{
	[SerializeField]
	private RectTransform content;

	[SerializeField]
	private float yPosOne = 0f;
	[SerializeField]
	private float yPosTwo = 100f;

	[SerializeField]
	private RawImage[] stageImages = new RawImage[20];

	[SerializeField]
	private Texture notCompletedTexture;
	[SerializeField]
	private Texture completedTexture;

	private float currentYPos;
	private int currentStageIndex = 0;

	public void StartGame()
	{
		content.localPosition = new Vector3(0, yPosOne, 0);
		foreach (RawImage stageImage in stageImages)
		{
			stageImage.texture = notCompletedTexture;
		}

		GameManager.GMInstance.OnBetUpdate += UpdateGainValue;
		UpdateGainValue();
	}

	private void UpdateGainValue()
	{
		for (int i = currentStageIndex; i < stageImages.Length; i++)
		{
			stageImages[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GMInstance.GetStageGain(i).ToString("F1") + "$";
		}
	}

	public void ScrollStages(float newValue)
	{
		newValue = newValue switch
		{
			> 1f => 1f,
			< 0f => 0f,
			_ => newValue
		};

		currentStageIndex = Mathf.RoundToInt(newValue * stageImages.Length);
		GameManager.GMInstance.AddStagePassed(currentStageIndex);
		for (var i = 0; i < currentStageIndex; i++)
		{
			stageImages[i].texture = completedTexture;
		}

		currentYPos = Mathf.Lerp(yPosOne, yPosTwo, newValue);

		content.DOLocalMoveY(currentYPos, 1.5f);
	}
}
