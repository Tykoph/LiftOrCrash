using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private MainMenu mainMenu;
	[SerializeField]
	private GameUI gameUI;
	[SerializeField]
	private GameObject wardrobeMenu;
	[SerializeField]
	private GameObject cashoutMenu;
	[SerializeField]
	private GameObject winPopup;
	[SerializeField]
	private GameObject losePopup;

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
		wardrobeMenu.SetActive(false);
	}

	public void InitializeUI()
	{
		mainMenu.gameObject.SetActive(true);
		gameUI.gameObject.SetActive(false);
		cashoutMenu.SetActive(false);
		winPopup.SetActive(false);
		losePopup.SetActive(false);
		wardrobeMenu.SetActive(false);
	}

	public void StartGame()
	{
		mainMenu.gameObject.SetActive(false);
		gameUI.gameObject.SetActive(true);
		gameUI.StartGame();
		OpenWardrobeMenu(); // The first time opening the wardrobe menu requires to SetActive 2 time for some reason // This prevents the player from needing to click the button twice
	}

	public void OpenWardrobeMenu()
	{
		wardrobeMenu.SetActive(true);
	}

	public void NextRound()
	{
		if (GameManager.GMInstance.liftCharacter == null) return;
		gameUI.NextRound();
	}

	public void ShowCashoutMenu()
	{
		cashoutMenu.SetActive(true);
	}

	public void HideCashoutMenu()
	{
		cashoutMenu.SetActive(false);
	}

	public void ShowWinPopup()
	{
		winPopup.SetActive(true);
	}

	public void ShowLosePopup()
	{
		losePopup.SetActive(true);
	}
}
