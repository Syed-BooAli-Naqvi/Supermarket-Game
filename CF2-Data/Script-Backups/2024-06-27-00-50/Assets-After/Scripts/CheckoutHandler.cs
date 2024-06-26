using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using static GameManager;

public class CheckoutHandler : MonoBehaviour
{
    public List<CustomerPosition> customerPositions;
    public List<Transform> parentObjT;
    public bool isStarted;
    public bool canStart;
    public GameObject cam;
    public Transform bagT;
    public PaymentState currentPaymentState;
    public float totalPayableAmount;
    public CashPaymentCheckout cashPaymentCheckout;
    public CardPaymentCheckout cardPaymentCheckout;
    public int productCount;
    public CustomerPosition currentCustomer;

    public void CanPlaceAtCheckout(bool check)
    {
        canStart = check;
        cam.SetActive(check);
    }

    public async void StartCheckout(PaymentState paymentState)//, CustomerPosition index)
    {
        if (!canStart)
        {
            return;
        }
        if (isStarted)
            return;
        totalPayableAmount = 0;
        currentCustomer = customerPositions.FindLast(match => match.isOccupied);
        isStarted = true;
        currentPaymentState = paymentState;
        Debug.Log(currentCustomer.customer.name);
        bool isComplete = false;
        for (int j = 0; j < currentCustomer.customer.objT.Count; j++)
        {
            isComplete = true;
            currentCustomer.customer.objT[j].gameObject.SetActive(true);
            currentCustomer.customer.objT[j].SetParent(parentObjT[j]);
            currentCustomer.customer.objT[j].GetComponent<Collider>().enabled = false;
            DOTween.To(() => currentCustomer.customer.objT[j].position, x => currentCustomer.customer.objT[j].position = x, parentObjT[j].position, 0.1f).OnComplete(() =>
            {
                currentCustomer.customer.objT[j].GetComponent<Collider>().enabled = true;
                currentCustomer.customer.objT[j].GetComponent<Product>().canCheckout = true;
                productCount++;
                isComplete = false;
            });
            await AsyncWaiter.WaitUntilAsync(() => !isComplete);
        }
    }

    public Camera checkoutCamera;

    void Update()
    {
        if (ControlFreak2.CF2Input.touchCount > 0)
        {
            ControlFreak2.InputRig.Touch touch = ControlFreak2.CF2Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began)
            {
                CheckTouchOrMouse(touch.position);
            }
        }
        else if (ControlFreak2.CF2Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            CheckTouchOrMouse(ControlFreak2.CF2Input.mousePosition);
        }
    }

    void CheckTouchOrMouse(Vector3 screenPosition)
    {
        Ray ray = checkoutCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (checkoutCamera.gameObject.activeSelf && Physics.Raycast(ray, out hit))
        {
            // Check if the collider of the object is touched
            if (hit.collider.GetComponent<Product>() && hit.collider.GetComponent<Product>().canCheckout)
            {
                hit.collider.enabled = false;
                DOTween.To(() => hit.transform.position, x => hit.transform.position = x, bagT.position, 0.4f);
                totalPayableAmount += hit.collider.GetComponent<Product>().myCurrentPrice;
                productCount--;
                if (productCount == 0)
                {
                    if(currentPaymentState == PaymentState.cash)
                    {
                        cashPaymentCheckout.SetUI(UnityEngine.Random.Range(totalPayableAmount + 10f, totalPayableAmount + 40f), totalPayableAmount);
                        cashPaymentCheckout.gameObject.SetActive(true);
                    }
                    else
                    {
                        cardPaymentCheckout.SetUI(totalPayableAmount);
                        cardPaymentCheckout.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void CompleteCheckout()
    {
        for (int i = 0; i < parentObjT.Count; i++)
        {
            if (parentObjT[i].childCount > 0)
                DestroyImmediate(parentObjT[i].GetChild(0).gameObject);
        }
        for (int i = 0; i < customerPositions[^1].customer.demands.Count; i++)
        {
            customerPositions[^1].customer.shelfHandlers[i].shelf.currentItemIndex -= customerPositions[^1].customer.demands[i].amount;
            customerPositions[^1].customer.shelfHandlers[i].shelf.currentTakenIndex -= customerPositions[^1].customer.demands[i].amount;
            customerPositions[^1].customer.shelfHandlers[i].Save();
        }
        customerPositions[^1].isOccupied = false;
        customerPositions[^1].customer.CompleteCheckout();
        for (int i = 0; i < customerPositions.Count; i++)
        {
            customerPositions[i].isOccupied = false;
            customerPositions[i].customer = null;
        }
        isStarted = false;
        for (int i = 0; i < allCustomer.Count; i++)
        {
            if (allCustomer[i].gameObject.activeSelf && allCustomer[i].customerState == CustomerState.atCounter)
            {
                allCustomer[i].GoToCheckout(this);
            }
        }
        XPManager.Instance.GainXP(5);
    }
}

[Serializable]
public class CustomerPosition
{
    public Customer customer;
    public Transform point;
    public bool isOccupied;
}
