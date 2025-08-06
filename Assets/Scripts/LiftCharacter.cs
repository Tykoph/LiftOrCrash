using UnityEngine;
using Random = UnityEngine.Random;

public class LiftCharacter : MonoBehaviour
{
	private int standardLiftWeight = 1500;
	private int MaxLiftWeight => standardLiftWeight + boostValue;
	private float currentLiftWeight;
	public float PercentageLifted => currentLiftWeight / standardLiftWeight;

	public bool HaveBoost { get; private set; } = false;
	private int boostValue;

	[SerializeField]
	private SOCharacterTraitList soCharacterTraitList;
	public SOCharacterTrait SOCharacterTrait { get; private set; }

	[SerializeField]
	private SOCharacterEmotionList soCharacterEmotionList;
	public SOCharacterEmotion SOCharacterEmotion { get; private set; }

	public void AddLiftWeight(int weight)
	{
		currentLiftWeight += weight;
		UIManager.UIInstance.UpdateGoalBar();
		UpdateEmotionMeter();

		if (currentLiftWeight <= MaxLiftWeight && currentLiftWeight >= standardLiftWeight)
		{
			print($"Winner with weight: {currentLiftWeight} for a max lift of: {MaxLiftWeight}");
			GameManager.GMInstance.WinGame();
		}
		else if (currentLiftWeight > MaxLiftWeight)
		{
			print($"Looser with weight: {currentLiftWeight} for a max lift of: {MaxLiftWeight}");
			GameManager.GMInstance.LooseGame();
		}
	}

	public void AddBoost(int boostAmount)
	{
		if (HaveBoost) return;

		boostValue += boostAmount;
		HaveBoost = true;
		UpdateEmotionMeter();
	}

	public void GenerateCharacter()
	{
		int randomValue = Random.Range(1500, 3001);
		int roundedValue = Mathf.RoundToInt(randomValue / 10f) * 10; // Round to nearest 10
		standardLiftWeight = roundedValue;
		currentLiftWeight = 0f;

		boostValue = 0;
		HaveBoost = false;

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
			if (MaxLiftWeight <= currentLiftWeight)
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
