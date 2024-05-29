using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using System.Threading.Tasks;
using DG.Tweening;
using static GameManager;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    public CustomerState customerState;
    public int maxDemand;
    public List<CustomerDemand> demands;
    public List<ShelfHandler> shelfHandlers;
    public Transform pos;
    public NavMeshAgent agent;
    public Animator agentAnimator;
    public List<CustomerTransforms> CTS;
    public Transform bagT, firstT;
    public CustomerPosition myCounterPositions;
    public int howManyDemands;
    public List<Transform> objT;
    public PaymentState paymentState;
    public Vector3 initialPos;
    public List<Avatar> avatars;
    public int random;
    public Transform look;
    public void Start()
    {
        initialPos = transform.position;
    }
    public async Task ChangePlayer()
    {
        Debug.Log("OnEnable");
        objT.Clear();
        await Task.Delay(10);
        foreach (var item in CTS)
        {
            item.gameObject.SetActive(false);
        }
        await Task.Delay(10);
        random = Random.Range(0, CTS.Count);
        CTS[random].transform.SetAsFirstSibling();
        CTS[random].gameObject.SetActive(true);
        agentAnimator.avatar = avatars[random];
        bagT = CTS[random].bagT;
        firstT = CTS[random].firstT;
    }
    public ShelfParentHandler mySPH;
    public async Task SetDemands(ShelfParentHandler parentHandler,CheckoutHandler checkoutHandler)
    {
        mySPH = parentHandler;
        shelfHandlers = parentHandler.shelfHandlers.FindAll(shelfH => shelfH.shelf.isOccupied && shelfH.shelf.currentItemIndex > 0 && shelfH.shelf.currentItemIndex != shelfH.shelf.currentTakenIndex);// && shelfH.shelf.currentTakenIndex < shelfH.shelf.currentItemIndex);
        if (shelfHandlers.Count > 0)
        {
            //maxDemand = 7;
            maxDemand = Random.Range(1, 6);
            customerState = CustomerState.atShelf;
            demands.Clear();

            shelfHandlers = shelfHandlers.OrderBy(x => Random.value).ToList();
            howManyDemands = Random.Range(1, (shelfHandlers.Count + 1) / 2);
            for (int i = 0; i < howManyDemands; i++)
            {
                if (i < shelfHandlers.Count && maxDemand > 0)
                {
                    if (maxDemand > (shelfHandlers[i].shelf.currentItemIndex - shelfHandlers[i].shelf.currentTakenIndex))
                    {
                        if (shelfHandlers[i].shelf.currentItemIndex - shelfHandlers[i].shelf.currentTakenIndex > 0)
                        {
                            maxDemand = shelfHandlers[i].shelf.currentItemIndex - shelfHandlers[i].shelf.currentTakenIndex;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    demands.Add(new CustomerDemand()
                    {
                        type = shelfHandlers[i].shelf.product.myProductType,
                        amount = Random.Range(1, maxDemand)
                    });
                    shelfHandlers[i].shelf.currentTakenIndex += demands[^1].amount;
                    maxDemand -= demands[^1].amount;
                }
            }
            agent.SetDestination(parentHandler.point.position);
            agentAnimator.Play("Walk");
            pos = parentHandler.point;
            await AsyncWaiter.WaitUntilAsync(() => agent.gameObject.activeInHierarchy && agent.remainingDistance < 0.6f && !agent.pathPending);
            agentAnimator.Play("Idle");
            agent.isStopped = true;
            await Task.Delay(1500);
            bool isComplete = false;
            for (int i = 0; i < demands.Count; i++)
            {
                for (int j = 0; j < demands[i].amount; j++)
                {
                    isComplete = false;
                    Product pro = shelfHandlers[i].shelf.objectT.Find(match => !match.GetComponent<Product>().isPicked).GetComponent<Product>();
                    pro.isPicked = true;
                    Transform proT = pro.transform;
                    proT.SetParent(bagT);
                    objT.Add(proT);

                    Vector3 scale = proT.localScale;
                    DOTween.To(() => proT.position, x => proT.position = x, firstT.position, 0.8f).OnComplete(() =>
                    {
                        DOTween.To(() => proT.localScale, x => proT.localScale = x, new Vector3(0, 0, 0), 0.2f).OnComplete(() =>
                        {
                            isComplete = true;
                        });
                    });
                    await AsyncWaiter.WaitUntilAsync(() => isComplete);
                    pro.gameObject.SetActive(false);
                    proT.localScale = scale;
                }
            }
            customerState = CustomerState.atCounter;
            GoToCheckout(checkoutHandler);

        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public async void GoToCheckout(CheckoutHandler checkoutHandler)
    {
        CustomerPosition index = checkoutHandler.customerPositions.FindLast(match => !match.isOccupied);
        myCounterPositions = index;
        myCounterPositions.isOccupied = true;
        myCounterPositions.customer = this;
        agent.isStopped = false;
        agent.SetDestination(myCounterPositions.point.position);
        agentAnimator.Play("Walk");
        pos = myCounterPositions.point;
        await AsyncWaiter.WaitUntilAsync(() => agent.gameObject.activeInHierarchy && agent.remainingDistance < 0.6f && !agent.pathPending);
        agentAnimator.Play("Idle");
        agent.isStopped = true;
        transform.LookAt(look);
        paymentState = Random.Range(0, 2) == 0 ? PaymentState.cash : PaymentState.card;
        if (!checkoutHandler.isStarted)
        {
            await AsyncWaiter.WaitUntilAsync(() => checkoutHandler.canStart);
            checkoutHandler.StartCheckout(paymentState);//, index);
        }
    }
    public async void CompleteCheckout()
    {
        myCounterPositions.isOccupied = false;
        myCounterPositions.customer = null;
        customerState = CustomerState.goingOut;

        agent.isStopped = false;
        agent.SetDestination(initialPos);
        agentAnimator.Play("Walk");
        pos = myCounterPositions.point;
        await AsyncWaiter.WaitUntilAsync(() => agent.gameObject.activeInHierarchy && agent.remainingDistance < 0.6f && !agent.pathPending);
        agentAnimator.Play("Idle");
        agent.isStopped = true;
        customerState = CustomerState.none;
        GameManager.Instance.Star();
        gameObject.SetActive(false);
    }
}

public class AsyncWaiter
{
    // Generic function that waits until a condition is met
    public static async Task WaitUntilAsync(Func<bool> condition)
    {
        while (!condition())
        {
            // Wait asynchronously for a short period before checking the condition again
            await Task.Delay(100); // Adjust the delay as needed
        }
    }
}
[Serializable]
public class CustomerDemand
{
    public ProductType type;
    public int amount;
}
[Serializable]
public enum CustomerState
{
    none,atShelf,atCounter,goingOut
}
[Serializable]
public enum PaymentState
{
    cash, card
}
