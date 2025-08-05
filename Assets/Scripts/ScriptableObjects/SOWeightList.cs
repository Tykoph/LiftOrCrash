using UnityEngine;

[CreateAssetMenu(fileName = "WeightListData", menuName = "Data/List/WeightList", order = 2)]
public class SOWeightList : ScriptableObject
{
	[SerializeField]
	public SOWeight[] weightList;
}
