using UnityEngine;

public class GraphicsSettings : MonoBehaviour
{
    private void Start()
    {
        QualitySettings.SetQualityLevel(5, true);

        Debug.Log("Initial Graphics (5)");
    }

    public void QualityLowPoly()
    {
        QualitySettings.SetQualityLevel(0, true);
        Debug.Log("Graphics Changed to Low Poly");
    }

    public void QualityLow()
    {
        QualitySettings.SetQualityLevel(1, true);
        Debug.Log("Graphics Changed to Low ");
    }


    public void QualityMedium()
    {
        QualitySettings.SetQualityLevel(2, true);
        Debug.Log("Graphics Changed to Medium");
    }

    public void QualityHigh()
    {
        QualitySettings.SetQualityLevel(3, true);
        Debug.Log("Graphics Changed to High");
    }

    public void QualityVeryHigh()
    {
        QualitySettings.SetQualityLevel(4, true);
        Debug.Log("Graphics Changed to Very High");
    }

    public void QualityUltra()
    {
        QualitySettings.SetQualityLevel(5, true);
        Debug.Log("Graphics Changed to Ultra");
    }

}
