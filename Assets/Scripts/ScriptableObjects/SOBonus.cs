using UnityEngine;


[CreateAssetMenu(fileName = "BonusData", menuName = "Data/BonusObject", order = 0)]
public class SOBonus : ScriptableObject
{
	[SerializeField]
	public string bonusName;
	[SerializeField]
	public Texture bonusIcon;
	[SerializeField]
	public int bonusValue;
	[SerializeField] [Range(0, 1)]
	public float bonusSpawnChance;
}
