using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AllProducts;

public class ProductDetails : MonoBehaviour
{
    public Image itemImg;
    public TMP_Text productName, productQuantityInBox, placingPostion, unitPrice, amount, totalPrice;
    public Button plus, minus, addToCart;
    public ProductAmounts myProductAmount;
    public ProductsHandler productsHandler;

    public bool test;
    private void OnValidate()
    {
        if (test)
        {
            test = false;
            ProductAmounts a = FindAnyObjectByType<AllProducts>().allProducts.Find(a => a.myProductType == myProductAmount.myProductType);
            myProductAmount.myItemPrice = a.myItemPrice;
            myProductAmount.myPrice = a.myPrice;
            myProductAmount.deliveryTime = a.deliveryTime;
            myProductAmount.name = a.name;
            myProductAmount.marketPrice = a.marketPrice;
            myProductAmount.maxMarketPrice = a.maxMarketPrice;
            myProductAmount.light = a.light;

            itemImg.sprite = AllProduct.Find(match => match.myProductType == myProductAmount.myProductType).mySprite;


            totalPrice.text = "$" + myProductAmount.myPrice;
            unitPrice.text = "$" + myProductAmount.myItemPrice;
            productName.text = myProductAmount.name;
            productQuantityInBox.text = "12";
        }
    }
    public int myOpening;
    public GameObject lockObj;
    void OnEnable()
    {
        lockObj.SetActive(!(PlayerPrefs.GetInt(XPManager.LevelKey) >= myOpening));
        ResetAll();
    }

    public void ResetAll()
    {
        amount.text = "1";
        myProductAmount.totalAmount = 1;
        totalPrice.text = "$" + myProductAmount.myPrice;
        unitPrice.text = "$" + myProductAmount.myItemPrice;
        productName.text = myProductAmount.name;
    }

    public void Plus()
    {
        if (myProductAmount.totalAmount > 9)
            return;
        myProductAmount.totalAmount += 1;
        amount.text = myProductAmount.totalAmount.ToString();
        totalPrice.text = "$" + (myProductAmount.myPrice * myProductAmount.totalAmount);
    }
    public void Minus()
    {
        if (myProductAmount.totalAmount > 0)
        {
            myProductAmount.totalAmount -= 1;
            amount.text = myProductAmount.totalAmount.ToString();
            totalPrice.text = "$" + (myProductAmount.myPrice * myProductAmount.totalAmount);
        }
    }
    public void AddToCart()
    {
        if (myProductAmount.totalAmount > 0)
        {
            int deliveryNum = productsHandler.productAmounts.Count;
            productsHandler.productAmounts.Add(new ProductAmounts()
            {
                totalAmount = myProductAmount.totalAmount,
                myProductType = myProductAmount.myProductType,
                deliveryTime = myProductAmount.deliveryTime * myProductAmount.totalAmount,
                myPrice = myProductAmount.myPrice,
                myItemPrice = myProductAmount.myItemPrice,
                deliveryNum = deliveryNum,
                light = myProductAmount.light
            });
            productsHandler.CalculateTotalPrice(myProductAmount.myPrice * myProductAmount.totalAmount);
            productsHandler.CalculateTotalAmount(myProductAmount.totalAmount);
            ResetAll();
        }
    }
}
