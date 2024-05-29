using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static PlayerHandler;
using static UIHandler;

public class DeliveryBox : MonoBehaviour, IInteractable
{
    public ShelfType shelfType;
    public bool canOpenClose = true;
    public int myItemsCount;
    public ProductAmounts productAmount;
    public Transform[] myItems, myObjects;
    public bool test;
    public GameObject closed, opened;
    public HighlightPlus.HighlightEffect highlight;

    private void OnValidate()
    {
        if (test)
        {
            test = false;
            ProductAmounts a = FindObjectOfType<AllProducts>().allProducts.Find(a => a.myProductType == productAmount.myProductType);

            productAmount.myItemPrice = a.myItemPrice;
            productAmount.myPrice = a.myPrice;
            productAmount.deliveryTime = a.deliveryTime;
            productAmount.name = a.name;
            productAmount.marketPrice = a.marketPrice;
            productAmount.maxMarketPrice = a.maxMarketPrice;
        }
    }

    void Start()
    {
        ChangeLayerRecursive(this.transform, 0);
        if (shelfType == ShelfType.simpleShelf)
            foreach (var item in myObjects)
            {
                item.gameObject.SetActive(false);
            }
        //yield return new WaitForSeconds(2);
        //yield return new WaitUntil(() => GetComponent<Rigidbody>().velocity.magnitude < 0.1f);
        //GetComponent<Rigidbody>().isKinematic = true;
        highlight.enabled = PlayerPrefs.GetInt(SharedPref.DeliveryTutorial) != 1;
    }

    public void Interact()
    {
        if (CurrentDeliveryBox == null)
        {
            Debug.Log("Added Listener");
            InteractHandBtn.gameObject.SetActive(true);
            InteractHandBtn.onClick.AddListener(() =>
            {

                CFCG.alpha = 0;
                GetComponent<Rigidbody>().isKinematic = true;
                if (PlayerPrefs.GetInt(SharedPref.DeliveryTutorial)!=1)
                {
                    TutorialManager.Instance.StartShelfSettingTutorial();
                }
                PlayerPrefs.SetInt(SharedPref.DeliveryTutorial, 1);
                FCP.canRotate = false;
                FCP.canMove = false;
                transform.SetParent(BoxPosT);
                PlayerHandler.Instance.currentDeliveryBox = this;
                ChangeLayerRecursive(this.transform, 6);
                DOTween.To(() => transform.eulerAngles, x => transform.eulerAngles = x, BoxPosT.eulerAngles, 0.2f);
                DOTween.To(() => transform.position, x => transform.position = x, BoxPosT.position, 0.5f).OnComplete(() =>
                {
                    if (canOpenClose)
                    {
                        if (shelfType == ShelfType.simpleShelf)
                            foreach (var item in myObjects)
                            {
                                item.gameObject.SetActive(true);
                            }
                        opened.SetActive(true);
                        closed.SetActive(false);
                    }
                    FCP.canRotate = true;
                    FCP.canMove = true;
                    DropBoxBtn.gameObject.SetActive(PlayerPrefs.GetInt(SharedPref.DustbinTutorial) == 1);
                });
                CFCG.alpha = 1;
                DisableInteractBtn();
            });
        }
    }
    public void Drop()
    {
        if (!productAmount.isPlaced)
            if (shelfType == ShelfType.simpleShelf)
                foreach (var item in myObjects)
                {
                    item.gameObject.SetActive(false);
                }
        PlayerHandler.Instance.currentDeliveryBox = null;
        transform.SetParent(null);
        if (canOpenClose)
        {
            opened.SetActive(false);
            closed.SetActive(true);
        }
        DisableInteractBtn();
        GetComponent<Rigidbody>().isKinematic = false;
        ChangeLayerRecursive(this.transform, 0);
        //Start();
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

    public void NonInteract()
    {
    }
}
