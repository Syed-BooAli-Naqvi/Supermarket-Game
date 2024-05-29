using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;
using UnityEngine;
using static PlayerHandler;
using static UIHandler;
using static AllProducts;
using TMPro;

public class ShelfHandler : MonoBehaviour, IInteractable
{
    public Shelf shelf;
    public PendingDelivery pendingDelivery;
    public TMP_Text productName, productPrice;
    public GameObject priceTag;
    public ShelfType shelfType;

    private void Start()
    {
        //Debug.Log("Shelf Cntains its key" + PlayerPrefs.HasKey(gameObject.name + SharedPref.ShelfData));
        if (PlayerPrefs.HasKey(gameObject.name + SharedPref.ShelfData))
        {
            shelf = PlayerPrefsExtra.GetObject(gameObject.name + SharedPref.ShelfData, new Shelf());
            Product prod = AllProductObjects.Find(match => match.product.myProductType == shelf.product.myProductType);
            shelf.objectT.Clear();
            for (int i = 0; i < shelf.currentItemIndex; i++)
            {
                Product product = Instantiate(prod, transform.GetChild(i));
                shelf.objectT.Add(product.transform);
                product.myCurrentPrice = shelf.currentItemPrice;
                ChangeLayerRecursive(product.transform, 0);
            }
            shelf.objectT.Reverse();
            priceTag.SetActive(true);
            productName.text = shelf.product.name;
            productPrice.text = "$" + shelf.currentItemPrice.ToString("#.00");
            shelf.currentTakenIndex = 0;
            priceTag.SetActive(shelf.isOccupied);
            CheckForHighlight();
        }
    }

    public IEnumerator AddAllProductsToShelf(DeliveryBox deliveryBox)
    {
        if (shelf != null)
        {
            DropBoxBtn.gameObject.SetActive(false);
            shelf.isOccupied = true;
            GetComponent<Collider>().enabled = false;
            shelf.currentItemPrice = deliveryBox.productAmount.myItemPrice;
            shelf.product.myProductType = deliveryBox.productAmount.myProductType;
            shelf.product.myItemPrice = deliveryBox.productAmount.myItemPrice;

            shelf.product.myPrice = deliveryBox.productAmount.myPrice;
            shelf.product.deliveryTime = deliveryBox.productAmount.deliveryTime;
            shelf.product.name = deliveryBox.productAmount.name;
            shelf.product.marketPrice = deliveryBox.productAmount.marketPrice;
            shelf.product.maxMarketPrice = deliveryBox.productAmount.maxMarketPrice;
            priceTag.SetActive(true);
            productName.text = shelf.product.name;
            productPrice.text = "$" + shelf.currentItemPrice.ToString("#.00");
            bool isComplete = false;
            for (int i = 0; i < deliveryBox.myObjects.Length; i++)
            {
                shelf.objectT.Add(deliveryBox.myObjects[i]);
                DOTween.To(() => deliveryBox.myObjects[i].position, x => deliveryBox.myObjects[i].position = x, transform.GetChild(i).position, 0.2f).OnComplete(() =>
                {
                    ChangeLayerRecursive(deliveryBox.myObjects[i], 0);
                    deliveryBox.myObjects[i].SetParent(transform.GetChild(i));
                    deliveryBox.myObjects[i].GetComponent<Product>().myCurrentPrice = shelf.currentItemPrice;
                    shelf.currentItemIndex++;
                    isComplete = true;
                });
                yield return new WaitUntil(() => isComplete);
                isComplete = false;
            }
            shelf.objectT.Reverse();
            pendingDelivery.productAmounts[deliveryBox.productAmount.deliveryNum].isPlaced = true;
            CurrentDeliveryBox.productAmount.isPlaced = true;
            if (pendingDelivery.timerState == PendingDelivery.TimerState.Ended || pendingDelivery.timerState == PendingDelivery.TimerState.none)
                pendingDelivery.Save();

            PlayerPrefs.SetInt(SharedPref.ShelfSettingTutorial, 1);
            Save();
            if (PlayerPrefs.GetInt(SharedPref.DustbinTutorial) != 1)
            {
                TutorialManager.Instance.StartDustbinTutorial();
            }
            if (PlayerPrefsExtra.GetBool(SharedPref.ShopOpenStatus))
                GameManager.Instance.Start();

            CFCG.alpha = 1;
        }
        FCP.canRotate = true;
        FCP.canMove = true;
        DropBoxBtn.gameObject.SetActive(PlayerPrefs.GetInt(SharedPref.DustbinTutorial) == 1);
    }

