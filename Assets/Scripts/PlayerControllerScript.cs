using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;

    Rigidbody2D myBody;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private bool isGrounded = false;
    private bool doubleJump = false;

    PlayerStamina stamina;

    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        stamina = GetComponent<PlayerStamina>();
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
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || (!doubleJump && stamina.getCurrentStamina() >= 50) ))
        {
            Jump();
            if (!isGrounded && !doubleJump)
            {
                doubleJump = true;
                stamina.reduceStamina(50);

            }
        }
    }

    public void Jump()
    {
            myBody.AddForce(new Vector2(0, jumpHeight));
    }

}
