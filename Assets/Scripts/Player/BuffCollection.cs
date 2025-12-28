using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffCollection : MonoBehaviour
{

    private bool _buffSpeedFlag;
    private bool _buffSoundFlag;
    private bool _buffVisionFlag;

    public bool BuffVisionFlag
    {
        get 
        { 
            return _buffVisionFlag; 
        }
        set
        { 
            _buffVisionFlag = value;
        }
    }


    /*public bool BuffSpeedFlag
    {
        get
        {
            return _buffSpeedFlag;
        }
        set 
        {   
            _buffSpeedFlag = value;
        }
    }*/


    [SerializeField] private float _buffSpeedValue = 0f;
    public float BuffSpeed
    {
        get
        {
            return _buffSpeedValue;
        }
        set
        {
            _buffSpeedValue = value;
        }
    }

    [SerializeField] private float _buffSoundValue = 0f;

    public float BuffSound
    {
        get
        {
            return _buffSoundValue;
        }
        set
        {
            _buffSoundValue = value;
        }
    }



    [SerializeField] private float _buffVisionValue = 0f;

    
    public float BuffVision
        {
            get
            {
                return _buffVisionValue;
            }
            set
            {
                _buffVisionValue = value;
            }
        }
    


    //public TextMeshProUGUI BuffTextSpeed;
    //public TextMeshProUGUI BuffTextSound;
    //public TextMeshProUGUI BuffTextVision;

    public GameObject BuffVisualsSpeed;
    public GameObject BuffVisualsSound;
    public GameObject BuffVisualsVision;
    


    public Image ImageSpeed;
    public Image ImageSound;
    public Image ImageVision;

    public TextMeshProUGUI BuffAntiStack;





    IEnumerator HideGUI(GameObject guiObject, float secondsToWait, bool show = false)
    {
            yield return new WaitForSeconds(secondsToWait);
            guiObject.SetActive(show);
    }

    private void ActivateBuff(Image _objectImage, GameObject _objectVisuals, ref bool _flag, ref float _buffValue, float valueAssign, string buffName)
    {
            _objectImage.gameObject.SetActive(true);
            _objectVisuals.SetActive(true);
            _flag = true;
            _buffValue = valueAssign;
            Debug.Log(buffName + " Buff Value" + _buffValue);
            //Debug.Log(buffName + " " + _flag);
            Debug.Log(buffName + "Activated");
            TimedDeactivation(_objectImage, _objectVisuals, 10, buffName);
    }

    private void DeActivateBuff(ref bool _flag, string buffName)
    { 
            _flag = false;
            Debug.Log(buffName + "Deactivated");
            //Debug.Log(buffName + " " + _flag);

    }

    IEnumerator ResetBuff(Image _objectImage, GameObject _objectVisuals, float secondsToWait, string buffName, bool show = false)
    {
            yield return new WaitForSeconds(secondsToWait);
            _objectImage.gameObject.SetActive(false);
            _objectVisuals.SetActive(false);
        
            if(buffName == "Speed")
            {

                BuffSpeed = 0;
                DeActivateBuff(ref _buffSpeedFlag, buffName);
                //Debug.Log(buffName + " Buff Value " + _buffSpeedValue);

            } 

            if(buffName == "Vision")
            {
                BuffVision = 60;
                DeActivateBuff(ref _buffVisionFlag, buffName);
            }

            if(buffName == "Sound")
            {
                BuffSound = 0f;
                DeActivateBuff(ref _buffSoundFlag, buffName);
            }

    }

    private void TimedDeactivation(Image _objectImage, GameObject _objectVisuals, float secondsToWait, string buffName, bool show = false)
    {
            StartCoroutine(ResetBuff(_objectImage, _objectVisuals, secondsToWait, buffName, show));  
    }



    private void OnTriggerEnter(Collider collision)
    {


        if (collision.CompareTag("BuffSpeed"))
        {
            /*
            Buff++;
            BuffText.text = "Buffs: " + Buff;
            Debug.Log(Buff);
            Destroy(collision.gameObject);
            */
            if (!_buffSpeedFlag)
            {
                //BuffTextSpeed.gameObject.SetActive(true);

                /*

                ImageSpeed.gameObject.SetActive(true);
                BuffVisualsSpeed.gameObject.SetActive(true);
                _buffSpeedFlag = true;
                BuffSpeed = 5f;
                Debug.Log("Speed Buff Value " + BuffSpeed);

                */
                ActivateBuff(ImageSpeed, BuffVisualsSpeed, ref _buffSpeedFlag, ref _buffSpeedValue, 5, "Speed");
                Destroy(collision.gameObject);
            }
            else
            {
                BuffAntiStack.gameObject.SetActive(true);
                StartCoroutine(HideGUI(BuffAntiStack.gameObject, 2.0f));
            }


        }

        if (collision.CompareTag("BuffSound"))
        {
            
            if (!_buffSoundFlag)
            {
                //BuffTextSound.gameObject.SetActive(true);

                ActivateBuff(ImageSound, BuffVisualsSound, ref _buffSoundFlag, ref _buffSoundValue, 60, "Sound");
                Destroy(collision.gameObject);
            }
            else
            {
                BuffAntiStack.gameObject.SetActive(true);
                StartCoroutine(HideGUI(BuffAntiStack.gameObject, 2.0f));
            }
            
        }

        if (collision.CompareTag("BuffVision"))
        {
            
            if (!_buffVisionFlag)
            {
                //BuffTextVision.gameObject.SetActive(true);

                ActivateBuff(ImageVision, BuffVisualsVision, ref _buffVisionFlag, ref _buffVisionValue, 60, "Vision");
                Destroy(collision.gameObject);
            }
            else
            {
                BuffAntiStack.gameObject.SetActive(true);
                StartCoroutine(HideGUI(BuffAntiStack.gameObject, 2.0f));
            }
            
        }
    }

   
}
