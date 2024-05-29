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
        SetTutTxt("Go And buy supermarket from dealer.");
    }

    public void StartNameChangeTut()
    {
        Debug.Log("StartNameChangeTut");
        foreach (var item in DealerTuts)
            item.SetActive(false);
        SetTutTxt("Let's change your shop name.");
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

            SetTutTxt("Go and pick up store banner from inside the store.");
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

        SetTutTxt("Now place this banner outside the shop");
    }


    public void StarDoorOpenTut()
    {
        Debug.Log("StarDoorOpenTut");
        SetTutTxt("Open your supermarket door.");
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
        SetTutTxt("Welcome to your supermarket.\nNow go to your laptop.");
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
        SetTutTxt("Now you need to go outside and wait till delivery is completed.\nYou can speed up using lighting.");
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
        SetTutTxt("Now you need to go set these products in shelf.");
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
        SetTutTxt("You should keep your shop clean.\nGo outside and throw this box in dustbin.");
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
        SetTutTxt("You should keep in mind to set the price of products you place on shelf.\nLet's go and set prices.");
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
        SetTutTxt("Let's open your market.");
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
        SetTutTxt("Let's goto your checkout counter.");
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
