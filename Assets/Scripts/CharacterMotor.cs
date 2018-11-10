using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour 
{
	//Handle player input and collisions.
	CharacterController m_CharacterController;
	Vector3 m_MoveVector;
	float m_MoveSpeed = 10f; //ToDo: different speeeds for strafeing, backwards, jumping.
	float m_JumpSpeed = 15f;
	float m_Gravity = 1f;
	float m_TerminalVelocity = 20f;
	float m_VerticalVelocity = 0f;

	void Start()
	{
		m_CharacterController = gameObject.GetComponent<CharacterController>();
	}

	void ResetMotor()
	{
		m_VerticalVelocity = m_MoveVector.y;
		m_MoveVector = Vector3.zero;
	}

	void GetInput()
	{
		float deadZone = 0.1f;

		if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
		{
			m_MoveVector  += new Vector3(0f, 0f, Input.GetAxis("Vertical"));
		}

		if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
		{
			m_MoveVector += new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
		}
	}

	void HandleActionInput()
	{
		if (m_CharacterController.isGrounded && Input.GetKeyUp(KeyCode.Space))
		{
			m_VerticalVelocity = m_JumpSpeed;
		}
		//ToDo: Shift for sprint?
	}

	void ApplyGravity()
	{
		//Don't apply gravity unless we are in the air.
		if (m_CharacterController.isGrounded)
			return;

		if (m_MoveVector.y > -m_TerminalVelocity)
		{
			m_VerticalVelocity -= m_Gravity;
			//m_MoveVector = new Vector3(m_MoveVector.x, m_MoveVector.y - m_Gravity, m_MoveVector.z);
		}
	}

	void ProcessMotion()
	{
		//Translate move vector to world space.
		m_MoveVector = transform.TransformDirection(m_MoveVector);

		if (m_MoveVector.magnitude > 1)
			m_MoveVector = Vector3.Normalize(m_MoveVector);

		ApplyGravity();

		m_MoveVector *= m_MoveSpeed;
		m_MoveVector = new Vector3(m_MoveVector.x, m_VerticalVelocity, m_MoveVector.z);

		m_CharacterController.Move(m_MoveVector * Time.deltaTime);
	}

	void Update()
	{
		ResetMotor();
		GetInput();
		HandleActionInput();
		ProcessMotion();
	}
}
