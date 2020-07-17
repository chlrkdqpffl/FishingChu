using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
	public Text[] m_textDice; 

	private int[] m_arrDice;
    void Start()
    {
		m_arrDice = new int[5];
		
		ScoreManager.Instance.CalcDiceValue(m_arrDice);

	}
	
	public void OnRollDice()
	{
		for (int i = 0; i < m_arrDice.Length; ++i) {
			m_arrDice[i] = Random.Range(1, 7);
			m_textDice[i].text = m_arrDice[i].ToString();
		}
	}
}
