using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MovementController))]
public class Player : MonoBehaviour {

    public float jumpHeight = 4;
    public float timeToJumpApex = 0.4F;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float velocityXSmoothing;

    private float moveSpeed = 15;
    private float gravity;
    private float jumpVelocity;
    private float input;
    Vector3 velocity;
    MovementController controller;

    void Start()
    {
        controller = GetComponent<MovementController>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        input = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input * moveSpeed;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
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
