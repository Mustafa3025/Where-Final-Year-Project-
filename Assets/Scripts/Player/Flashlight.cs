using UnityEngine;
using UnityEngine.InputSystem;
public class Flashlight : MonoBehaviour
{

    public GameObject ON;
    public GameObject OFF;
    private bool isOn;

    public bool FlashlightIsOn
    {
        get
        {
            return isOn;
        }
        set
        {
            isOn = value;
        }
    }


    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        isOn = false;
    }


    public void ToggleFlashlightInput(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        else
        {
            isOn = !isOn;
            ON.SetActive(isOn);
            OFF.SetActive(!isOn);
        }
    }

    /*
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
            {
                OFF.SetActive(true);
                ON.SetActive(false);
            }
            if (!isOn)
            {
                OFF.SetActive(false);
                ON.SetActive(true);
                isOn = false;       
            }

                isOn = !isOn;
        }

    }
    */


}
