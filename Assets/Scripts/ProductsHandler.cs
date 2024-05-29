using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SharedPref;
using static UIHandler;

public class ProductsHandler : MonoBehaviour
{
    public TMP_Text totalCartPrice, totalProducts;
    public List<ProductAmounts> productAmounts;
    public float productTotalPrice;
    public int productTotalAmount;
    public PendingDelivery pendingDelivery;
    public Button bank,buy;

    // Start is called before the first frame update
    void Start()
    {
        ClearAll();
    }
    public void ClearAll()
    {
        buy.interactable = false;
        productTotalPrice = 0;
        productTotalAmount = 0;
        totalCartPrice.text = "$0.00";
        totalProducts.text = "0";
        productAmounts.Clear();
    }
    public void BuyAll()
    {
        if (productTotalPrice <= PlayerPrefs.GetFloat(Cash, 100))
        {
            PlayerPrefs.SetInt(LaptopTut, 1);
            PlayerPrefs.SetFloat(Cash, PlayerPrefs.GetFloat(Cash, 100) - productTotalPrice);
            for (int i = 0; i < productAmounts.Count; i++)
            {
                pendingDelivery.productAmounts.Add(productAmounts[i]);
            }
            pendingDelivery.Save();
            ClearAll();
        }
        else
        {
            bank.onClick.Invoke();
        }
        CFCG.alpha = 1;
    }
    public void CalculateTotalPrice(float Price)
    {
        buy.interactable = true;
        productTotalPrice += Price;
        totalCartPrice.text = "$" + (productTotalPrice < 1 ? productTotalPrice.ToString("#0.00") : productTotalPrice.ToString("#.00"));
    }
    public void CalculateTotalAmount(int Amount)
    {
        productTotalAmount += Amount;
        totalProducts.text = productTotalAmount.ToString();
    }
}
[System.Serializable]
public class ProductAmounts
{
    public string name;
    public ProductType myProductType;
    public int totalAmount;
    public float deliveryTime, myPrice, myItemPrice, marketPrice, maxMarketPrice;
    public bool isCompleted, isPlaced;
    public int deliveryNum;
    public Sprite mySprite;
    public int light;
}
[System.Serializable]
public enum ProductType
{
    Flour, SlicedBread, Oil, Pasta, Sugar, Cereal, Coffee, Milk, Cheese, Water, BlackTea, Eggs, ChickenMeat, BeefMeat, Penne, Fries, Candy, Rice, Banana, Cucumber, EggPlant, Apple
}
