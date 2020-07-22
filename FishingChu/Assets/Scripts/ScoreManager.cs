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
	
	public int CalcRankOfHandsFunc(RankOfHands type, Dictionary<int, int> dicDice)
	{
		/*
		Debug.Log("CalcRankOfHandsFunc\n");
		foreach(var dic in dicDice)
		{
			Debug.Log(dic.Key + " : " + dic.Value);
		}
		Debug.Log("======================");
		*/

		int score = 0;
		switch(type)
		{
			case RankOfHands.eChoice:
				score = CalcTotalSum(dicDice);
				break;

			case RankOfHands.e4ofaKind:
				if(true == Check4ofaKind(dicDice))
					score = CalcTotalSum(dicDice);
				break;

			case RankOfHands.eFullHouse:
				if (true == CheckFullHouse(dicDice))
					score = CalcTotalSum(dicDice);
				break;

			case RankOfHands.eSmallStraight:
				score = CheckSmallStaright(dicDice);
				break;

			case RankOfHands.eLargeStraight:
				score = CheckLargeStaright(dicDice);
				break;

			case RankOfHands.eFishingChu:
				score = CheckFishingChu(dicDice);
				break;

			default:
				Debug.LogError("<color=red> Not existent type : " + type + "\n ScoreManager:CalcRankOfHandsFunc  </color>");
				break;
		}

		return score;
	}

	int CalcTotalSum(Dictionary<int, int> dicDice)
	{
		int result = 0;
		foreach (var value in dicDice)
		{
			result += value.Key * value.Value;
		}

		return result;
	}

	bool Check4ofaKind(Dictionary<int, int> dicDice)
	{
		if (2 < dicDice.Count)
			return false;

		foreach(var value in dicDice)
		{
			if (4 <= value.Value)
				return true;
		}

		return false;
	}

	bool CheckFullHouse(Dictionary<int, int> dicDice)
	{
		if (2 < dicDice.Count)
			return false;

		if (dicDice.Count == 1)
		{
			return true;
		}
		else
		{
			foreach (var value in dicDice)
			{
				if (value.Value == 2 || value.Value == 3)
					return true;
			}
		}
		
		return false;
	}

	int CheckSmallStaright(Dictionary<int, int> dicDice)
	{
		if (4 <= dicDice.Count)
			return 15;
		else
			return 0;
	}

	int CheckLargeStaright(Dictionary<int, int> dicDice)
	{
		if (5 <= dicDice.Count)
			return 30;
		else
			return 0;
	}

	int CheckFishingChu(Dictionary<int, int> dicDice)
	{
		if (1 == dicDice.Count)
			return 50;
		else
			return 0;
	}
}
