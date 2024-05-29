using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPaymentCheckout : MonoBehaviour
{
    public TMP_Text totalTxt, givingTxt;
    public float total;
    public Button ok;
    public CheckoutHandler checkoutHandler;

    public void CompletePayment()
    {
        PlayerPrefs.SetFloat(SharedPref.Cash, PlayerPrefs.GetFloat(SharedPref.Cash, 100) + total);
        gameObject.SetActive(false);
        checkoutHandler.CompleteCheckout();
    }

    public void SetUI(float total)
    {
        this.total = total;
        totalTxt.text = "$" + total.ToString("#0.00");
        ok.interactable = false;
        givingTxt.text = "$0.00";
    }

    public void AddPayment(string give)
    {
        if ((give == "." && givingTxt.text.Contains(".")) || (givingTxt.text.Length > 6))
            return;
        if (string.IsNullOrEmpty(givingTxt.text))
            givingTxt.text = "$0.00";
        else if (givingTxt.text == "$0.00" && give == ".")//(give == "0" || give == "."))
            return;
        else if (givingTxt.text == "$0.00" && give != ".")// give != "0" && give != ".")
            givingTxt.text = "$";

        givingTxt.text += give;
        //bool isGood = givingTxt.text == totalTxt.text;
        bool isGood =
            givingTxt.text.Contains("1") ||
            givingTxt.text.Contains("2") ||
            givingTxt.text.Contains("3") ||
            givingTxt.text.Contains("4") ||
            givingTxt.text.Contains("5") ||
            givingTxt.text.Contains("6") ||
            givingTxt.text.Contains("7") ||
            givingTxt.text.Contains("7") ||
            givingTxt.text.Contains("8") ||
            givingTxt.text.Contains("9");
        ok.interactable = isGood;
        givingTxt.color = isGood ? Color.green : Color.red;
    }

    public void RemoveLast()
    {
        if (givingTxt.text == "$0.00")
            return;
        givingTxt.text = givingTxt.text.Remove(givingTxt.text.Length - 1);
        if (givingTxt.text == "$")
            givingTxt.text = "$0.00";

        bool isGood =
            givingTxt.text.Contains("1") ||
            givingTxt.text.Contains("2") ||
            givingTxt.text.Contains("3") ||
            givingTxt.text.Contains("4") ||
            givingTxt.text.Contains("5") ||
            givingTxt.text.Contains("6") ||
            givingTxt.text.Contains("7") ||
            givingTxt.text.Contains("7") ||
            givingTxt.text.Contains("8") ||
            givingTxt.text.Contains("9");
        ok.interactable = isGood;
        givingTxt.color = isGood ? Color.white : Color.red;
    }

    public void ResetAll()
    {
        SetUI(total);
    }
}
