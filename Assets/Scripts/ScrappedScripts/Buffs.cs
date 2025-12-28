using UnityEngine;
using UnityEngine.UI;


public class Buffs : MonoBehaviour
{
    /*
     private void OnTriggerEnter(Collider other)
     {
         PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

         //if the player inventory isnt null then we know that the collision is with the player
         if (playerInventory != null)
         {
             playerInventory.BuffCollected();
             //setting the buff to inactive once its collected

             gameObject.SetActive(false);
         }
     }
    */
    
    public string gameObjectName;

    private void OnTriggerEnter(Collider collision)
    {
        //if(collision.CompareTag("Player"))
        PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
        gameObjectName = collision.gameObject.ToString();
        if (playerInventory.flagBuffSpeed && gameObjectName == "Buff Speed")
        {
            Debug.Log("Obtained Speed Buff");
            gameObject.SetActive(false);
        }

        if (playerInventory.flagBuffVision && gameObjectName == "Buff Vision")
        {
            Debug.Log("Obtained Vision Buff");
            gameObject.SetActive(false);
        }

        if (playerInventory.flagBuffSound && gameObjectName == "Buff Sound")
        {
            Debug.Log("Obtained Sound Buff");
            gameObject.SetActive(false);
        }
    }
}
