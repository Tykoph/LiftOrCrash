using UnityEngine;
using UnityEngine.UI;

public class GoalBar : MonoBehaviour
{
	[SerializeField]
	public Slider goalBar;

	public void StartGame()
	{
		goalBar.value = 0f;
	}
}
