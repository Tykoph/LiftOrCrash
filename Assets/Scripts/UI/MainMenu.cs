using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private Button startGameButton;

	private void Start()
	{
		startGameButton.onClick.AddListener(OnStartGameButtonClicked);
	}

	private static void OnStartGameButtonClicked()
	{
		GameManager.GMInstance.StartGame();
	}
}
