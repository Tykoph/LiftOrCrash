using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public delegate void MoneyUpdate();
	public event MoneyUpdate OnMoneyUpdate;

	public delegate void GainUpdate();
	public event GainUpdate OnGainUpdate;

	public delegate void BetUpdate();
	public event BetUpdate OnBetUpdate;

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
		OnBetUpdate?.Invoke();
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

	public float GetStageGain(int stage)
	{
		stage++;
		return BetAmount * (stage * 0.1f);
	}

	public void AddStagePassed(int stageReached)
	{
		stageDifference += stageReached - StagePassed;
		for (; StagePassed < stageReached; StagePassed++)
		{
			float stageGain = GetStageGain(StagePassed);
			AddCurrentGain(stageGain);
		}
		print(StagePassed + " stage passed, Stage Diff: " + stageDifference);

		if (stageDifference < CASHOUT_STAGE_DIFFERENCE) return;
		stageDifference = 0;
		UIManager.UIInstance.ShowCashoutMenu();
	}

	private void AddJackpotGain()
	{
		float jackpotGain = currentGain * 2f;
		AddCurrentGain(jackpotGain);
		print("Jackpot gained: " + jackpotGain);
	}

	public void Cashout()
	{
		print("You cashed out with " + currentGain + " gain!");
		AddMoney(currentGain);
		UIManager.UIInstance.HideCashoutMenu();
		InitializeGame();
	}

	public void LoseGame()
	{
		UIManager.UIInstance.ShowLosePopup();
	}

	public void WinGame()
	{
		AddJackpotGain();
		UIManager.UIInstance.ShowWinPopup();
	}

	public void ReturnToMainMenu(bool win = true)
	{
		if (!win)
		{
			InitializeGame();
			return;
		}

		Cashout();
	}
}