    public void CheckForHighlight()
    {
        if (PlayerPrefs.GetInt(SharedPref.ShelfPriceSetTut) != 1)
        {
            if (priceTag.activeSelf)
            {
                if (priceTag.GetComponent<HighlightPlus.HighlightEffect>())
                {
                    priceTag.GetComponent<HighlightPlus.HighlightEffect>().enabled = true;
                }
            }
        }
    }

    void ChangeLayerRecursive(Transform parent, int newLayerName)
    {

        // Change the layer of the parent
        parent.gameObject.layer = newLayerName;

        // Iterate through each child
        foreach (Transform child in parent)
        {
            // Change the layer of the child
            child.gameObject.layer = newLayerName;

            // Recursive call to process the child's children
            ChangeLayerRecursive(child, newLayerName);
        }
    }

    public void Interact()
    {
        if (CurrentDeliveryBox != null && !CurrentDeliveryBox.productAmount.isPlaced && !shelf.isOccupied && GetComponentInParent<ShelfParentHandler>() && GetComponentInParent<ShelfParentHandler>().isBought)
        {
            Debug.Log("Added Listener");
            InteractHandBtn.gameObject.SetActive(true);
            InteractHandBtn.onClick.AddListener(() =>
            {
                CFCG.alpha = 0;
                if (shelfType != CurrentDeliveryBox.shelfType)
                {
                    if (CurrentDeliveryBox.shelfType == ShelfType.simpleShelf)
                    {
                        SimpleMessageHandler.Instance.ShowMessage("You can place this item only on simple shelf.");
                    }
                    else if(CurrentDeliveryBox.shelfType == ShelfType.fruitAndVeg)
                    {
                        SimpleMessageHandler.Instance.ShowMessage("You can place this item only on fruit shelf.");
                    }
                    return;
                }
                FCP.canRotate = false;
                FCP.canMove = false;
                StartCoroutine(AddAllProductsToShelf(CurrentDeliveryBox));
                DisableInteractBtn();
            });
        }
    }


    public void Save()
    {
        for (int i = 0; i < shelf.objectT.Count; i++)
        {
            if (shelf.objectT[i] == null)
            {
                shelf.objectT.RemoveAt(i);
                i--;
            }
        }
        if (shelf.currentItemIndex == 0)
        {
            shelf.isOccupied = false;
        }
        PlayerPrefsExtra.SetObject(gameObject.name + SharedPref.ShelfData, shelf);
        shelf = PlayerPrefsExtra.GetObject(gameObject.name + SharedPref.ShelfData, new Shelf());
        productName.text = shelf.product.name;
        productPrice.text = "$" + shelf.currentItemPrice.ToString("#.00");
        priceTag.SetActive(shelf.currentItemIndex != 0);
        GetComponent<Collider>().enabled = shelf.currentItemIndex != 0;
    }


    public void NonInteract()
    {
    }

}
[Serializable]
public class Shelf
{
    public bool isOccupied;
    public int currentItemIndex;
    public int currentTakenIndex;
    public List<Transform> objectT;
    public BoxCollider shelfC;
    public ProductAmounts product;
    public float currentItemPrice;
}
[Serializable]
public enum ShelfType
{
    simpleShelf, fruitAndVeg
}
