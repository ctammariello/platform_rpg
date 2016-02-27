using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	public float moveSpeed;
	public float jumpHeight;

	Transform myTrans;
	Rigidbody2D myBody;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	private bool isGrounded = false;
	private bool doubleJumped;

	void Start () {
		myBody = this.GetComponent<Rigidbody2D>();
		myTrans = this.transform;
	}

	void FixedUpdate(){
			Move(Input.GetAxisRaw("Horizontal"));
			if(isGrounded){
				doubleJumped = false;
			}

			if(Input.GetButtonDown("Jump") && isGrounded){
				Jump();
			}
			if(Input.GetButtonDown("Jump") && !doubleJumped && !isGrounded){
				Jump();
				doubleJumped = true;
			}
			isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
	}

	public void Move(float horizontalInput){
		Vector2 moveVel = myBody.velocity;
		moveVel.x = horizontalInput * moveSpeed;
		myBody.velocity = moveVel;
	}

	public void Jump(){
		myBody.velocity += jumpHeight * Vector2.up;
	}
}
