using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MovementController))]
public class Player : MonoBehaviour {

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = 0.4F;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float velocityXSmoothing;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = 0.25f;
    public float timeToWallUnstick;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    private float moveSpeed = 15;
    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private float input;
    Vector3 velocity;
    MovementController controller;

    void Start()
    {
        controller = GetComponent<MovementController>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2*Mathf.Abs(gravity)* minJumpHeight);
    }

    void Update()
    {
        Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int wallDirX = (controller.collisions.left)?-1:1;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        bool wallSliding = false;
        if((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0){
            wallSliding = true;

            if(velocity.y <-wallSlideSpeedMax){
              velocity.y = -wallSlideSpeedMax;
            }

            if(timeToWallUnstick > 0){
              velocityXSmoothing = 0;
              velocity.x = 0;

              if(input.x != wallDirX && input.x != 0){
                timeToWallUnstick -= Time.deltaTime;
              }
              else{
                timeToWallUnstick = wallStickTime;
              }
            }
            else{
              timeToWallUnstick = wallStickTime;
            }
        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        //input = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown (KeyCode.Space))
        {
            if(wallSliding){
              if(wallDirX == input.x){
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
              }
              else if(input.x == 0){
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
              }
              else{
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
              }
            }
            if(controller.collisions.below){
              velocity.y = maxJumpVelocity;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space)){
          if(velocity.y > minJumpVelocity){
            velocity.y = minJumpVelocity;
          }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.move(velocity * Time.deltaTime);
    }

    public float getInput()
    {
        return input;
    }

    public Bounds getBounds()
    {
        return controller.getBounds();
    }
}
