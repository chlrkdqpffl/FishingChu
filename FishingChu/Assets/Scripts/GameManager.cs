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
		ShowDiceOnActive();

		m_playerData[(int)m_nNowPlayer].CalcDiceValue(m_arrDice);
	}

	public void OnKeepDice(int index)
	{
		if (m_playerData[(int)m_nNowPlayer].PlayerAction != PlayerData.ActionType.eKeepSelect)
			return;

		m_playerData[(int)m_nNowPlayer].KeepDice(m_arrDice[index]);
		m_btnDice[index].gameObject.SetActive(false);
		SortDicePosition();
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

	void SortDicePosition()
	{
		// 현재는 2D UI 로 포지션을 강제 세팅하지만, 주사위 (3D) 로 변경 시 다시 세팅 필요
		List<GameObject> activeList = new List<GameObject>();

		foreach (var dice in m_btnDice)
		{
			if (dice.gameObject.activeSelf == true)
				activeList.Add(dice.gameObject);
		}

		switch (GetDisplayKeepCount())
		{
			case 0:
				activeList[0].GetComponent<RectTransform>().localPosition = new Vector3(-60, 60, 0);
				activeList[1].GetComponent<RectTransform>().localPosition = new Vector3(160, 60, 0);
				activeList[2].GetComponent<RectTransform>().localPosition = new Vector3(380, 60, 0);
				activeList[3].GetComponent<RectTransform>().localPosition = new Vector3(600, 60, 0);
				activeList[4].GetComponent<RectTransform>().localPosition = new Vector3(820, 60, 0);
				break;

			case 1:
				activeList[0].GetComponent<RectTransform>().localPosition = new Vector3(50, 60, 0);
				activeList[1].GetComponent<RectTransform>().localPosition = new Vector3(270, 60, 0);
				activeList[2].GetComponent<RectTransform>().localPosition = new Vector3(490, 60, 0);
				activeList[3].GetComponent<RectTransform>().localPosition = new Vector3(710, 60, 0);

				break;

			case 2:
				activeList[0].GetComponent<RectTransform>().localPosition = new Vector3(160, 60, 0);
				activeList[1].GetComponent<RectTransform>().localPosition = new Vector3(380, 60, 0);
				activeList[2].GetComponent<RectTransform>().localPosition = new Vector3(600, 60, 0);
				break;

			case 3:
				activeList[0].GetComponent<RectTransform>().localPosition = new Vector3(270, 60, 0);
				activeList[1].GetComponent<RectTransform>().localPosition = new Vector3(490, 60, 0);
				break;

			case 4:
				activeList[0].GetComponent<RectTransform>().localPosition = new Vector3(380, 60, 0);
				break;
		}
	}

	void ShowDiceOnActive()
	{
		m_tmDiceParent.gameObject.SetActive(true);
		for (int i = 0; i < m_arrDice.Length; ++i)
		{
			if (m_arrDice[i] <= 0)
			{
				m_btnDice[i].gameObject.SetActive(false);
			}
			else
			{
				m_btnDice[i].gameObject.SetActive(true);
				m_btnDice[i].GetComponentInChildren<Text>().text = m_arrDice[i].ToString();
			}
		}

		SortDicePosition();
	}
}
