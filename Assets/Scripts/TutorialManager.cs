using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using static SharedPref;

public class TutorialManager : Singleton<TutorialManager>
{
    public GameObject moveTut,rotateTut,tutObj;
    public FirstPersonController controller;
    public TMP_Text tutTxt;

    IEnumerator Start()
    {
        if (PlayerPrefs.GetInt(MovingTutorial) != 1)
        {
            moveTut.SetActive(true);
            controller.canMove = true;
            controller.canRotate = false;
        }
        if (PlayerPrefs.GetInt(RotatingTutorial) != 1)
        {
            yield return new WaitUntil(() => rotateTut.activeSelf);
            yield return new WaitUntil(() => !rotateTut.activeSelf);
        }
        if (PlayerPrefs.GetInt(DealerTut)!=1)
        {
            StartDealerTut();
        }
        else if (PlayerPrefs.GetInt(NameChangeTut) != 1)
        {
            StartNameChangeTut();
        }
        else if (PlayerPrefs.GetInt(BannerPickTut) != 1)
        {
            StartBannerPickTut();
        }
        else if (PlayerPrefs.GetInt(BannerPlaceTut) != 1)
        {
            StartBannerPickTut();
        }
        else if (PlayerPrefs.GetInt(LaptopTut) != 1)
        {
            StartComputerTutorial();
        }
        else if (PlayerPrefs.GetInt(DeliveryTutorial)!=1)
        {
            StartDeliveryTutorial();
        }
        else if (PlayerPrefs.GetInt(ShelfSettingTutorial) != 1)
        {
            StartShelfSettingTutorial();
        }
    }
    public void ShowRotationTutorial()
    {
        if (PlayerPrefs.GetInt(RotatingTutorial) != 1)
        {
            rotateTut.SetActive(true);
            controller.canRotate = true;
        }
    }

    public GameObject[] DealerTuts, ComputerTut, DeliveryTut, ShelfTut, DustbinTut, ShelfPriceTut, ShopOCTut, CheckoutCounterTut, BannerPickTuts, BannerPlaceTuts;

    public void StartDealerTut()
    {
        Debug.Log("StartDelaerTut");
        if (PlayerPrefs.GetInt(SharedPref.DealerTut) != 1)
            foreach (var item in DealerTuts)
            item.SetActive(true);
        SetTutTxt("Go and buy the supermarket from the dealer.");
    }

    public void StartNameChangeTut()
    {
        Debug.Log("StartNameChangeTut");
        foreach (var item in DealerTuts)
            item.SetActive(false);
        SetTutTxt("Decide on a new name for your store.");
    }

    public void StartBannerPickTut()
    {
        if (PlayerPrefs.GetInt(BannerPickTut) != 1)
        {
            Debug.Log("StartBannerPickTut");
            foreach (var item in DealerTuts)
                item.SetActive(false);

            foreach (var item in BannerPickTuts)
                item.SetActive(true);

            SetTutTxt("Collect the store banner from inside the shop.");
        }
    }


    public void StartBannerPlaceTut()
    {
        Debug.Log("StartBannerPlaceTut");
        foreach (var item in DealerTuts)
            item.SetActive(false);

        foreach (var item in BannerPickTuts)
            item.SetActive(false);

        foreach (var item in BannerPlaceTuts)
            item.SetActive(true);

        SetTutTxt("Place the banner outside the shop where it's visible.");
    }


    public void StarDoorOpenTut()
    {
        Debug.Log("StarDoorOpenTut");
        SetTutTxt("Open the supermarket door.");
        foreach (var item in BannerPickTuts)
            item.SetActive(false);
        foreach (var item in BannerPlaceTuts)
            item.SetActive(false);
    }

    public void StartComputerTutorial()
    {
        Debug.Log("StartComputerTutorial");
        foreach (var item in DealerTuts)
            item.SetActive(false);
        foreach (var item in BannerPickTuts)
            item.SetActive(false);
        if (PlayerPrefs.GetInt(SharedPref.LaptopTut) != 1)
            foreach (var item in ComputerTut)
            item.SetActive(true);
        foreach (var item in BannerPlaceTuts)
            item.SetActive(false);
        SetTutTxt("Access your laptop for further tasks.");
    }

    public void StartDeliveryTutorial()
    {
        foreach (var item in DealerTuts)
            item.SetActive(false);
        foreach (var item in BannerPickTuts)
            item.SetActive(false);
        foreach (var item in BannerPlaceTuts)
            item.SetActive(false);
        foreach (var item in ComputerTut)
        {
            item.SetActive(false);
        }
        Debug.Log("StartDeliveryTutorial");
        if (PlayerPrefs.GetInt(SharedPref.DealerTut) != 1)
            foreach (var item in DeliveryTut)
            item.SetActive(true);
        SetTutTxt("Go outside and wait until deliveries are completed. Use lighting to speed up this process if possible.");
    }

