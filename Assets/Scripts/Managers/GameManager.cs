using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float Money { get; private set; } = 0f;
	public float CurrentGain { get; private set; } = 0f;
	public float BetAmount { get; private set; } = 1f;

	[SerializeField]
	public GameObject characterPrefab;
	public LiftCharacter LiftCharacter { get; private set; }

	public static GameManager GMInstance { get; private set; }

	private void Awake()
	{
		if (GMInstance == null)
		{
			GMInstance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		InitializeGame();
	}

	public void AddMoney(float amount)
	{
		Money += amount;
	}

	public void AddCurrentGain(float amount)
	{
		CurrentGain += amount;
	}

	public void AddBetAmount(float amount)
	{
		BetAmount += amount;
	}

	public void StartGame()
	{
		CurrentGain = 0f;
		GenerateCharacter();
		UIManager.UIInstance.StartGame();
	}

	private void GenerateCharacter()
	{
		GameObject newCharacter = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity);
		LiftCharacter = newCharacter.GetComponent<LiftCharacter>();
		LiftCharacter.GenerateCharacter();
	}

	private void InitializeGame()
	{
		UIManager.UIInstance.mainMenu.gameObject.SetActive(true);
		UIManager.UIInstance.gameUI.gameObject.SetActive(false);
		// UIManager.UIInstance.characterSelectionMenu.SetActive(false);
	}

	public void LooseGame()
	{
		print("You loosed the game!");
		Destroy(LiftCharacter.gameObject);
		LiftCharacter = null;
		InitializeGame();
	}

}
