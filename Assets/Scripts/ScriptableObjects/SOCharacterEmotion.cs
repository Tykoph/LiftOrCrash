using System;
using UnityEngine;

public enum CharacterEmotion
{
	None,
	Serene,
	Sweaty,
	Shaky,
	Exhausted,
	Defeated,
	Winner
}

public enum FaceAnimationList
{
	Idle1 = 1,
	Idle2 = 2,
	Idle3 = 3,
	Unconscious = 4,
	Pain = 5,
	Fear1 = 6,
	Fear2 = 7,
	Sleeping = 8,
	Angry = 9,
	Reckless = 10,
	Fever = 11,
	Tearful = 12,
	Scream = 13,
	Relaxed = 14,
	HeartEyes = 15
}

[CreateAssetMenu(fileName = "CharacterEmotionData", menuName = "Data/CharacterEmotion", order = 3)]
public class SOCharacterEmotion : ScriptableObject
{
	[SerializeField]
	public CharacterEmotion characterEmotion;

	[SerializeField]
	public FaceAnimationList faceAnimation;

	[SerializeField]
	public Animator bodyAnimation;

}
