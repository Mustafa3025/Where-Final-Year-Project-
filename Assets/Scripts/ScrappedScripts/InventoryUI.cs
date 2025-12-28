using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] Sprite[] buffSprites;
    [SerializeField] Sprite newSprite;
    //PlayerInventory playerInventory
    /*
    if (playerInventory != null)
    {
        newSprite = buffSprites[0];
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory.flagBuffSpeed)
        {
            Debug.Log("Sprite Speed On");
            newSprite = buffSprites[0];
       
        }

        if (playerInventory.flagBuffVision)
        {
            Debug.Log("Sprite Vision On");
            newSprite = buffSprites[1];

        }

        if (playerInventory.flagBuffSound)
        {
            Debug.Log("Sprite Sound On");
            newSprite = buffSprites[2];
        }
    }
}
