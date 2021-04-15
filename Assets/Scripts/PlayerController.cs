using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public DynamicJoystick LeftJoystick;
	public FixedTouchField CameraJoystick;
	public FixedButton SitButton;
	// public FixedButton PushButton;
	public FixedButton ButtonJump;

	public float movementSpeed = 5f;
	public float jumpPower = 2f;
	private float heightOfFall = 0f;

	// motion
	private float gravityForce;
	private Vector3 moveVector;

	private CapsuleCollider player_collider; 
	private Rigidbody rb;
	public Animator ch_animator;

	private bool isSitDown = false; 
	private bool isHang = false;
	private bool isGrounded;

	void Start() {
		player_collider = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
		ch_animator = GetComponent<Animator>();
	}

	void Update() {
		if (isSitDown != SitButton.isClicked) {
			Sit(SitButton.isClicked);
		}
		// HandlePushItemsInput();
	}

	void FixedUpdate() {

		bool isDeath = ch_animator.GetBool("Death");
		bool isPush = ch_animator.GetBool("Push");

		HandleMovementInput(isDeath || isPush);

		if (!isSitDown) {
			JumpOnTheWall();
		}
	}

	void TeleportAfterHang() {
		transform.position = transform.Find("HangPositionEnd").transform.position;
	}

	void HandleMovementInput(bool isCanMove) {
		if (!isCanMove) {

			Vector3 movement;

			if (!isHang) {
				Vector3 movementX = Camera.main.transform.right * (LeftJoystick.input.x == 0 ? Input.GetAxis("Horizontal") * (movementSpeed / 2) : LeftJoystick.input.x * (movementSpeed / 2));
				Vector3 movementZ = Camera.main.transform.forward * (LeftJoystick.input.y == 0 ? Input.GetAxis("Vertical") * movementSpeed : LeftJoystick.input.y * movementSpeed);

				movement = movementX + movementZ;

			} else {
				movement = Camera.main.transform.right * (LeftJoystick.input.x == 0 ? Input.GetAxis("Horizontal") * (movementSpeed / 2) : LeftJoystick.input.x * (movementSpeed / 2));

				if (Input.GetKeyDown(KeyCode.J)) {
					ch_animator.SetTrigger("ClimbJump");
				}
			}

			ch_animator.SetFloat("Horizontal", LeftJoystick.input.y == 0 ? Input.GetAxis("Vertical") : LeftJoystick.input.y);
			ch_animator.SetFloat("Vertical", LeftJoystick.input.x == 0 ? Input.GetAxis("Horizontal") : LeftJoystick.input.x);
			rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
		}
	}

	void HandlePushItemsInput() {

		// if(isGrounded) {
		// 	if (Input.GetKey(KeyCode.F) || PushButton.isPressed) {
		// 		// ch_animator.SetBool("Push", true);
		// 	} else {
		// 		// ch_animator.SetBool("Push", false);
		// 	}
		// } else {
		// 	// ch_animator.SetBool("Push", false);
		// }
	}

	public void Sit(bool isPressed) {
		if (isGrounded) {
			isSitDown = isPressed;

			if (isSitDown) {
				player_collider.height = 1.1f;
				player_collider.center = new Vector3(0, 0.4f, 0);
				movementSpeed = movementSpeed / 2.66f;
				// ch_animator.SetBool("Sit", true);
			} else {
				player_collider.height = 2;
				player_collider.center = new Vector3(0, 0.9f, 0);
				movementSpeed = movementSpeed * 2.66f;
				// ch_animator.SetBool("Sit", false);
			}

		}
	}

	public void JumpOnTheWall() {
		if (!isHang) {
			RaycastHit hit;
			if (Physics.Raycast(transform.position + new Vector3(0, 1.4f, 0), transform.forward, out hit, 0.4f)) {
				
				if (hit.transform.tag == "CanClimb") {
					if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
						ch_animator.SetTrigger("Jump");
						// rb.velocity = new Vector3(0, Mathf.Sqrt(20), 0);
						isGrounded = false;
					}
				}

			}
		}
	}

	public void ClimbOnTheWall(bool status) {
		if (status) {
			rb.velocity = Vector3.zero;
			rb.useGravity = false;
			ch_animator.SetBool("Climb", true);
			movementSpeed = movementSpeed / 4;
			isHang = true;
		} else {
			rb.useGravity = true;
			ch_animator.SetBool("Climb", false);
			movementSpeed = movementSpeed * 4;
			isHang = false;
		}
	}

	void OnCollisionStay() {
        isGrounded = true;

		if (heightOfFall - transform.position.y > 10f) {
			// ch_animator.SetBool("Death", true);
		} 

    }

	void OnCollisionExit() {
		isGrounded = false;
		heightOfFall = transform.position.y;
	}

}
