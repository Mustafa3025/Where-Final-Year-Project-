using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerObj;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Activated Teleport Plate at = " + player.position);
            playerObj.SetActive(false);
            player.position = destination.position;
            Debug.Log("Player Teleported to  = " + player.position);
            playerObj.SetActive(true);
        }

    }
}
