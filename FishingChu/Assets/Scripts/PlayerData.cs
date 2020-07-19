using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
	public enum ActionType
	{
		eNone,
		eRollDice,
		eKeepSelect,
		eScoreSelect,
		eTurnExit,
	}

	[Header("UI Variable")]
	public Text[] m_textScore;
	public Button[] m_btnKeepDice;



	private int[] m_nTempScore;
	private int[] m_nRealScore;

	private Dictionary<int, int> m_dicKeepDice = new Dictionary<int, int>();

	private ActionType m_nowAction;
	public ActionType PlayerAction
	{
		get { return m_nowAction; }
	}
	
	private int m_nKeepDiceCount = 0;
	public int KeepCount
	{
		get { return m_nKeepDiceCount; }
	}
	private bool m_bBonusScore = false;

	private void Awake()
	{
		for (int i = 1; i <= 6; ++i)
			m_dicKeepDice.Add(i, 0);

		m_nTempScore = new int[m_textScore.Length];
		m_nRealScore = new int[m_textScore.Length];

		InitScore();
	}

	public void InitScore()
	{
		m_nowAction = ActionType.eNone;

		// Clear Keep Dice 
		for (int i = 1; i <= 6; ++i)
			m_dicKeepDice[i] = 0;
		m_nKeepDiceCount = 0;

		foreach (var btn in m_btnKeepDice)
			btn.gameObject.SetActive(false);

		// Clear Score
		for (int i = 0; i < m_textScore.Length; ++i)
		{
			m_nTempScore[i] = 0;
			m_nRealScore[i] = 0;
		}

		// Dummy Value
		for (int i = 0; i < m_nRealScore.Length; ++i)
			m_nRealScore[i] = -1;

		// Clear UI
		foreach (var text in m_textScore)
		{
			text.text = "";
		}

		m_textScore[(int)ScoreManager.Categories.eSubTotal - 1].text = string.Format("{0}/{1}", 
			0, GameManager.ReferenceBonusScore);

		m_textScore[(int)ScoreManager.Categories.eTotal - 1].text = "0";
	}

	public void CalcDiceValue(int[] arrDice)
	{
//		m_nowAction = ActionType.eRollDice; // 주사위 굴리는 연출 들어갈 시 추가 예정
		m_nowAction = ActionType.eKeepSelect;

		var dicTemp = new Dictionary<int, int>();
		for (int i = 1; i <= 6; ++i)
			dicTemp.Add(i, 0);

		foreach (var value in arrDice)
		{
			if(value != 0)
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
		UpdateTotalScore();

		// UI
		UpdateScoreText();
	}

	public void KeepDice(int number)
	{
		m_nKeepDiceCount++;
		m_dicKeepDice[number]++;

		UpdateKeepDiceUI(number);

		// Check Keep Count
		if (GameManager.DiceCount <= m_nKeepDiceCount)
		{
			m_nowAction = ActionType.eScoreSelect;
			ScoreSelectMode();
		}
	}

	void ScoreSelectMode()
	{

	}

	void UpdateKeepDiceUI(int number)
	{
		for(int i = 0; i < m_nKeepDiceCount; ++i)
		{
			m_btnKeepDice[i].gameObject.SetActive(true);
		}

		m_btnKeepDice[m_nKeepDiceCount - 1].GetComponentInChildren<Text>().text = number.ToString();
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

	void UpdateTotalScore()
	{
		int rankOfHandsScore = 0;

		for (int selectValue = (int)ScoreManager.Categories.eChoice; selectValue <= (int)ScoreManager.Categories.eFishingChu; ++selectValue)
			rankOfHandsScore += m_nRealScore[selectValue];

		if (rankOfHandsScore < 0)
			rankOfHandsScore = 0;

		m_nRealScore[(int)ScoreManager.Categories.eTotal - 1] = m_nRealScore[(int)ScoreManager.Categories.eSubTotal - 1] + rankOfHandsScore;
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
		m_textScore[(int)ScoreManager.Categories.eSubTotal - 1].color = new Color(233, 233, 233);
		m_textScore[(int)ScoreManager.Categories.eSubTotal - 1].text = string.Format("{0}/{1}", 
			m_nRealScore[(int)ScoreManager.Categories.eSubTotal - 1], GameManager.ReferenceBonusScore);

		// Update Bonus
		if (!m_bBonusScore)
			m_textScore[(int)ScoreManager.Categories.eBonus - 1].text = "";
		else
		{
			m_textScore[(int)ScoreManager.Categories.eBonus - 1].text = string.Format("+{0}", m_nRealScore[(int)ScoreManager.Categories.eBonus - 1]);
		}

		// Update Total
		m_textScore[(int)ScoreManager.Categories.eTotal - 1].text = m_nRealScore[(int)ScoreManager.Categories.eTotal - 1].ToString();
	}
}
