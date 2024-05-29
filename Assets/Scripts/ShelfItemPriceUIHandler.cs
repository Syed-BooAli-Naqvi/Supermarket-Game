using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerHandler;
using static AllProducts;

public class ShelfItemPriceUIHandler : MonoBehaviour
{
    public ShelfHandler currentShelf;
    public TMP_Text cost, currentPrice, marketPrice, profit, lighting, productName;
    public Image itemImg;
    public Slider priceSlider;
    public ProductAmounts myProductAmount;
    public float currentProductMarketPrice;
    public int lights;
    public Button bank;

    void Start()
    {
        priceSlider.onValueChanged.AddListener(delegate
        {
            SetUIOnSlider(priceSlider.value);
        });
    }

    public void SetUIOnSlider(float val)
    {
        profit.text = "$" + (val - myProductAmount.myItemPrice).ToString("#00.00");
        currentPrice.text = "$" + val.ToString("#00.00");
        if (val >= currentProductMarketPrice + ((lights + 1) * 0.4f))
        {
            lights++;
            lighting.text = lights.ToString();
        }
        else if ((val > currentProductMarketPrice) && val < currentProductMarketPrice + (Mathf.Max(1, lights - 1) * 0.4f))
        {
            lights = Mathf.Max(0, lights - 1);
            lighting.text = lights.ToString();
        }
    }

    public void SetMe(float currentPrice, ShelfHandler selectedShelf)
    {
        selectedShelf.priceTag.GetComponent<HighlightPlus.HighlightEffect>().highlighted = false;
        selectedShelf.priceTag.GetComponent<HighlightPlus.HighlightEffect>().enabled = false;
        ProductAmounts product = selectedShelf.shelf.product;
        currentShelf = selectedShelf;
        lights = 0;
        lighting.text = "0";
        if (currentPrice >= product.marketPrice)
            currentProductMarketPrice = currentPrice;
        else
            currentProductMarketPrice = product.marketPrice;
        myProductAmount = product;
        cost.text = "$" + currentPrice.ToString("#00.00");
        this.currentPrice.text = "$" + currentPrice.ToString("#00.00");
        marketPrice.text = "$" + product.marketPrice.ToString("#00.00");
        //itemImg.sprite = AllProduct.Find(match => match.myProductType == product.myProductType).mySprite;
        productName.text = product.name;
        priceSlider.maxValue = product.maxMarketPrice;
        priceSlider.minValue = product.myItemPrice;
        priceSlider.value = currentPrice;
        profit.text = "$" + (priceSlider.value- product.myItemPrice).ToString("#00.00");
        SetUIOnSlider(priceSlider.value);
    }

    public void Save()
    {
        if (lights <= PlayerPrefs.GetInt(SharedPref.Light, 50))
        {
            currentShelf.priceTag.GetComponent<HighlightPlus.HighlightEffect>().enabled = false;
            PlayerPrefs.SetInt(SharedPref.Light, PlayerPrefs.GetInt(SharedPref.Light, 50) - lights);
            currentShelf.shelf.currentItemPrice = priceSlider.value;
            for (int i = 0; i < currentShelf.shelf.objectT.Count; i++)
            {
                currentShelf.shelf.objectT[i].GetComponent<Product>().myCurrentPrice = currentShelf.shelf.currentItemPrice;
            }
            currentShelf.Save();
            FCP.canRotate = true;
            FCP.canMove = true;
            PlayerPrefs.SetInt(SharedPref.ShelfPriceSetTut, 1);
            if (PlayerPrefs.GetInt(SharedPref.ShopOpenCloseTut)!=1)
            {
                TutorialManager.Instance.ShopOpenCloseTutorial();
            }
        }
        else
        {
            gameObject.SetActive(false);
            bank.onClick.Invoke();
            FCP.canRotate = true;
            FCP.canMove = true;
        }
    }
}
