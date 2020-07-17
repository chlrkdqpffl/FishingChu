using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
	private int[] m_arrDice;
    void Start()
    {
		m_arrDice = new int[5];
		m_arrDice[0] = 1;
		m_arrDice[1] = 3;
		m_arrDice[2] = 1;
		m_arrDice[3] = 2;
		m_arrDice[4] = 5;

		ScoreManager.Instance.CalcDiceValue(m_arrDice);

	}

	void Update()
    {
		
	}
}
