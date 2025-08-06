using System.Collections.Generic;
using UnityEngine;

public enum CharacterTrait
{
	None,
	Neutral,
	Confident,
	Enthusiastic,
	Anxious
}

[CreateAssetMenu(fileName = "CharacterTraitData", menuName = "Data/CharacterTrait", order = 2)]
public class SOCharacterTrait : ScriptableObject
{
	[SerializeField]
	public CharacterTrait characterTrait;

	[SerializeField] [Range(0, 1)]
	public float sereneThreshold = 0.3f;
	[SerializeField] [Range(0, 1)]
	public float sweatyThreshold = 0.5f;
	[SerializeField] [Range(0, 1)]
	public float shakyThreshold = 0.8f;
	[SerializeField] [Range(0, 1)]
	public float exhaustedThreshold = 1.0f;
}
