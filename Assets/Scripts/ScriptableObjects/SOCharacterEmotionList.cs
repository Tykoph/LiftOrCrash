using UnityEngine;

[CreateAssetMenu(fileName = "CharacterEmotionList", menuName = "Data/List/CharacterEmotionList", order = 3)]
public class SOCharacterEmotionList : ScriptableObject
{
	[SerializeField]
	public SOCharacterEmotion sereneEmotion;
	[SerializeField]
	public SOCharacterEmotion sweatyEmotion;
	[SerializeField]
	public SOCharacterEmotion shakyEmotion;
	[SerializeField]
	public SOCharacterEmotion exhaustedEmotion;
	[SerializeField]
	public SOCharacterEmotion defeatedEmotion;
	[SerializeField]
	public SOCharacterEmotion winnerEmotion;
}
