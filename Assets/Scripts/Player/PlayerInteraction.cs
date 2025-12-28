using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{

    public Camera PlayerCamera;
    public float InteractionDistance = 3f;
    public GameObject interactionText;
    private InteractableObject currentInteractable;

    public InteractableObject CurrentInteractable
    {
        get 
        { 
            return currentInteractable; 
        }
    }


    void LateUpdate()
    {
        Ray ray = PlayerCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionDistance))
        {
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
            if (interactableObject != null && interactableObject != currentInteractable)
            {
                currentInteractable = interactableObject;
                interactionText.SetActive(true);
                //interactionText = currentInteractable.GetInteractionText();
                TextMeshProUGUI textComponment = interactionText.GetComponent<TextMeshProUGUI>();
                if (textComponment != null) 
                {
                    textComponment.text = currentInteractable.GetInteractionText();
                }

            }
        }

        else
        {
            currentInteractable = null;
            interactionText.SetActive(false);
        }

        /*
        //if (Input.GetKeyDown(KeyCode.E) && !currentInteractable.timedObject)
        if (!currentInteractable.timedObject)
        {
            currentInteractable?.InteractionNormal();
            Debug.Log("Normal Object Interacted");
        }


        //if (Input.GetKeyDown(KeyCode.E) && currentInteractable.timedObject)
        if (currentInteractable.timedObject)
        {
            currentInteractable?.InteractionTimer();
            Debug.Log("Timed Object Interacted");
        }
        */
    }
}
