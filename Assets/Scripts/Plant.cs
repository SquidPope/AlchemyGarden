using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantType { Carrot, Flower, Mushroom, Onion, Potato, TallMushroom, ThiccMushroom, Total}
public class Plant : MonoBehaviour 
{
	GameObject m_Model;
	int m_GrowthTime; //Total growth states
	public int m_GrowthState; //Current growth state
	float m_GrowthScaleStart = 0.1f;
	float m_GrowthScaleCurrent;
	float m_GrowthScaleEnd = 0.6f;
	int m_WaterNeeded;
	int m_WiltCount = 0;
	int m_WiltTotal = 5;

	public PlantType m_Type;
	public bool m_CanHarvest = false;
	public Soil m_Soil;
	public List<GameObject> m_PlantModels; //These models should be in the same order as the plant types in the enum.

	public void Init(PlantType t)
	{
		//ToDo: Solve scale issues in blender, so m_GrowthScaleEnd can be 1 for everything.
		m_Type = t;
		switch (m_Type)
		{
			case PlantType.Carrot:
			m_WaterNeeded = 1;
			m_GrowthTime = 3;
			m_GrowthScaleEnd = 0.3f;
			break;

			case PlantType.Flower:
			m_WaterNeeded = 2;
			m_GrowthTime = 7;
			m_GrowthScaleEnd = 0.5f;
			break;

			case PlantType.Mushroom:
			m_WaterNeeded = 5;
			m_GrowthTime = 10;
			m_GrowthScaleEnd = 0.9f;
			break;

			case PlantType.Onion:
			m_WaterNeeded = 1;
			m_GrowthTime = 4;
			break;

			case PlantType.Potato:
			m_WaterNeeded = 1;
			m_GrowthTime = 5;
			m_GrowthScaleEnd = 0.3f;
			break;

			case PlantType.TallMushroom:
			m_WaterNeeded = 5;
			m_GrowthTime = 8;
			m_GrowthScaleEnd = 0.5f;
			break;

			case PlantType.ThiccMushroom:
			m_WaterNeeded = 5;
			m_GrowthTime = 6;
			break;

			default:
			m_Type = PlantType.Carrot;
			m_WaterNeeded = 1;
			m_GrowthTime = 3;
			m_GrowthScaleEnd = 0.3f;
			break;
		}

		m_GrowthScaleCurrent = m_GrowthScaleStart;
		m_GrowthState = 0;

		Debug.Log("Plant index: " + (int)m_Type);
		Debug.Log("Models count: " + m_PlantModels.Count);

		if (m_PlantModels.Count <= (int)PlantType.Total)
		{
			m_Model = GameObject.Instantiate(m_PlantModels[(int)m_Type], transform.position, Quaternion.identity);
			m_Model.transform.localScale = Vector3.one * m_GrowthScaleCurrent;
		}
		else
		{
			Debug.LogError("Plant models index out of bounds.");
		}
	}

	public void Grow()
	{
		Debug.Log("Growing");
		if (m_CanHarvest)
			return;

		if (m_GrowthState >= m_GrowthTime)
		{
			m_CanHarvest = true;
			m_GrowthScaleCurrent = m_GrowthScaleEnd;
		}
		else
		{
			if (m_Soil.GetWaterLevel() >= m_WaterNeeded)
			{
				//ToDo: Check weeds state of soil.
				m_GrowthState++;
				m_GrowthScaleCurrent += m_GrowthScaleEnd / m_GrowthTime;

				if (m_WiltCount > 0)
					m_WiltCount--;
			}
			else
			{
				Wilt();
			}

			//ToDo: Change art to reflect growth state.
			m_Model.transform.localScale = Vector3.one * m_GrowthScaleCurrent;
		}
	}

	void Wilt()
	{
		if (m_WiltCount >= m_WiltTotal)
		{
			m_Soil.RemovePlant();
			GameObject.Destroy(gameObject);
		}
		else
		{
			m_WiltCount++;
			m_GrowthState--;

			//Don't make it smaller than it was to start with.
			if (m_GrowthScaleCurrent > m_GrowthScaleStart)
				m_GrowthScaleCurrent -= m_GrowthScaleEnd / m_GrowthTime;
		}
	}

	public void Harvest()
	{
		if (!m_CanHarvest)
			return;

		m_Soil.RemovePlant();
		//ToDo: Place an item of this type in the player's inventory, or spawn a "Pick-up-able" version.
		GameObject.Destroy(gameObject);
	}

	void Update()
	{
		//ToDo: Remove this when Time becomes a thing.
		if (Input.GetKeyUp(KeyCode.G))
		{
			Debug.Log("Growing should be happening wts");
			Grow();
		}
			
	}
}
