using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonSFXs : MonoBehaviour
{

    public AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;
    



    public void HoverSound()
    {
        myFX.PlayOneShot(hoverFX);
        Debug.Log("Hover SFX Played");
    }

    public void ClickSound()
    {   
        myFX.PlayOneShot(clickFX);
        Debug.Log("Click SFX Played");
    }
}
