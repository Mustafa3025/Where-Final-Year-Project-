using UnityEngine;

public class twoDimensionalAnimationStateController : MonoBehaviour
{

    //z is forward and backwards, and x is left and right

    Animator animator;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 2.0f;
    public float decceleration = 2.0f;
    public float maxRunVelocity = 0.5f;
    public float maxSprintVelocity = 2.0f;

    int VelocityXHash;
    int VelocityZHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");
    }

    void changeVelocity(bool pressForward, bool pressBackwards, bool pressLeft, bool pressRight, float currentMaxVelocity)
    {
        // Acceleration part //

        //Increasing Velocity in the Z direction
        if (pressForward && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        //Increaseing Velocity in the Z -ve direction

        if (pressBackwards && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        //Increasing Velocity in the X, -ve direction
        if (pressLeft && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        //Increasing Velocity in the X, +ve direction
        if (pressRight && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
       



        // Deecleration part //

        //Slow decrease Velocity Z
        if (!pressForward && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * decceleration;
        }

        if(!pressBackwards && velocityZ < -0.0f)
        {
            velocityZ += Time.deltaTime * decceleration;
        }

        //Decceleartion Left and Right //

        if (!pressLeft && velocityX < -0.0f)
        {
            velocityX += Time.deltaTime * decceleration;
        }

        if (!pressRight && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * decceleration;
        }

    }


    void lockOrResetVelocity(bool pressForward, bool pressBackwards, bool pressLeft, bool pressRight, float currentMaxVelocity)
    {

        //Resetting to 0 incase decceleration goes -ve for some reason
        /*
        if (!pressForward && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }
        */

        //Resetting if both forward and backward are not pressed
        if (!pressForward && !pressBackwards && velocityZ != 0.0f && (velocityZ < 0.5f && velocityZ > -0.5f))
        {
            velocityZ = 0.0f;
        }

        //Resetting if both left and right are not pressed

        if (!pressLeft && !pressRight && velocityX != 0.0f && (velocityX < 0.5f && velocityX > -0.5f))
        {
            velocityX = 0.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        bool pressForward = Input.GetKey(KeyCode.W);
        bool pressBackwards = Input.GetKey(KeyCode.S);
        bool pressLeft = Input.GetKey(KeyCode.A);
        bool pressRight = Input.GetKey(KeyCode.D);
        bool pressSprint = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = pressSprint ? maxSprintVelocity : maxRunVelocity;

        changeVelocity(pressForward, pressBackwards, pressLeft, pressRight, currentMaxVelocity);
        lockOrResetVelocity(pressForward, pressBackwards, pressLeft, pressRight, currentMaxVelocity);


        //Setting Animator Variables with the variables defined here//

        //This isbefore code refactoring
        //animator.SetFloat("Velocity Z", velocityZ);
        //animator.SetFloat("Velocity X", velocityX);
        animator.SetFloat(VelocityXHash, velocityX);
        animator.SetFloat(VelocityZHash, velocityZ);


    }

}
