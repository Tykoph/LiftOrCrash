using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float Money { get; private set; } = 0f;
	public float CurrentGain { get; private set; } = 0f;
	public float BetAmount { get; private set; } = 1f;

	[SerializeField]
	private GameObject characterPrefab;
	public LiftCharacter LiftCharacter { get; private set; }

	public static GameManager GMInstance { get; private set; }

	public int StagePassed { get; private set; } = 0;

	private int stageDifference = 0;
	private const int CASHOUT_STAGE_DIFFERENCE = 3;

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
		UIManager.UIInstance.InitializeUI();
		StagePassed = 0;
	}

	public void AddStagePassed(int newStage)
	{
		stageDifference += newStage - StagePassed;
		for (; StagePassed < newStage; StagePassed++)
		{
			CurrentGain += BetAmount * (StagePassed * 0.1f);
			UIManager.UIInstance.SetCurrentGainText();
			print(StagePassed + " stage passed, current gain: " + CurrentGain);
		}

		if (stageDifference < CASHOUT_STAGE_DIFFERENCE) return;
		stageDifference = 0;
		UIManager.UIInstance.CashoutMenu(true);
	}

	public void Cashout()
	{
		print("You cashed out with " + CurrentGain + " gain!");
		AddMoney(CurrentGain);
		CurrentGain = 0f;
		UIManager.UIInstance.CashoutMenu(false);
		InitializeGame();
	}

	public void LooseGame()
	{
		print("You loosed the game!");
		Destroy(LiftCharacter.gameObject);
		LiftCharacter = null;
		InitializeGame();
	}

}
