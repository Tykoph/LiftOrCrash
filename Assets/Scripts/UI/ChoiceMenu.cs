using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChoiceMenu : MonoBehaviour
{
	[SerializeField] public List<int> liftChoices = new List<int>
	{
		10, 20, 50, 75, 100, 150, 200, 300, 500
	};

	[SerializeField]
	public SOWeightList soWeightList;

	[SerializeField]
	public SOBonusList soBonusList;

	[SerializeField]
	public Button[] liftChoiceButtons;
	[SerializeField]
	public Button bonusButton;

	private int[] liftWeights = new int[]{0,0,0};
	private int bonusValue;

	[SerializeField]
	public float bonusButtonSpawnChance = 0.1f;

	private float totalWeightProbability = 0f;
	private float totalBonusProbability = 0f;

	private void Start()
	{
		liftChoiceButtons[0].onClick.AddListener(OnLeftButtonClicked);
		liftChoiceButtons[1].onClick.AddListener(OnMiddleButtonClicked);
		liftChoiceButtons[2].onClick.AddListener(OnRightButtonClicked);
		bonusButton.onClick.AddListener(OnBonusButtonClicked);

		foreach (SOWeight weight in soWeightList.weightList)
		{
			totalWeightProbability += weight.weightSpawnChance;
		}

		foreach (SOBonus bonus in soBonusList.bonusList)
		{
			totalBonusProbability += bonus.bonusSpawnChance;
		}
	}

	private SOWeight CalculateWeightProbability()
	{
		SOWeight selectedWeight = null;
		float cumulativeWeight = 0f;
		float randomWeight = Random.Range(0f, totalWeightProbability);

		foreach (SOWeight weight in soWeightList.weightList)
		{
			cumulativeWeight += weight.weightSpawnChance;
			if (!(randomWeight <= cumulativeWeight)) continue;
			selectedWeight = weight;
			break;
		}

		return selectedWeight;
	}

	private SOBonus CalculateBonusProbability()
	{
		SOBonus selectedBonus = null;
		float cumulativeWeight = 0f;
		float randomWeight = Random.Range(0f, totalBonusProbability);

		foreach (SOBonus bonus in soBonusList.bonusList)
		{
			cumulativeWeight += bonus.bonusSpawnChance;
			if (!(randomWeight <= cumulativeWeight)) continue;
			selectedBonus = bonus;
			break;
		}

		return selectedBonus;
	}

	private void GenerateChoices()
	{
		int buttonId = 0;
		bool spawnBonusButton = false;
		if (!GameManager.GMInstance.liftCharacter.HaveBoost)
		{
			float randomSpawn = Random.value;
			spawnBonusButton = randomSpawn < bonusButtonSpawnChance;
			print("Random Spawn: " + randomSpawn + ", Bonus Button Spawn: " + spawnBonusButton);
		}

		foreach (Button button in liftChoiceButtons)
		{
			SOWeight weight = CalculateWeightProbability();
			button.GetComponentInChildren<TextMeshProUGUI>().text = weight.weightValue + " kG";
			button.GetComponentInChildren<RawImage>().texture = weight.weightIcon;
			var buttonImage = button.GetComponent<Image>();
			buttonImage.color = weight.weightTier switch
			{
				Tier.Tier0 => Color.cornflowerBlue,
				Tier.Tier1 => Color.green,
				Tier.Tier2 => Color.yellow,
				Tier.Tier3 => Color.red,
				_ => Color.white
			};
			liftWeights[buttonId] = weight.weightValue;
			buttonId++;
		}

		if (!spawnBonusButton) return;
		liftChoiceButtons[1].gameObject.SetActive(false);
		SOBonus bonus = CalculateBonusProbability();
		bonusButton.gameObject.SetActive(true);
		bonusButton.GetComponentInChildren<RawImage>().texture = bonus.bonusIcon;
		bonusValue = bonus.bonusValue;
	}

	public void NextRound()
	{
		foreach (Button button in liftChoiceButtons)
		{
			button.gameObject.SetActive(true);
		}
		bonusButton.gameObject.SetActive(false);
		GenerateChoices();
	}

	private void OnLeftButtonClicked()
	{
		GameManager.GMInstance.liftCharacter.AddLiftWeight(liftWeights[0]);
		print("Added Left weight: " + liftWeights[0]);
		UIManager.UIInstance.NextRound();
	}
	
	private void OnMiddleButtonClicked()
	{
		GameManager.GMInstance.liftCharacter.AddLiftWeight(liftWeights[1]);
		print("Added Middle weight: " + liftWeights[1]);
		UIManager.UIInstance.NextRound();
	}
	
	private void OnRightButtonClicked()
	{
		GameManager.GMInstance.liftCharacter.AddLiftWeight(liftWeights[2]);
		print("Added Right weight: " + liftWeights[2]);
		UIManager.UIInstance.NextRound();
	}

	private void OnBonusButtonClicked()
	{
		GameManager.GMInstance.liftCharacter.AddBoost(bonusValue);
		print("Used bonus: " + bonusValue);
		UIManager.UIInstance.NextRound();
	}
}