    public void StartShelfSettingTutorial()
    {
        Debug.Log("StartShelfSettingTutorial");
        foreach (var item in DealerTuts)
            item.SetActive(false);
        foreach (var item in BannerPlaceTuts)
            item.SetActive(false);
        foreach (var item in BannerPickTuts)
            item.SetActive(false);
        foreach (var item in ComputerTut)
            item.SetActive(false);
        foreach (var item in DeliveryTut)
            item.SetActive(false);
        if (PlayerPrefs.GetInt(SharedPref.ShelfSettingTutorial) != 1)
            foreach (var item in ShelfTut)
            item.SetActive(true);
        SetTutTxt("Arrange the delivered products on the shelves.");
    }


    public void StartDustbinTutorial()
    {
        Debug.Log("StartDustbinTutorial");
        foreach (var item in DealerTuts)
            item.SetActive(false);
        foreach (var item in BannerPlaceTuts)
            item.SetActive(false);
        foreach (var item in BannerPickTuts)
            item.SetActive(false);
        foreach (var item in ComputerTut)
            item.SetActive(false);
        foreach (var item in DeliveryTut)
            item.SetActive(false);
        foreach (var item in ShelfTut)
            item.SetActive(false);
        if (PlayerPrefs.GetInt(SharedPref.DustbinTutorial) != 1)
            foreach (var item in DustbinTut)
            item.SetActive(true);
        SetTutTxt("Ensure the shop is kept clean throughout.\nTake any packaging materials outside and throw them in the dustbin.");
    }


    public void StartPriceSettingTutorial()
    {
        Debug.Log("StartPriceSettingTutorial");
        foreach (var item in ComputerTut)
            item.SetActive(false);
        foreach (var item in BannerPlaceTuts)
            item.SetActive(false);
        foreach (var item in BannerPickTuts)
            item.SetActive(false);
        foreach (var item in DealerTuts)
            item.SetActive(false);
        foreach (var item in DeliveryTut)
            item.SetActive(false);
        foreach (var item in ShelfTut)
            item.SetActive(false);
        foreach (var item in DustbinTut)
            item.SetActive(false);
        if (PlayerPrefs.GetInt(SharedPref.ShelfPriceSetTut) != 1)
            foreach (var item in ShelfPriceTut)
            item.SetActive(true);
        SetTutTxt("Remember to price the products on the shelves.");
    }


    public void ShopOpenCloseTutorial()
    {
        Debug.Log("ShopOpenCloseTutorial");
        foreach (var item in DealerTuts)
            item.SetActive(false);
        foreach (var item in BannerPlaceTuts)
            item.SetActive(false);
        foreach (var item in BannerPickTuts)
            item.SetActive(false);
        foreach (var item in ComputerTut)
            item.SetActive(false);
        foreach (var item in DeliveryTut)
            item.SetActive(false);
        foreach (var item in ShelfTut)
            item.SetActive(false);
        foreach (var item in DustbinTut)
            item.SetActive(false);
        foreach (var item in ShelfPriceTut)
            item.SetActive(false);
        if (PlayerPrefs.GetInt(SharedPref.ShopOpenCloseTut) != 1)
            foreach (var item in ShopOCTut)
            item.SetActive(true);
        SetTutTxt("Open the market to customers.");
    }


    public void StartCounterTutorial()
    {
        Debug.Log("StartCounterTutorial");
        foreach (var item in DealerTuts)
            item.SetActive(false);
        foreach (var item in BannerPlaceTuts)
            item.SetActive(false);
        foreach (var item in BannerPickTuts)
            item.SetActive(false);
        foreach (var item in ComputerTut)
            item.SetActive(false);
        foreach (var item in DeliveryTut)
            item.SetActive(false);
        foreach (var item in ShelfTut)
            item.SetActive(false);
        foreach (var item in DustbinTut)
            item.SetActive(false);
        foreach (var item in ShelfPriceTut)
            item.SetActive(false);
        foreach (var item in ShopOCTut)
            item.SetActive(false);
        if (PlayerPrefs.GetInt(SharedPref.CounterTut) != 1)
            foreach (var item in CheckoutCounterTut)
            item.SetActive(true);
        SetTutTxt("Go to your checkout counter to assist customers with their purchases.");
    }

    public void SetTutTxt(string tut)
    {
        tutObj.SetActive(false);
        Invoke(nameof(Check), 1.5f);
        tutTxt.text = tut;
    }
    public void Check()
    {
        tutObj.SetActive(true);
    }

}
