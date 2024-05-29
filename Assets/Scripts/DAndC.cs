using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAndC : MonoBehaviour
{
    public CashPaymentCheckout paymentCheckout;
    public float myValue;

    public void AddToChange()
    {
        paymentCheckout.AddPayment(myValue);
    }
}
