using UnityEngine;


[CreateAssetMenu(fileName = "WeightData", menuName = "Data/WeightData", order = 1)]
public class SOWeight : ScriptableObject
{
	[SerializeField]
	public Sprite weightIcon;
	[SerializeField]
	public int weightValue;
	[SerializeField] [Range(0, 1)]
	public float weightSpawnChance;
}
