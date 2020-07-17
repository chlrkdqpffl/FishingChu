using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	// rank of hands
	public enum RankOfHands
	{
		eNone,
		eChoice,
		e4ofaKind,
		eFullHouse,
		eSmallStraight,
		eLargeStraight,
		eFishingchu,
	}

	public enum Categories
	{
		eNone,
		eAce,
		eDeuces,
		eThrees,
		eFours,
		eFives,
		eSixes,
		eSubTotal,
		eBonus,
		eChoice,
		e4ofaKind,
		eFullHouse,
		eSmallStraight,
		eLargeStraight,
		eFishingchu,
		eTotal,
	}

	public Text[] m_textPlayer1;
	public Text[] m_textPlayer2;

	private static ScoreManager _instance;
	public static ScoreManager Instance
	{
		get	{
			if (_instance == null)
			{
				_instance = new ScoreManager();
			}
			return _instance;
		}
	}

//	private Dictionary<int, int> m_dic
	
	// 각 족보별 계산식 적용
	//delegate 




	public void CalcDiceValue(int[] arrDice)
	{


	}

	int CalcTotalSum(int[] arrDice)
	{
		int result = 0;
		foreach (var value in arrDice)
			result += value;

		return result;
	}

	bool Check4ofaKind(int[] arrDice)
	{
		var dicTemp = new Dictionary<int, int>();

		foreach (var value in arrDice)
		{
			if (dicTemp.ContainsKey(arrDice[0]))
				dicTemp[0]++;
			else
				dicTemp.Add(arrDice[0], 1);
		}

		if (2 < dicTemp.Count)
			return false;

		foreach(var value in dicTemp)
		{
			if (4 <= value.Value)
				return true;
		}

		return false;
	}

	bool CheckFullHouse(int[] arrDice)
	{
		var dicTemp = new Dictionary<int, int>();

		foreach (var value in arrDice)
		{
			if (dicTemp.ContainsKey(arrDice[0]))
				dicTemp[0]++;
			else
				dicTemp.Add(arrDice[0], 1);
		}

		if (2 < dicTemp.Count)
			return false;

		if (dicTemp.Count == 1)
		{
			return true;
		}
		else
		{
			foreach (var value in dicTemp)
			{
				if (value.Value == 2 || value.Value == 3)
					return true;
			}
		}
		
		return false;
	}

	int CheckSmallStaright(int[] arrDice)
	{
		Array.Sort(arrDice);

		int consecutiveNumber = 0;
		for (int i = 0; i < arrDice.Length - 1; ++i) {
			if (arrDice[i] == arrDice[i + 1] + 1)
				consecutiveNumber++;
		}

		if (4 <= consecutiveNumber)
			return 15;
		else
			return 0;
	}

	int CheckLargeStaright(int[] arrDice)
	{
		Array.Sort(arrDice);

		int consecutiveNumber = 0;
		for (int i = 0; i < arrDice.Length - 1; ++i)
		{
			if (arrDice[i] == arrDice[i + 1] + 1)
				consecutiveNumber++;
		}

		if (5 <= consecutiveNumber)
			return 30;
		else
			return 0;
	}

	int CheckFishingChu(int[] arrDice)
	{
		int selectValue = arrDice[0];
		foreach(var value in arrDice)
		{
			if (selectValue == value)
				continue;
			else
				return 0;
		}

		return 50;
	}

	void UpdateScore()
	{
		{ // Player1
			m_textPlayer1[(int)Categories.eAce].text			= "";
			m_textPlayer1[(int)Categories.eDeuces].text			= "";
			m_textPlayer1[(int)Categories.eThrees].text			= "";
			m_textPlayer1[(int)Categories.eFours].text			= "";
			m_textPlayer1[(int)Categories.eFives].text			= "";
			m_textPlayer1[(int)Categories.eSixes].text			= "";
			m_textPlayer1[(int)Categories.eSubTotal].text		= "";
			m_textPlayer1[(int)Categories.eBonus].text			= "";

			m_textPlayer1[(int)Categories.eChoice].text			= "";
			m_textPlayer1[(int)Categories.e4ofaKind].text		= "";
			m_textPlayer1[(int)Categories.eFullHouse].text		= "";
			m_textPlayer1[(int)Categories.eSmallStraight].text	= "";
			m_textPlayer1[(int)Categories.eLargeStraight].text	= "";

			m_textPlayer1[(int)Categories.eFishingchu].text		= "";
			m_textPlayer1[(int)Categories.eTotal].text			= "";
		}

		{ // Player2
			m_textPlayer2[(int)Categories.eAce].text			= "";
			m_textPlayer2[(int)Categories.eDeuces].text			= "";
			m_textPlayer2[(int)Categories.eThrees].text			= "";
			m_textPlayer2[(int)Categories.eFours].text			= "";
			m_textPlayer2[(int)Categories.eFives].text			= "";
			m_textPlayer2[(int)Categories.eSixes].text			= "";
			m_textPlayer2[(int)Categories.eSubTotal].text		= "";
			m_textPlayer2[(int)Categories.eBonus].text			= "";

			m_textPlayer2[(int)Categories.eChoice].text			= "";
			m_textPlayer2[(int)Categories.e4ofaKind].text		= "";
			m_textPlayer2[(int)Categories.eFullHouse].text		= "";
			m_textPlayer2[(int)Categories.eSmallStraight].text	= "";
			m_textPlayer2[(int)Categories.eLargeStraight].text	= "";

			m_textPlayer2[(int)Categories.eFishingchu].text		= "";
			m_textPlayer2[(int)Categories.eTotal].text			= "";
		}
	}
}
