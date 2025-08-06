using UnityEngine;
using Random = UnityEngine.Random;

public class LiftCharacter : MonoBehaviour
{
	private int standardLiftWeight = 1500;
	public int MaxLiftWeight => standardLiftWeight + BoostAmount;
	public float CurrentLiftWeight { get; private set; } = 0f;
	public float PercentageLifted => CurrentLiftWeight / standardLiftWeight;

	public bool HaveBoost { get; private set; } = false;
	public int BoostAmount { get; private set; } = 0;

	[SerializeField]
	private SOCharacterTraitList soCharacterTraitList;
	public SOCharacterTrait SOCharacterTrait { get; private set; }

	[SerializeField]
	private SOCharacterEmotionList soCharacterEmotionList;
	public SOCharacterEmotion SOCharacterEmotion { get; private set; }

	public void AddLiftWeight(int weight)
	{
		CurrentLiftWeight += weight;
		UIManager.UIInstance.UpdateGoalBar();
		UpdateEmotionMeter();

		if (CurrentLiftWeight <= MaxLiftWeight && CurrentLiftWeight <= standardLiftWeight)
		{
			GameManager.GMInstance.WinGame();
		}
		else if (CurrentLiftWeight > MaxLiftWeight)
		{
			GameManager.GMInstance.LooseGame();
		}
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
		int randomValue = Random.Range(1500, 3001);
		int roundedValue = Mathf.RoundToInt(randomValue / 10f) * 10; // Round to nearest 10
		standardLiftWeight = roundedValue;
		print($"Generated Lift Weight: {standardLiftWeight}, for a random value of {randomValue}");
		CurrentLiftWeight = 0f;

		int randomTrait = Random.Range(0, soCharacterTraitList.characterTraitList.Length);
		SOCharacterTrait = soCharacterTraitList.characterTraitList[randomTrait];

		UpdateEmotionMeter();
	}

	private void UpdateEmotionMeter()
	{
		if (PercentageLifted < SOCharacterTrait.sereneThreshold)
		{
			SOCharacterEmotion = soCharacterEmotionList.sereneEmotion;
		}
		else if (PercentageLifted < SOCharacterTrait.sweatyThreshold)
		{
			SOCharacterEmotion = soCharacterEmotionList.sweatyEmotion;
		}
		else if (PercentageLifted < SOCharacterTrait.shakyThreshold)
		{
			SOCharacterEmotion = soCharacterEmotionList.shakyEmotion;
		}
		else if (PercentageLifted < SOCharacterTrait.exhaustedThreshold)
		{
			SOCharacterEmotion = soCharacterEmotionList.exhaustedEmotion;
		}
		else if (PercentageLifted >= SOCharacterTrait.exhaustedThreshold)
		{
			if (MaxLiftWeight <= CurrentLiftWeight)
			{
				SOCharacterEmotion = soCharacterEmotionList.defeatedEmotion;
			}
			else
			{
				SOCharacterEmotion = soCharacterEmotionList.winnerEmotion;
			}
		}
	}
}
