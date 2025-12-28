using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{

    public string InteractionText = "Press E";
    public UnityEvent onInteract;
    private Sound _sound;

   
    
    public bool timedObject = false;


    private void Awake()
    {
       _sound = GetComponent<Sound>();
    }

    public string GetInteractionText()
    {
        return InteractionText;
    }


    public void InteractionNormal()
    {
        onInteract.Invoke();
    }

    public void InteractionTimed()
    {
        _sound.SoundOn(_sound.sound, 15);
        Debug.Log("Alarm Activated");
    }



}
