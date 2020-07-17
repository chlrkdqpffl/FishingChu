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

	public enum Cate
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
		// Text 에 글자 업데이트


	}
}
