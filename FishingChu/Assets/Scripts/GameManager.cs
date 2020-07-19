using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	enum PlayerType
	{
		eLeft,
		eRight,
		eMax,
	}

	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
			}
			return _instance;
		}
	}

	public const int ReferenceBonusScore = 63;
	public const int GameOverTurn = 12;
	public const int DiceCount = 5;

	[Header("UI Variable")]
	public Transform m_tmDiceParent;
	public Text m_textTurn;
	public Button[] m_btnDice;

	private int m_nNowTurn;
	private int[] m_arrDice;
	private PlayerType m_nNowPlayer = PlayerType.eLeft;

	public PlayerData[] m_playerData;

	void Awake()
	{
		m_arrDice = new int[DiceCount];
	}

	void Start()
	{
		InitGame();
	}

	void InitGame()
	{
		foreach (var player in m_playerData)
			player.InitScore();

		m_tmDiceParent.gameObject.SetActive(false);
	}

	public void OnRollDice()
	{
		// Random Dice
		for (int i = 0; i < m_arrDice.Length; ++i)
		{
			if (i < m_arrDice.Length - GetDisplayKeepCount())
				m_arrDice[i] = UnityEngine.Random.Range(1, 7);
			else
				m_arrDice[i] = 0;
		}

		// Sort
		Array.Sort(m_arrDice);

		// UI
		ShowDicePositionOnSort();

		m_playerData[(int)m_nNowPlayer].CalcDiceValue(m_arrDice);
	}

	public void OnKeepDice(int index)
	{
		if (m_playerData[(int)m_nNowPlayer].PlayerAction != PlayerData.ActionType.eKeepSelect)
			return;

		m_playerData[(int)m_nNowPlayer].KeepDice(m_arrDice[index]);
		m_btnDice[index].gameObject.SetActive(false);
	}

	int GetDisplayKeepCount()
	{
		int nKeepCount = 0;
		foreach (var player in m_playerData)
		{
			if (nKeepCount < player.KeepCount)
				nKeepCount = player.KeepCount;
		}

		return nKeepCount;
	}

	void ShowDicePositionOnSort()
	{
		m_tmDiceParent.gameObject.SetActive(true);
		for (int i = 0; i < m_arrDice.Length; ++i)
		{
			if (i < m_arrDice.Length - GetDisplayKeepCount())
			{
				m_btnDice[i].gameObject.SetActive(true);
				m_btnDice[i].GetComponentInChildren<Text>().text = m_arrDice[i].ToString();
			}
			else
			{
				m_btnDice[i].gameObject.SetActive(false);
			}
		}

		// Sort Position

		List<GameObject> activeList = new List<GameObject>();

		foreach (var dice in m_btnDice)
		{
			if(dice.gameObject.activeSelf == true)
				activeList.Add(dice.gameObject);
		}

		switch (GetDisplayKeepCount())
		{
			case 0:

				break;

			case 1:

				break;

			case 2:

				break;

			case 3:

				break;

			case 4:

				break;
		}
	}
}
