using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;
using UnityEngine;
using static PlayerHandler;
using static UIHandler;
using static AllProducts;
public class ShelfItemPriceTagSetter : MonoBehaviour, IInteractable
{
    public ShelfHandler shelfHandler;
    public void Interact()
    {
        if (CurrentDeliveryBox == null)
        {
            InteractHandBtn.gameObject.SetActive(true);
            InteractHandBtn.onClick.AddListener(() =>
            {
                FCP.canRotate = false;
                FCP.canMove = false;
                ShelfItemPriceUI.SetMe(shelfHandler.shelf.currentItemPrice, shelfHandler);
                ShelfItemPriceUI.gameObject.SetActive(true);
                DisableInteractBtn();
            });
        }
    }

    public void NonInteract()
    {

    }
}
