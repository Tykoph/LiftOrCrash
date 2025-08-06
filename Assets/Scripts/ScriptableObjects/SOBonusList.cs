using UnityEngine;

[CreateAssetMenu(fileName = "BonusListData", menuName = "Data/List/BonusList", order = 0)]
public class SOBonusList : ScriptableObject
{
	[SerializeField]
	public SOBonus[] bonusList;
}
