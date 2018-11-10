using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour 
{
	//Camera should follow mouse movement
	float m_Yaw = 0f;
	float m_Pitch = 0f;

	float m_SpeedVertical = 2f;
	float m_SpeedHorizontal = 2f;

	void FixedUpdate()
	{
		m_Yaw += m_SpeedHorizontal * Input.GetAxis("Mouse X");
		m_Pitch -= m_SpeedVertical * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(m_Pitch, m_Yaw, 0f); //ToDo: SMOOTH
	}
}
