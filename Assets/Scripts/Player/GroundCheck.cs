using UnityEngine;
/*
public class GroundCheck : MonoBehaviour
{

    public PlayerController playerController;
    private void OnTriggerEnter(Collider other)
    {

        //if (other.gameObject  == playerController.gameObject)
        if (!other.CompareTag("Ground"))
        {
            return;
        }
        playerController.setGrounded(true);
    }

    private void OnTriggerExit(Collider other)
    {

        //if (other.gameObject == playerController.gameObject)
        if (!other.CompareTag("Ground"))
        {
            return;
        }
        //playerController.setGrounded(false);
    }
    private void OnTriggerStay(Collider other)
    {

        //if (other.gameObject == playerController.gameObject)

        if (!other.CompareTag("Ground"))
        {
            return;
        }
        playerController.setGrounded(true);
    }

}
*/