using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CharacterTrait
{
	None,
	Neutral,
	Confident,
	Enthusiastic,
	Anxious
}

public enum CharacterEmotion
{
	None,
	Serene,
	Sweaty,
	Shaky,
	Exhausted,
	Defeated
}

public class LiftCharacter : MonoBehaviour
{
	private int standardLiftWeight = 1000;
	public int MaxLiftWeight => standardLiftWeight + BoostAmount;
	public float CurrentLiftWeight { get; private set; } = 0f;

	public bool HaveBoost { get; private set; } = false;
	public int BoostAmount { get; private set; } = 0;

	public CharacterTrait Trait { get; private set; } = CharacterTrait.None;
	public CharacterEmotion Emotion { get; private set; } = CharacterEmotion.Serene;

	private float PercentageLifted => CurrentLiftWeight / MaxLiftWeight;

	public List<float> emotionThresholds = new List<float>
	{
		0.3f, // Serene
		0.5f, // Sweaty
		0.8f, // Shaky
		1.0f  // Exhausted
	};

	public void AddLiftWeight(int weight)
	{
		CurrentLiftWeight += weight;
		UpdateEmotionMeter();
	}

	public void AddBoost(int boostAmount)
	{
		if (HaveBoost) return;

		BoostAmount += boostAmount;
		HaveBoost = true;
		UpdateEmotionMeter();
	}

	public void GenerateCharacter()
	{
		standardLiftWeight = Random.Range(1000, 2001);
		CurrentLiftWeight = 0f;
		UpdateEmotionMeter();
		Trait = Random.Range(1, 5) switch
		{
			1 => CharacterTrait.Neutral,
			2 => CharacterTrait.Confident,
			3 => CharacterTrait.Enthusiastic,
			4 => CharacterTrait.Anxious,
			_ => CharacterTrait.None
		};
		UpdateEmotionThresholds();
	}

	private void UpdateEmotionThresholds()
	{
		emotionThresholds = Trait switch
		{
			CharacterTrait.Neutral => new List<float> { 0.3f, 0.5f, 0.8f, 1.0f },
			CharacterTrait.Confident => new List<float> { 0.5f, 0.4f, 0.7f, 1.0f },
			CharacterTrait.Enthusiastic => new List<float> { 0.1f, 0.7f, 0.85f, 1.0f },
			CharacterTrait.Anxious => new List<float> { 0.2f, 0.4f, 0.6f, 1.0f },
			_ => emotionThresholds
		};
	}

	private void UpdateEmotionMeter()
	{
		if (PercentageLifted < emotionThresholds[0])
		{
			Emotion = CharacterEmotion.Serene;
		}
		else if (PercentageLifted < emotionThresholds[1])
		{
			Emotion = CharacterEmotion.Sweaty;
		}
		else if (PercentageLifted < emotionThresholds[2])
		{
			Emotion = CharacterEmotion.Shaky;
		}
		else if (PercentageLifted < emotionThresholds[3])
		{
			Emotion = CharacterEmotion.Exhausted;
		}
		else if (PercentageLifted >= emotionThresholds[3])
		{
			Emotion = CharacterEmotion.Defeated;
			GameManager.GMInstance.LooseGame();
		}
		else
		{
			Emotion = CharacterEmotion.None;
		}
	}
}
