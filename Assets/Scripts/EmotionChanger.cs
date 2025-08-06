using UnityEngine;

public class EmotionChanger : MonoBehaviour
{
	[SerializeField]
	private GameObject noneEmotion;
	[SerializeField]
	private GameObject sereneEmotion;
	[SerializeField]
	private GameObject sweatyEmotion;
	[SerializeField]
	private GameObject shakyEmotion;
	[SerializeField]
	private GameObject exhaustedEmotion;
	[SerializeField]
	private GameObject defeatedEmotion;
	[SerializeField]
	private GameObject winnerEmotion;

	public void ChangeEmotion(CharacterEmotion emotion)
	{
		noneEmotion.SetActive(emotion == CharacterEmotion.None);
		sereneEmotion.SetActive(emotion == CharacterEmotion.Serene);
		sweatyEmotion.SetActive(emotion == CharacterEmotion.Sweaty);
		shakyEmotion.SetActive(emotion == CharacterEmotion.Shaky);
		exhaustedEmotion.SetActive(emotion == CharacterEmotion.Exhausted);
		defeatedEmotion.SetActive(emotion == CharacterEmotion.Defeated);
		winnerEmotion.SetActive(emotion == CharacterEmotion.Winner);
	}
}
