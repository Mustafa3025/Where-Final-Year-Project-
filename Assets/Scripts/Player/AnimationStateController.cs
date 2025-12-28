using Unity.Hierarchy;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.1f;
    int velocityHash;
    int isWalkingHash;
    int isRunningHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        velocityHash = Animator.StringToHash("Velocity");
    }

    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool pressFoward = Input.GetKey("w");
        bool pressRun = Input.GetKey("left shift");


        if (pressFoward && velocity < 1.0f)
        {
            velocity += Time.deltaTime * deceleration;
        }

        if (!pressFoward && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * acceleration;
        }

        if (!pressFoward && velocity < 0.0f)
        {
            velocity = 0.0f;
        }

        animator.SetFloat(velocityHash, velocity);

        /*
        if (!isWalking && pressFoward)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !pressFoward)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (pressFoward && pressRun))
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!pressFoward || !pressRun))
        {
            animator.SetBool(isRunningHash, false);
        }
        */

    }
}
