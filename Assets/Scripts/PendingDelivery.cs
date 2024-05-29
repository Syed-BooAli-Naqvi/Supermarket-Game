using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PendingDelivery : MonoBehaviour
{
    public enum TimerState
    {
        none,inProgress,Ended
    }
    public GameObject deliveryObj;
    public TMP_Text timeTxt;
    public List<ProductAmounts> productAmounts;
    public List<DeliveryBox> deliveryBoxes;
    public int currentIndex = 0;
    public TimerState timerState;
    public Transform[] instanciatePoints;
    public Button bankBtn;

    // Start is called before the first frame update
    void Start()
    {
        SpeedUpStatic = false;
        if (PlayerPrefs.HasKey(SharedPref.PendingDeliveries))
        {
            productAmounts = PlayerPrefsExtra.GetList(SharedPref.PendingDeliveries, new List<ProductAmounts>());

            for (int i = 0; i < productAmounts.Count; i++)
            {
                if (productAmounts[i].isCompleted)
                {
                    if (!productAmounts[i].isPlaced)
                        StartCoroutine(InstantiateBoxes(currentIndex, productAmounts[i].totalAmount));
                    currentIndex++;
                }
            }
            timerState = TimerState.inProgress;
            StartCoroutine(RunTimer());
        }
    }

    IEnumerator RunTimer()
    {
        deliveryObj.SetActive(true);
        while (currentIndex < productAmounts.Count)
        {
            // Start the timer for the current product
            lightTxt.text = productAmounts[currentIndex].light.ToString();
            yield return StartCoroutine(StartProductTimer(productAmounts[currentIndex]));

            // Once the timer is completed, set isCompleted to true for the current product
            productAmounts[currentIndex].isCompleted = true;
            Save();
            StartCoroutine(InstantiateBoxes(currentIndex, productAmounts[currentIndex].totalAmount));
            Debug.Log("Delivery Complete");
            // Move to the next ProductAmounts
            currentIndex++;

            // Reset currentIndex to 0 if all ProductAmounts are processed
            if (currentIndex >= productAmounts.Count)
            {
                timerState = TimerState.Ended; // Set the flag to true when the coroutine ends
            }
        }
        timerState = TimerState.Ended;
        deliveryObj.SetActive(false);
    }
    public Transform[] boxPos;
    public TMP_Text lightTxt;
    public IEnumerator InstantiateBoxes(int currentIndeX, int totalAmount)
    {
        for (int i = 0; i < totalAmount; i++)
        {
            DeliveryBox deliveryBox = deliveryBoxes.Find(box => box.productAmount.myProductType == productAmounts[currentIndeX].myProductType);
            if (deliveryBox != null)
            {
                deliveryBox.productAmount.deliveryNum = currentIndeX;
                Instantiate(deliveryBox).transform.position = instanciatePoints[Random.Range(0, instanciatePoints.Length)].position;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public bool speedUp { get { return SpeedUpStatic; } }
    public static bool SpeedUpStatic;
    public void SpeedUp(bool light)
    {
        if (light)
        {
            if (PlayerPrefs.GetInt(SharedPref.Light, 50)>= productAmounts[currentIndex].light)
            {
                PlayerPrefs.SetInt(SharedPref.Light, PlayerPrefs.GetInt(SharedPref.Light, 50) - productAmounts[currentIndex].light);
                SpeedUpStatic = true;
            }
            else
            {
                bankBtn.onClick.Invoke();
            }
        }
        else
        {
            SpeedUpStatic = true;
        }
    }

    IEnumerator StartProductTimer(ProductAmounts product)
    {
        // Start the timer for the specified duration
        float timer;
        if (PlayerPrefs.GetInt(SharedPref.FirstDelivery,0)!=1)
        {
            PlayerPrefs.SetInt(SharedPref.FirstDelivery, 1);
            timer = 4;
        }
        else
        {
            timer = product.deliveryTime;
        }
        while (timer > 0)
        {
            timeTxt.text = "Next Delivery in: " + ConvertSecondsToTime(timer);
            yield return new WaitForSeconds(1);
            timer -= 1;
            if (speedUp)
            {
                SpeedUpStatic = false;
                timer = 0;
            }
            product.deliveryTime = timer;
            Debug.Log("check");
            PlayerPrefsExtra.SetList(SharedPref.PendingDeliveries, productAmounts);
        }
    }


    string ConvertSecondsToTime(float totalSeconds)
    {
        int hours = (int)(totalSeconds / 3600);
        float remainingSeconds = totalSeconds % 3600;
        int minutes = (int)(remainingSeconds / 60);
        int seconds = (int)(remainingSeconds % 60);

        string timeString = "";

        if (hours > 0)
        {
            timeString += hours + "h ";
        }

        if (minutes > 0 || hours > 0)
        {
            timeString += minutes + "m ";
        }

        timeString += seconds + "s";

        return timeString;
    }
    public void Save()
    {
        PlayerPrefsExtra.SetList(SharedPref.PendingDeliveries, productAmounts);
        //productAmounts = PlayerPrefsExtra.GetList(SharedPref.PendingDeliveries, new List<ProductAmounts>());
        if (TimerEnded() == TimerState.Ended)
        {
            currentIndex = 0;
            for (int i = 0; i < productAmounts.Count; i++)
            {
                if (productAmounts[i].isCompleted)
                {
                    currentIndex++;
                }
            }
            timerState = TimerState.inProgress;
            StartCoroutine(RunTimer());
        }
    }

    // Example method to check if the timer coroutine has ended
    public TimerState TimerEnded()
    {
        return timerState;;
    }
}
