using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class PlayerController : MonoBehaviour
{
    private PlayerInput PlayerInput;

    public Rigidbody rb;
    public GameObject camHolder;
    public Camera cam;
    public PlayerInteraction interaction;
    //public GameObject flashlight;
    public SpotLight flashlight;
    public bool flashlightIsOn = false;
    public float flashlightRange = 60f;

    //public CinemachineCamera cam;   

    public float speedNormal, speedSprint, sensitivity, maxForce, jumpForce;
    private Vector2 move, look;
    private float lookRotation;
    [SerializeField] private float _buffSpeedValue = 0f;
    [SerializeField] private float _buffVisionValue = 0f;
    [SerializeField] private bool _buffVisionFlag;

    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private MainMenu inGameMenuClass;

    private Flashlight FlashLightComponent;
    private BuffCollection BuffCollectionComponent;


    public bool groundedFlag, sprintingFlag;
    //public bool groundedFlag, sprintingFlag, speedBuffFlag;
    //public bool speedBuff = GetComponent<BuffCollection>().BuffSpeedFlag;
    
    public bool inGameMenuPressedFlag = false;


    public void CursorVisibility()
    {
        if (inGameMenu.gameObject.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
 
    public void ActionMapSwitchPlayer()
    {
        PlayerInput.SwitchCurrentActionMap("Player");
    }

    public void ActionMapSwitchUI()
    {
        PlayerInput.SwitchCurrentActionMap("UI");
    }

    public void OnMenuTabDep_()
    {
        /* Old input code no scalability
     if (Input.GetKeyDown(KeyCode.X))
     {
         inGameMenuPressedFlag = true;
         Debug.Log("Menu Tab being Called");
         if (inGameMenu.gameObject.activeSelf == false)
         {
             Debug.Log("settingsFlag and inGameMenu True");
             inGameMenu.gameObject.SetActive(true);
             Cursor.lockState = CursorLockMode.Confined;
             Cursor.visible = true;

         }


     }
     */


        /* bugged for some reason idk why lol
        if (Input.GetKeyDown(KeyCode.X) && inGameMenuPressedFlag == true)
        {
            inGameMenu.gameObject.SetActive(false);
            inGameMenuPressedFlag = false;
        }
        */
    }
    public void OnMenuTab(InputAction.CallbackContext context)
    {
        //CursorVisibility();
        if (context.performed)
        { 
            inGameMenuPressedFlag = true;
            Debug.Log("Menu Tab being Called");
            if (inGameMenu.gameObject.activeSelf == false)
            {
                ActionMapSwitchUI();
                Debug.Log("Actionmap switched to UI");
                Debug.Log("settingsFlag and inGameMenu True");
                inGameMenu.gameObject.SetActive(true);
                inGameMenuClass.OpenOptionsMenu();
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

            }

        }
        /*
        if (!context.performed)
        { 
            PlayerInput.SwitchCurrentActionMap("Player");
            Debug.Log("Actionmap switched to Player");
        }*/


    }

    public void OnFlashlight(InputAction.CallbackContext context)
    {

    }

    public void OnInteract(InputAction.CallbackContext context)
    {

        if(!context.performed)
        {
            return;
        }
        
        if(interaction.CurrentInteractable == null) 
        {
            Debug.Log("No Object to interact with");
            return;
        }

        if (interaction.CurrentInteractable.timedObject)
        {
            interaction.CurrentInteractable?.InteractionTimed();
            Debug.Log("Timed Object Interacted");
        }
        if (!interaction.CurrentInteractable.timedObject)
        {

            interaction.CurrentInteractable?.InteractionNormal();
            Debug.Log("Normal Object Interacted");
        }

       

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        //Debug.Log("OnMove Called");

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>(); 
        //Debug.Log("OnLook Called");

    }
    public void OnJump(InputAction.CallbackContext context)
    {
    //jump = context.ReadValue<Vector2>();
        Jump();
        //Debug.Log("OnJump Called");

    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //sprinting = context.ReadValue<bool>();
           

        if (context.performed)
        {
            sprintingFlag = true;
                
        }   
        else if (context.canceled)
        {
            sprintingFlag = false;
        }
 
        //Debug.Log("OnSprint Called");
    }

    //Hidding mouse icon in the game view window so it doesnt look wierd and annoying
    void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();
        interaction = GetComponentInChildren<PlayerInteraction>();
        BuffCollectionComponent = GetComponent<BuffCollection>();
        FlashLightComponent = GetComponentInChildren<Flashlight>();
        
        //inGameMenuClass = GetComponent<MainMenu>();
        Cursor.lockState = CursorLockMode.Locked;
        Physics.gravity = new Vector3(0, -34f, 0);

        flashlightRange = flashlight.range;
    }

    //WASD movement
    void Move()
    {
        //_buffSpeedValue = GetComponent<BuffCollection>().BuffSpeed;
        //_buffVisionValue = GetComponent<BuffCollection>().BuffVision;
        //bool _buffVisionFlag = GetComponent<BuffCollection>().BuffVisionFlag;
        //flashlightIsOn = GetComponentInChildren<Flashlight>().FlashlightIsOn;
        _buffVisionValue = BuffCollectionComponent.BuffVision;
        _buffSpeedValue = BuffCollectionComponent.BuffSpeed;
        _buffVisionFlag = BuffCollectionComponent.BuffVisionFlag;
        flashlightIsOn =FlashLightComponent.FlashlightIsOn;


        if(!_buffVisionFlag && flashlightIsOn)
        {
            flashlightRange = 60;
            Debug.Log("Flashlight Range " + flashlightRange);
        }
        if(_buffVisionFlag && flashlightIsOn)
        {

            flashlightRange = _buffVisionValue;
            Debug.Log("Flashlight Range Buffed " + flashlightRange);
        }

        //Finding Target Velocity
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
       
        //Was tying out to change the FOV liek skyrim sprint using the nnormal and the cinemachine doo hikey

        if (sprintingFlag) 
        {

            targetVelocity *= speedNormal + speedSprint + _buffSpeedValue;
            //Debug.Log("Normal Speed = " + speedNormal.ToString() + " || Sprint Speed = " + speedSprint.ToString() + " || Buff Speed = " + _buffSpeedValue.ToString());
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 69, 10 * Time.deltaTime);
            //cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, 69, 10 * Time.deltaTime);
            if (_buffSpeedValue > 0)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 80, 10 * Time.deltaTime);
                //cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, 80, 10 * Time.deltaTime);

            }

        }
        else
        {
            targetVelocity *= speedNormal + _buffSpeedValue;
            //Debug.Log("Normal Speed = " + speedNormal.ToString() + " || Buff Speed = " + _buffSpeedValue.ToString());
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 50, 10 * Time.deltaTime);
            //cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, 50, 10 * Time.deltaTime);
            if (_buffSpeedValue > 0)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 80, 10 * Time.deltaTime);
                //cam.Lens.FieldOfView = Mathf.Lerp(cam.Lens.FieldOfView, 90, 10 * Time.deltaTime);

            }
        }

        //Debug.Log("Camera FOV = " + cam.fieldOfView.ToString());



        //Align Direction
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate Forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //limit Force
        Vector3.ClampMagnitude(velocityChange, maxForce);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
            
    }
        
    // Controls the turning of the player via mouse,  both horizontally and vertically
    // Also setting a range for the max and min vertical movement so the camera doesntflip into a 360
    void Look()
    {
            
        transform.Rotate(Vector3.up * look.x * sensitivity);
            
        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
            
        //Debug.Log($"Look Input: {look}");

    }

    void Jump()
    {
        Vector3 jumpForces = Vector3.zero;
            
        if (groundedFlag)
        {
            jumpForces = Vector3.up * jumpForce;
        }
        rb.AddForce(jumpForces, ForceMode.Impulse);         
    
    }



    private void Update()
    {
        //Move();
        Look();
    }

    private void FixedUpdate()
    {
        Move();

        //Jump();
    }

    /* void Update()
     {
         //Debug.Log("Update is running");
     }
    */
    //The reason for doing this is so that we want to move our camera after the scene is updated
    //Generally a good practice (making the camera move after the player moves)
    void LateUpdate()
    {
           // Look();
            CursorVisibility();
            //OnMenuTab(); Old part when the code used Fixed Keyboard binding
            //Debug.Log("Late Update is running");
    }


    public void setGrounded(bool state)
    {
        groundedFlag = state;
    
    }
}
