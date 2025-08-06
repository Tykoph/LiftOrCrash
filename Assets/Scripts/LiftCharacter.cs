using UnityEngine;
using Random = UnityEngine.Random;

public class LiftCharacter : MonoBehaviour
{
	public delegate void LiftUpdate();
	public event LiftUpdate OnLiftUpdate;

	private int standardLiftWeight = 1500;
	public int MaxLiftWeight => standardLiftWeight + boostValue;

	public int CurrentLiftWeight { get; private set; }
	public float PercentageLifted => (float)CurrentLiftWeight / standardLiftWeight;

	public bool HaveBoost { get; private set; } = false;
	private int boostValue;

	[SerializeField]
	private SOCharacterTraitList soCharacterTraitList;
	public SOCharacterTrait SOCharacterTrait { get; private set; }

	[SerializeField]
	private SOCharacterEmotionList soCharacterEmotionList;
	public SOCharacterEmotion SOCharacterEmotion { get; private set; }

	[SerializeField]
	private EmotionChanger emotionChanger;

	public void AddLiftWeight(int weight)
	{
		CurrentLiftWeight += weight;
		OnLiftUpdate?.Invoke();
		UpdateEmotionMeter();
		CheckWinCondition();
	}

	private void CheckWinCondition()
	{
		if (CurrentLiftWeight <= MaxLiftWeight && CurrentLiftWeight >= standardLiftWeight)
		{
			print($"Winner with weight: {CurrentLiftWeight} for a max lift of: {MaxLiftWeight}");
			GameManager.GMInstance.WinGame();
		}
		else if (CurrentLiftWeight > MaxLiftWeight)
		{
			print($"Looser with weight: {CurrentLiftWeight} for a max lift of: {MaxLiftWeight}");
			GameManager.GMInstance.LoseGame();
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
		CurrentLiftWeight = 0;

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
			if (MaxLiftWeight <= CurrentLiftWeight)
			{
				SOCharacterEmotion = soCharacterEmotionList.defeatedEmotion;
			}
			else
			{
				SOCharacterEmotion = soCharacterEmotionList.winnerEmotion;
			}
		}

		emotionChanger.ChangeEmotion(SOCharacterEmotion.characterEmotion);
	}
}
