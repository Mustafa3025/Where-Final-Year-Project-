using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSettigns : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider audioSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("volumeChanges"))
        {
            LoadVolume();
        }
        else
        {
            SetGlobalAudio();
        }
    }

    public void SetGlobalAudio()
    {
        float volume = audioSlider.value;
        myMixer.SetFloat("MasterVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("volumeChanges", volume);
    }
     
    private void LoadVolume()
    {
        audioSlider.value = PlayerPrefs.GetFloat("volumeChanges");
        SetGlobalAudio();
    }


}
