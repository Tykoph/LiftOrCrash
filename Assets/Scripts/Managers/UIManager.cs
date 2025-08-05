using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	public MainMenu mainMenu;
	[SerializeField]
	public GameUI gameUI;
	[SerializeField]
	public GameObject characterSelectionMenu;

	public static UIManager UIInstance { get; private set; }

	private void Awake()
	{
		if (UIInstance == null)
		{
			UIInstance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void StartGame()
	{
		mainMenu.gameObject.SetActive(false);
		gameUI.gameObject.SetActive(true);
		gameUI.StartGame();
	}

	public void NextRound()
	{
		if (GameManager.GMInstance.LiftCharacter == null) return;
		gameUI.NextRound();
	}
}
