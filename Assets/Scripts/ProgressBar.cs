using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    private Slider slider;
    private float targetProgess = 0;
    public float fillSpeed = 0.5f;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        IncrementProgress(0.75f);
    }
    public void Update()
    {
        if (slider.value < targetProgess)
            slider.value += fillSpeed * Time.deltaTime;
    }


    public void IncrementProgress(float newProgress)
    {
        targetProgess = slider.value + newProgress;
    }

}
