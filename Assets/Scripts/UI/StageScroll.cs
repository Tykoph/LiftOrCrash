using System;
using DG.Tweening;
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

	public void StartGame()
	{
		content.localPosition = new Vector3(0, yPosOne, 0);
		foreach (RawImage stageImage in stageImages)
		{
			stageImage.texture = notCompletedTexture;
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

		int stagePassed = Mathf.FloorToInt(newValue * stageImages.Length);
		GameManager.GMInstance.AddStagePassed(stagePassed);
		for (var i = 0; i < stagePassed; i++)
		{
			stageImages[i].texture = completedTexture;
		}

		// if (newValue < 0.5f) return;

		// newValue = (newValue - 0.5f) * 2f;
		currentYPos = Mathf.Lerp(yPosOne, yPosTwo, newValue);

		content.DOLocalMoveY(currentYPos, 1.5f);
	}
}
