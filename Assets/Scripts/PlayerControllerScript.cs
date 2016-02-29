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
    AttributeManager attributes;

    void Awake()
    {
        attributes = GetComponent<AttributeManager>();
        int agility = attributes.getAgility();
        //int brawn =attributes.getBrawn();
        moveSpeed = moveSpeed + (0.1f * (float)agility);
        //TODO ATTACK SPEED
        //attackSpeed = attackSpeed + (0.1f * (float)agility);

        //TODO ATTACK DAMAGE (what script will this live in?)
        //attackDamage = attackDamage + brawn;
    }

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

    void OnCollisionEnter2D(Collision2D other)
  	{
  		if(other.transform.tag == "MovingPlatform")
  		{
  		    transform.parent = other.transform;
  		}
  	}

  	void OnCollisionExit2D(Collision2D other)
  	{
  		if(other.transform.tag == "MovingPlatform")
  		{
  			transform.parent = null;
  		}
  	}
}
