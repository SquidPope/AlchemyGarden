using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour 
{
	public GameObject m_PlantPrefab;
	bool m_IsTriggered = false;
	bool m_Weeds = false;
	int m_WaterLevel;

	Plant m_Plant;

	void Start()
	{
		m_Weeds = false;
		m_WaterLevel = 0;
	}

	public int GetWaterLevel() { return m_WaterLevel; }
	public bool GetWeeds() { return m_Weeds; }

	public void PlantSeed(PlantType type)
	{
		if (!m_IsTriggered || m_Plant != null)
			return;
	
		m_Plant = GameObject.Instantiate(m_PlantPrefab, transform.position, Quaternion.identity).GetComponent<Plant>();
		m_Plant.Init(type);
		m_Plant.m_Soil = this;
	}

	public void RemovePlant()
	{
		m_Plant = null;
	}

	void OnTriggerEnter()
	{
		m_IsTriggered = true;
	}

	void OnTriggerExit()
	{
		m_IsTriggered = false;
	}

	//Temp: Remove!
	void Update()
	{
		if (!m_IsTriggered)
			return;
		
		if (Input.GetKeyUp(KeyCode.E))
		{
			Debug.Log("Planting");
			if (m_Plant == null)
			{
				int rand = Random.Range(0, (int)PlantType.Total);
				PlantSeed((PlantType)rand);
			}
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("Watering");
			m_WaterLevel++;
		}
	}
}
