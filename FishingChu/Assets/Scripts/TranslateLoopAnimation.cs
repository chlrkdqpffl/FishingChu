using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslateLoopAnimation : MonoBehaviour
{
	public AnimationCurve m_animCurve;
	public Vector3 m_startPosition;
	public Vector3 m_endPosition;
	public float m_delayTime = 0.0f;
	public float m_playTime = 1.0f;
	public bool m_bLoop = false;

	private Transform m_myTransform = null;
	private RectTransform m_myRectTransform = null;
	private float delayTimer = 0.0f;
	private float playTimer = 0.0f;

	void Start()
	{
		m_myTransform = GetComponent<Transform>();
		m_myRectTransform = GetComponent<RectTransform>();
	}

	void Update()
	{
		if (m_animCurve == null)
			return;

		if (delayTimer <= m_delayTime)
		{
			delayTimer += Time.deltaTime;
			return;
		}

		if (playTimer <= m_playTime)
		{
			//		if(m_myTransform != null)
			//				m_myTransform.localScale = Vector3.Lerp(m_startScale, m_endScale, m_animCurve.Evaluate(playTimer / m_playTime));
			//		else if (m_myRectTransform != null)
			m_myRectTransform.localPosition = Vector3.Lerp(m_startPosition, m_endPosition, m_animCurve.Evaluate(playTimer / m_playTime));

			playTimer += Time.deltaTime;
		}
		else
		{
			if (m_bLoop)
			{
				playTimer = 0.0f;
			}
		}
	}
}