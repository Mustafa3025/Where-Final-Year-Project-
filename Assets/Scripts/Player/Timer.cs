using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI elaspedTimeText;
    float elapsedTime;
    [SerializeField] TextMeshProUGUI remainingTimeText;
    [SerializeField] float remainingTime;
    [SerializeField] bool elaspedTimeFlag = false;
    [SerializeField] bool remainingTimeFlag = true;

    [SerializeField] GameObject elaspedTimeTextOBJ;
    [SerializeField] GameObject remainingTimeTextOBJ;

    [SerializeField] private bool gameStartFlag = false;

    void Update()
    {
        if (gameStartFlag)
        {

            if (Input.GetKeyDown(KeyCode.P))
            {
                elaspedTimeFlag = !elaspedTimeFlag;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                remainingTimeFlag = !remainingTimeFlag;
            }


            if (elaspedTimeFlag)
            {
                elaspedTimeTextOBJ.gameObject.SetActive(true);
            }
            else if (!elaspedTimeFlag)
            {
                elaspedTimeTextOBJ.gameObject.SetActive(false);
            }


            if (remainingTimeFlag)
            {
                remainingTimeTextOBJ.gameObject.SetActive(true);
            }
            else if (!remainingTimeFlag)
            {
                remainingTimeTextOBJ.gameObject.SetActive(false);
            }


            elapsedTime += Time.deltaTime;   //Gives the elasped time between 2 frames
            int minutesET = Mathf.FloorToInt(elapsedTime / 60);
            int secondsET = Mathf.FloorToInt(elapsedTime % 60);
            //timerText.text = elapsedTime.ToString();
            elaspedTimeText.text = string.Format("{0:00} : {1:00}", minutesET, secondsET);

            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                remainingTimeText.color = Color.red;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                remainingTimeText.color = Color.green;
            }

            if (remainingTime <= 30)
            {
                remainingTimeText.color = Color.yellow;
            }

            int minutesRT = Mathf.FloorToInt(remainingTime / 60);
            int secondsRT = Mathf.FloorToInt(remainingTime % 60);
            remainingTimeText.text = string.Format("{0:00} : {1:00}", minutesRT, secondsRT);


        }
    }
}
