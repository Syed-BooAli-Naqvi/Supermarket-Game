using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CashPaymentCheckout : MonoBehaviour
{
    public TMP_Text recievedTxt, totalTxt, changeTxt, givingTxt;
    public float recieved, total, change, giving;
    public Button ok;
    public CheckoutHandler checkoutHandler;

    public void CompletePayment()
    {
        gameObject.SetActive(false);
        checkoutHandler.CompleteCheckout();
        PlayerPrefs.SetFloat(SharedPref.Cash, PlayerPrefs.GetFloat(SharedPref.Cash, 100) + total);
    }


    public void SetUI(float recieved, float total)
    {
        recieved = (float)Math.Round(recieved, 2);
        total = (float)Math.Round(total, 2);
        this.recieved = recieved;
        this.total = total;
        change = recieved - total;
        change = (float)Math.Round(change, 2);
        recievedTxt.text = "$" + recieved.ToString("#00.00");
        totalTxt.text = "$" + total.ToString("#00.00");
        changeTxt.text = "$" + change.ToString("#00.00");
        giving = 0;
        givingTxt.text = "$00.00";
        givingTxt.color = Color.red;
        ok.interactable = false;
    }

    public void AddPayment(float give)
    {
        giving += give;
        giving = MathF.Round(giving, 2);
        givingTxt.text = "$" + giving.ToString("#00.00");
        //Debug.Log("giving = " + giving);
        //Debug.Log("change = " + change);
        //Debug.Log("change == change =" + (giving == change));
        bool isGood = giving == change;
        ok.interactable = isGood;
        givingTxt.color = isGood ? Color.green : Color.red;
    }
    
    public void ResetAll()
    {
        SetUI(recieved, total);
    }
}
