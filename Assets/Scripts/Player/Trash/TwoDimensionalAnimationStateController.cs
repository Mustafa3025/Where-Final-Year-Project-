using UnityEngine;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{

    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float decceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        bool pressedFoward = Input.GetKey("w");
        bool pressedLeft = Input.GetKey("a");
        bool pressedRight = Input.GetKey("d");
        bool pressedRun = Input.GetKey("left shift");

        float currentMaxVelocity = pressedRun ? maximumRunVelocity : maximumWalkVelocity;

        //Increasesplayer velocity when foward key is pressed and run key is not pressed
        //--and velocity is less than 0.5
        if (pressedFoward && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        //Increasesplayer velocity when left key is pressed and run key is not pressed
        //--and velocity is greater than -0.5
        if (pressedLeft && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        //Increasesplayer velocity when right key is pressed and run key is not pressed
        //--and velocity is less than 0.5
        if (pressedRight && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        //Decreased Velocity of the player when the foward key is not pressed
        if (!pressedFoward && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * decceleration;
        }

        //Resetting velocityZ to zero jst in case 
        if (!pressedFoward && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        //Decreasing Velocity of the left side
        if (!pressedLeft && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * decceleration;
        }

        //Decreasing Velocity of the right side
        if (!pressedRight && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * decceleration;
        }

        //Resetting Velocity X
        if (!pressedLeft && !pressedRight && velocityX != 0.0f && (velocityX > -0.5f && velocityX > 0.5f)) 
        {
            velocityX = 0.0f;
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }
}
