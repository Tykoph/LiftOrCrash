using UnityEngine;

public enum Tier
{
	Tier0,
	Tier1,
	Tier2,
	Tier3,
}

[CreateAssetMenu(fileName = "WeightData", menuName = "Data/WeightData", order = 1)]
public class SOWeight : ScriptableObject
{
	[SerializeField]
	public Texture weightIcon;
	[SerializeField]
	public Tier weightTier;
	[SerializeField]
	public int weightValue;
	[SerializeField] [Range(0, 1)]
	public float weightSpawnChance;
}
