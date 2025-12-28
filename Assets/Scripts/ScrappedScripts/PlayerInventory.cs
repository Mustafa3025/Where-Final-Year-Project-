using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //the get; private set; means any scripts and get the value but only this script can set the value

    //public int NumberOfBuffCollected {  get; private set; }
    /*public void BuffCollected()
    {
        NumberOfBuffCollected++;
    }
    */

    public bool flagBuffSpeed = false;
    public bool flagBuffVision = false;
    public bool flagBuffSound = false;


    public void SpeedBuff()
    {
        flagBuffSpeed = true;
    }
    public void SpeedDeBuff()
    {
        flagBuffSpeed = false;
    }

    public void VisionBuff()
    {
        flagBuffVision = true;
    }
    public void VisionDeBuff()
    {
        flagBuffVision = false;
    }

    public void SoundBuff()
    {
        flagBuffSound = true;
    }
    public void SoundDeBuff()
    {
        flagBuffSound = false;
    }

}
