using System.Collections;
using System.Collections.Generic;
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

	[Header("UI Variable")]
	public Text m_textTurn;
	public Text[] m_textDice;


	private int m_nNowTurn;
	private int[] m_arrDice;
	private PlayerType m_nNowPlayer = PlayerType.eLeft;

	public const int ReferenceBonusScore = 63;
	public const int GameOverTurn = 12;
	public PlayerData[] m_playerData;

	void Awake()
	{
		m_arrDice = new int[5];
	}

	public void OnRollDice(int[] arrDice)
	{
		for (int i = 0; i < m_arrDice.Length; ++i)
		{
			m_arrDice[i] = Random.Range(1, 7);
			m_textDice[i].text = m_arrDice[i].ToString();
		}

		m_playerData[(int)m_nNowPlayer].CalcDiceValue(arrDice);
	}

	public void OnKeepDice(int index)
	{
		m_playerData[(int)m_nNowPlayer].KeepDice(m_arrDice[index]);
	}


}
