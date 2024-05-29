using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumPad : MonoBehaviour
{
    public string myString;

    public CardPaymentCheckout paymentCheckout;

    public void AddToChange()
    {
        paymentCheckout.AddPayment(myString);
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            AddToChange();
        });
    }
}
