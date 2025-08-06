using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private MainMenu mainMenu;
	[SerializeField]
	private GameUI gameUI;
	[SerializeField]
	private GameObject characterSelectionMenu;

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

		mainMenu.gameObject.SetActive(false);
		gameUI.gameObject.SetActive(false);
		// characterSelectionMenu.SetActive(false);
	}

	public void InitializeUI()
	{
		mainMenu.gameObject.SetActive(true);
		gameUI.gameObject.SetActive(false);
		// characterSelectionMenu.SetActive(false);
	}

	public void CashoutMenu(bool activate)
	{
		gameUI.cashoutMenu.SetActive(activate);
	}

	public void StartGame()
	{
		mainMenu.gameObject.SetActive(false);
		gameUI.gameObject.SetActive(true);
		gameUI.StartGame();
	}

	public void NextRound()
	{
		if (GameManager.GMInstance.liftCharacter == null) return;
		gameUI.NextRound();
	}

	public void UpdateGoalBar()
	{
		gameUI.goalBar.UpdateGoalBar();
	}
}
