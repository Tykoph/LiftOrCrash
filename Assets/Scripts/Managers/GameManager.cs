using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public delegate void MoneyUpdate();
	public event MoneyUpdate OnMoneyUpdate;

	public delegate void GainUpdate();
	public event GainUpdate OnGainUpdate;

	private float money = 10f;
	private float currentGain = 0f;
	public float BetAmount { get; private set; } = 1f;

	[SerializeField]
	public LiftCharacter liftCharacter;

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
		money += amount;
		OnMoneyUpdate?.Invoke();
	}

	public void AddCurrentGain(float amount)
	{
		currentGain += amount;
		OnGainUpdate?.Invoke();
	}

	public void AddBetAmount(float amount)
	{
		if (amount > money)
		{
			Debug.LogWarning("Not enough money to add bet amount!");
			return;
		}
		AddMoney(-amount);
		BetAmount += amount;
	}

	public void StartGame()
	{
		currentGain = 0f;
		GenerateCharacter();
		UIManager.UIInstance.StartGame();
		OnMoneyUpdate?.Invoke();
		OnGainUpdate?.Invoke();
	}

	private void GenerateCharacter()
	{
		liftCharacter.gameObject.SetActive(true);
		liftCharacter.GenerateCharacter();
	}

	private void InitializeGame()
	{
		liftCharacter.gameObject.SetActive(false);
		UIManager.UIInstance.InitializeUI();
		StagePassed = 0;
		currentGain = 0f;
		BetAmount = 1f;
		stageDifference = 0;
	}

	public string GetMoneyString()
	{
		return money.ToString("F2") + "$";
	}

	public string GetCurrentGainString()
	{
		return currentGain.ToString("F2") + "$";
	}

	public string GetBetAmountString()
	{
		return BetAmount.ToString("F0") + "$";
	}

	public void AddStagePassed(int stageReached)
	{
		stageDifference += stageReached - StagePassed;
		for (; StagePassed < stageReached; StagePassed++)
		{
			float stageGain = BetAmount * (StagePassed * 0.1f);
			AddCurrentGain(stageGain);
			print(StagePassed + " stage passed, current gain: " + currentGain);
		}

		if (stageDifference < CASHOUT_STAGE_DIFFERENCE) return;
		stageDifference = 0;
		UIManager.UIInstance.CashoutMenu(true);
	}

	public void Cashout()
	{
		print("You cashed out with " + currentGain + " gain!");
		AddMoney(currentGain);
		UIManager.UIInstance.CashoutMenu(false);
		InitializeGame();
	}

	public void LooseGame()
	{
		print("You loosed the game!");
		liftCharacter.gameObject.SetActive(false);
		InitializeGame();
	}

	public void WinGame()
	{
		Cashout();
		print("You won the game!");
	}

}
