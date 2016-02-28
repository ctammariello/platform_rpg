using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;

    Transform myTrans;
    Rigidbody2D myBody;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private bool isGrounded = false;
    private bool doubleJump = false;

    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float move = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        myBody.velocity = new Vector2(move * moveSpeed, myBody.velocity.y);
        if (isGrounded)
        {
            doubleJump = false;
        }
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || !doubleJump))
        {
            Jump();
            if(!isGrounded && !doubleJump)
            {
                doubleJump = true;
            }
        }
    }

    public void Jump()
    {
        myBody.AddForce(new Vector2(0, jumpHeight));
    }

}
