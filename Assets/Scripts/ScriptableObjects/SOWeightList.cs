using UnityEngine;

[CreateAssetMenu(fileName = "WeightListData", menuName = "Data/List/WeightList", order = 1)]
public class SOWeightList : ScriptableObject
{
	[SerializeField]
	public SOWeight[] weightList;
}
