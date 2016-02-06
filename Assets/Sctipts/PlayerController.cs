using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public int m_PlayerNumber = 1;   
	public float m_Speed = 12f;
	public float m_JumpSpeed = 12f;

	private string m_MovementAxisName; 
	private string m_JumpButtonName;
	private Rigidbody m_Rigidbody;
	private Animation m_Animation;
	private Collider m_Collider;
	private float m_MovementInputValue;

	private float m_distToGround;

	// Use this for initialization

	private bool m_toJump = false;


	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Collider = GetComponent<Collider>();
	}


	void Start () {
		m_MovementAxisName = "Horizontal" + m_PlayerNumber;
		m_JumpButtonName = "Jump" + m_PlayerNumber;
		//m_MovementAxisName = "Horizontal";
		m_Animation = GetComponent<Animation> ();

		m_distToGround = m_Collider.bounds.extents.y;

	}
	
	// Update is called once per frame
	void Update () {
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		if (Input.GetButton(m_MovementAxisName)) {
			m_Animation.Play ("walk");
		} else if (Input.GetButtonUp(m_MovementAxisName)) {
			m_Animation.Play ("idle");
		}
		if(Input.GetButtonDown (m_JumpButtonName) && IsGrounded()){
			m_Animation.Play("jump");
			m_toJump = true;
		}

	}

	void FixedUpdate() {
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
	
		if (m_toJump) {
			m_Rigidbody.velocity = new Vector3 (0f,m_JumpSpeed, 0f);
			//movement += transform.up * m_JumpSpeed * Time.deltaTime;
			m_toJump = false;
		}
		m_Rigidbody.MovePosition (m_Rigidbody.position + movement);
	}

	bool IsGrounded () {
		return Physics.Raycast (transform.position, -Vector3.up, m_distToGround + 0.1f);
	}
}
