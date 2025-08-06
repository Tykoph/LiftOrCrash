using UnityEngine;

[CreateAssetMenu(fileName = "CharacterTraitList", menuName = "Data/List/CharacterTrait", order = 2)]
public class SOCharacterTraitList : ScriptableObject
{
	[SerializeField]
	public SOCharacterTrait[] characterTraitList;
}
