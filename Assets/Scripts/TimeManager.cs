using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour 
{
	//ToDo: Control lighting to simulate a day/night cycle.
	public Text m_TimeDisplay;
	float m_DayLength = 30f; //In seconds.
	float m_DayTimer = 0f;
	int m_TotalDays = 0;

	void Start()
	{
		m_TimeDisplay.text = "Days: " + m_TotalDays;
	}

	void Update()
	{
		m_DayTimer += Time.deltaTime;

		if (m_DayTimer >= m_DayLength)
		{
			//ToDo: Signal plants to try to grow.
			m_TotalDays++;
			m_DayTimer = 0f;
			m_TimeDisplay.text = "Days: " + m_TotalDays;
		}
	}
}
