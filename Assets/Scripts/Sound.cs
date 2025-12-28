using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;


public class Sound : MonoBehaviour 
{

    public AudioSource sound;
    private bool soundIsOn = false;

    public bool SoundIsOn
    {
        get
        {
            return soundIsOn;
        }
        
        set
        {
            soundIsOn = value;
        }
    }

    public void Awake()
    {
        sound.mute = true;
    }

    IEnumerator TimedSound(float seconds, AudioSource sound)
    {
        yield return new WaitForSeconds(seconds);
        soundIsOn = false;
        sound.mute = true;
        Debug.Log("Alarm Deactivated");
    }

    public void SoundOn(AudioSource sound, float soundTimer)
    {
       
        if (!soundIsOn)
        {
            soundIsOn = true;
            sound.mute = false;
            StartCoroutine(TimedSound(soundTimer, sound));
          
        }
    }


}