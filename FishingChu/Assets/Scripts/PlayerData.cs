using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
	[Header("UI Variable")]
	public Text[] m_textScore;

	private int[] m_nTempScore;
	private int[] m_nRealScore;


	private Dictionary<int, int> m_dicKeepDice = new Dictionary<int, int>();
	private bool m_bBonusScore = false;

	private void Awake()
	{
		for (int i = 1; i <= 6; ++i)
			m_dicKeepDice.Add(i, 0);

		m_nTempScore = new int[m_textScore.Length];
		m_nRealScore = new int[m_textScore.Length];

		// Dummy Value
		for(int i = 0; i < m_nRealScore.Length; ++i)
			m_nRealScore[i] = -1;
	}

	public void CalcDiceValue(int[] arrDice)
	{
		var dicTemp = new Dictionary<int, int>();
		for (int i = 1; i <= 6; ++i)
			dicTemp.Add(i, 0);

		foreach (var value in arrDice)
		{
			dicTemp[value]++;
		}

		// Top Score : Dice Count
		for (int selectValue = (int)ScoreManager.Categories.eAce; selectValue <= (int)ScoreManager.Categories.eSixes; ++selectValue)
		{
			m_nTempScore[selectValue - 1] = selectValue * (m_dicKeepDice[selectValue] + dicTemp[selectValue]);
		}

		// Bottom Score : Rank Of Hands
		for (int selectValue = (int)ScoreManager.Categories.eChoice; selectValue <= (int)ScoreManager.Categories.eFishingChu; ++selectValue)
		{
			m_nTempScore[selectValue - 1] = ScoreManager.Instance.CalcRankOfHandsFunc((ScoreManager.RankOfHands)selectValue, arrDice);
		}


		// Post Process
		UpdateSubTotalScore();
		CheckBonusScore();
		UpdateScoreText();
	}

	public void KeepDice(int number)
	{
		m_dicKeepDice[number]++;

		Debug.Log("<color=red> Keep the Number : " + number + " </color>");
	}

	void UpdateSubTotalScore()
	{
		int subTotalScore = 0;

		for (int selectValue = (int)ScoreManager.Categories.eAce; selectValue <= (int)ScoreManager.Categories.eSixes; ++selectValue)
			subTotalScore += m_nRealScore[selectValue];

		if (subTotalScore < 0)
			subTotalScore = 0;

		m_nRealScore[(int)ScoreManager.Categories.eSubTotal - 1] = subTotalScore;
	}

	void CheckBonusScore()
	{
		if (GameManager.ReferenceBonusScore <= m_nRealScore[(int)ScoreManager.Categories.eSubTotal - 1])
		{
			m_bBonusScore = true;
			m_nRealScore[(int)ScoreManager.Categories.eBonus - 1] = 35;
		}
	}

	void UpdateScoreText()
	{
		for (int i = 0; i < m_textScore.Length; ++i)
		{
			if (m_nRealScore[i] < 0)
			{
				m_textScore[i].color = Color.gray;
				m_textScore[i].text = m_nTempScore[i].ToString();
			}
			else
			{
				m_textScore[i].color = Color.black;
				m_textScore[i].text = m_nRealScore[i].ToString();
			}
		}

		// Update SubTotal Score
		m_textScore[(int)ScoreManager.Categories.eSubTotal - 1].color = Color.white;
		m_textScore[(int)ScoreManager.Categories.eSubTotal - 1].text = string.Format("{0}/{1}", 
			m_nRealScore[(int)ScoreManager.Categories.eSubTotal - 1], GameManager.ReferenceBonusScore);

		// Update Bonus
		if (!m_bBonusScore)
			m_textScore[(int)ScoreManager.Categories.eBonus - 1].gameObject.SetActive(false);
		else
		{
			m_textScore[(int)ScoreManager.Categories.eBonus - 1].gameObject.SetActive(true);
			m_textScore[(int)ScoreManager.Categories.eBonus - 1].text = string.Format("+{0}", m_nRealScore[(int)ScoreManager.Categories.eBonus - 1]);
		}
	}
}
