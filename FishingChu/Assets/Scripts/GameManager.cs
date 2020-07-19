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
	public Text[] m_textDice;

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
			m_arrDice[i] =  UnityEngine.Random.Range(1, 7);
	
		// Sort
		Array.Sort(m_arrDice);

		// UI
		m_tmDiceParent.gameObject.SetActive(true);
		for (int i = 0; i < m_arrDice.Length; ++i)
			m_textDice[i].text = m_arrDice[i].ToString();

		m_playerData[(int)m_nNowPlayer].CalcDiceValue(m_arrDice);
	}

	public void OnKeepDice(int index)
	{
		if (m_playerData[(int)m_nNowPlayer].PlayerAction != PlayerData.ActionType.eKeepSelect)
			return;

		m_playerData[(int)m_nNowPlayer].KeepDice(m_arrDice[index]);
	}


}
