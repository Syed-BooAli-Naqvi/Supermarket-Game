using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerHandler;
using static UIHandler;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public CheckoutHandler checkoutHandler;
    public List<ShelfParentHandler> shelfParentHandlers;
    public GameObject openSign, closeSign, leaveCheckoutBtn, greenBanner, insideBanner, realBanner;
    public List<Customer> customer;
    public static List<Customer> allCustomer { get { return Instance.customer; } }
    public GameObject dealer,dooor;

    public void Star()
    {
        StartCoroutine(Start());
    }
    public IEnumerator Start()
    {
        dooor.SetActive(PlayerPrefs.GetInt("SimpleDoor") != 1);
        dealer.SetActive(PlayerPrefs.GetInt(SharedPref.DealerComplete) != 1);
        yield return new WaitForSeconds(1);
        if (PlayerPrefs.HasKey(SharedPref.ShopOpenStatus))
        {
            OpenCloseShop(PlayerPrefsExtra.GetBool(SharedPref.ShopOpenStatus));
        }
        else
        {
            OpenCloseShop(false);
        }
        greenBanner.SetActive(PlayerPrefs.GetInt(SharedPref.BannerComplete) != 1);
        insideBanner.SetActive(PlayerPrefs.GetInt(SharedPref.BannerComplete) != 1);
        realBanner.SetActive(PlayerPrefs.GetInt(SharedPref.BannerComplete) == 1);
    }
    public void SimpleDoor()
    {
        PlayerPrefs.SetInt("SimpleDoor", 1);
    }

    public void OpenCloseShop(bool open)
    {
        openSign.SetActive(open);
        closeSign.SetActive(!open);
        PlayerPrefsExtra.SetBool(SharedPref.ShopOpenStatus, open);
        if (open)
        {
            PlayerPrefs.SetInt(SharedPref.ShopOpenCloseTut, 1);
            if (PlayerPrefs.GetInt(SharedPref.CounterTut) != 1)
            {
                TutorialManager.Instance.StartCounterTutorial();
            }
            StartCustomerBehaviour();
        }
    }

    public async void StartCustomerBehaviour()
    {
        for (int i = 0; i < customer.Count; i++)
        {
            if (customer[i].customerState == CustomerState.none)
            {
                await AsyncWaiter.WaitUntilAsync(() => PlayerPrefsExtra.GetBool(SharedPref.ShopOpenStatus));
                customer[i].gameObject.SetActive(true);
                await customer[i].ChangePlayer();
                List<ShelfParentHandler> shelfParentHandler = shelfParentHandlers.FindAll(match => match.isBought);
                ShelfParentHandler sph = null;
                Shuffle(shelfParentHandler);
                for (int j = 0; j < shelfParentHandler.Count; j++)
                {
                    for (int k = 0; k < shelfParentHandler[j].shelfHandlers.Count; k++)
                    {
                        if(shelfParentHandler[j].shelfHandlers[k].shelf.isOccupied && shelfParentHandler[j].shelfHandlers[k].shelf.currentItemIndex > 0 && shelfParentHandler[j].shelfHandlers[k].shelf.currentItemIndex != shelfParentHandler[j].shelfHandlers[k].shelf.currentTakenIndex)
                        {
                            sph = shelfParentHandler[j];
                        }
                    }
                }
                if (sph)
                    await customer[i].SetDemands(sph, checkoutHandler);
                else
                    customer[i].gameObject.SetActive(false);
            }
        }
    }


    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void DropCurrentBox()
    {
        if (CurrentDeliveryBox != null)
        {
            CurrentDeliveryBox.Drop();
            PlayerHandler.Instance.currentDeliveryBox = null;
            DropBoxBtn.gameObject.SetActive(false);
            DisposeBoxBtn.gameObject.SetActive(false);
        }
    }

    public void DisposeCurrentBox(Transform bin)
    {
        if (CurrentDeliveryBox != null && CurrentDeliveryBox.productAmount.isPlaced)
        {
            FCP.canRotate = false;
            FCP.canMove = false;
            CurrentDeliveryBox.transform.SetParent(bin);

            DOTween.To(() => CurrentDeliveryBox.transform.localScale, x => CurrentDeliveryBox.transform.localScale = x, Vector3.zero, 0.5f);
            DOTween.To(() => CurrentDeliveryBox.transform.position, x => CurrentDeliveryBox.transform.position = x, bin.position, 0.5f).OnComplete(() =>
            {
                FCP.canRotate = true;
                FCP.canMove = true;
                CurrentDeliveryBox.gameObject.SetActive(false);
                PlayerHandler.Instance.currentDeliveryBox = null;
            });
            DisableInteractBtn();
            DropBoxBtn.gameObject.SetActive(false);
            DisposeBoxBtn.gameObject.SetActive(false);
            PlayerPrefs.SetInt(SharedPref.DustbinTutorial, 1);
            if (PlayerPrefs.GetInt(SharedPref.ShelfPriceSetTut)!=1)
            {
                TutorialManager.Instance.StartPriceSettingTutorial();
            }
            for (int i = 0; i < shelfParentHandlers.Count; i++)
            {
                for (int j = 0; j < shelfParentHandlers[i].shelfHandlers.Count; j++)
                {
                    shelfParentHandlers[i].shelfHandlers[j].CheckForHighlight();
                }
            }
        }
    }

    public void CanStartChecking(bool check)
    {
        BoxCam.SetActive(!check);
        PlayerPrefs.SetInt(SharedPref.CounterTut, 1);
        foreach (var item in TutorialManager.Instance.CheckoutCounterTut)
            item.SetActive(false);
        checkoutHandler.CanPlaceAtCheckout(check);
        leaveCheckoutBtn.SetActive(check);

        FCP.canRotate = !check;
        FCP.canMove = !check;
    }
    public void SetDealerStatus()
    {
        PlayerPrefs.SetInt(SharedPref.DealerComplete, 1);
        dealer.SetActive(PlayerPrefs.GetInt(SharedPref.DealerComplete) == 1);
        PlayerPrefs.SetInt(SharedPref.DealerTut, 1);
        TutorialManager.Instance.StartNameChangeTut();
    }
}
