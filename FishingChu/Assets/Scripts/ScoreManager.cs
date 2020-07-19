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
		eChoice = 9,
		e4ofaKind,
		eFullHouse,
		eSmallStraight,
		eLargeStraight,
		eFishingChu,
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
		eBonus,		// 8

		eChoice,
		e4ofaKind,
		eFullHouse,
		eSmallStraight,
		eLargeStraight,
		eFishingChu,
		eTotal,

		eMax,
	}

	private static ScoreManager _instance;
	public static ScoreManager Instance
	{
		get	{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType(typeof(ScoreManager)) as ScoreManager;
			}
			return _instance;
		}
	}
	
	public int CalcRankOfHandsFunc(RankOfHands type, int[] arrDice)
	{
		int score = 0;
		switch(type)
		{
			case RankOfHands.eChoice:
				score = CalcTotalSum(arrDice);
				break;

			case RankOfHands.e4ofaKind:
				if(true == Check4ofaKind(arrDice))
					score = CalcTotalSum(arrDice);
				break;

			case RankOfHands.eFullHouse:
				if (true == CheckFullHouse(arrDice))
					score = CalcTotalSum(arrDice);
				break;

			case RankOfHands.eSmallStraight:
				score = CheckSmallStaright(arrDice);
				break;

			case RankOfHands.eLargeStraight:
				score = CheckLargeStaright(arrDice);
				break;

			case RankOfHands.eFishingChu:
				score = CheckFishingChu(arrDice);
				break;

			default:
				Debug.LogError("<color=red> Not existent type : " + type + "\n ScoreManager:CalcRankOfHandsFunc  </color>");
				break;
		}

		return score;
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

		for(int i = 0; i < arrDice.Length; ++i)
		{
			if (dicTemp.ContainsKey(arrDice[i]))
				dicTemp[arrDice[i]]++;
			else
				dicTemp.Add(arrDice[i], 1);
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

		for (int i = 0; i < arrDice.Length; ++i)
		{
			if (dicTemp.ContainsKey(arrDice[i]))
				dicTemp[arrDice[i]]++;
			else
				dicTemp.Add(arrDice[i], 1);
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
		for (int i = 0; i < arrDice.Length - 1; ++i)
		{
			if (arrDice[i].Equals(arrDice[i + 1] - 1))
				consecutiveNumber++;
		}

		if (3 <= consecutiveNumber)
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
			if (arrDice[i].Equals(arrDice[i + 1] - 1))
				consecutiveNumber++;
		}

		if (4 <= consecutiveNumber)
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
}
