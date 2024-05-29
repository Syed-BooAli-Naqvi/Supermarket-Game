using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFurnitureBuyer : MonoBehaviour
{
    public List<ShelfParentHandler> shelfHandlers, simpleShelfHandlers;

    private void Start()
    {
        
    }
    public void GetSimpleShelf()
    {
        if (PlayerPrefs.GetInt(SharedPref.CanBuySimpleShelf) > 0)
        {
            bool isAllOK = false;
            simpleShelfHandlers = shelfHandlers.FindAll(match => match.shelfType == ShelfType.simpleShelf);
            for (int i = 0; i < simpleShelfHandlers.Count; i++)
            {
                if (simpleShelfHandlers[i].canCheck)
                {
                    Debug.Log("Code Should Be Here");
                    simpleShelfHandlers[i].CheckBought();
                    if (!simpleShelfHandlers[i].isBought)
                    {
                        Debug.Log("Code Should Be Here");
                        isAllOK = true;
                        if (PlayerPrefs.GetFloat(SharedPref.Cash, 100) >= 0)
                        {
                            Debug.Log("Code Should Be Here");
                            PlayerPrefs.SetInt(SharedPref.CanBuySimpleShelf, PlayerPrefs.GetInt(SharedPref.CanBuySimpleShelf) - 1);
                            simpleShelfHandlers[i].Buy();
                            SimpleMessageHandler.Instance.ShowMessage("Congrats on your new shelf.");
                            PlayerPrefs.SetFloat(SharedPref.Cash, PlayerPrefs.GetFloat(SharedPref.Cash, 100) - 0);
                            isAllOK = false;
                            break;
                        }
                        else
                        {
                            Debug.Log("Code Should Be Here");
                            SimpleMessageHandler.Instance.ShowMessage("You don't have enough cash.");
                        }
                    }
                }
            }
            if (!isAllOK)
            {
                SimpleMessageHandler.Instance.ShowMessage("You have bought enough shelves for your current store level.");
            }
        }
        else
        {
            SimpleMessageHandler.Instance.ShowMessage("Upgrade Your Store Level to get new shelves.");
        }
    }

    public void GetVegShelf()
    {
        if (PlayerPrefs.GetInt(SharedPref.CanBuyFruitShelf) > 0)
        {
            bool isAllOK = false;
            simpleShelfHandlers = shelfHandlers.FindAll(match => match.shelfType == ShelfType.fruitAndVeg);
            for (int i = 0; i < simpleShelfHandlers.Count; i++)
            {
                if (simpleShelfHandlers[i].canCheck)
                {
                    Debug.Log("Code Should Be Here");
                    simpleShelfHandlers[i].CheckBought();
                    if (!simpleShelfHandlers[i].isBought)
                    {
                        Debug.Log("Code Should Be Here");
                        isAllOK = true;
                        if (PlayerPrefs.GetFloat(SharedPref.Cash, 100) >= 0)
                        {
                            Debug.Log("Code Should Be Here");
                            PlayerPrefs.SetInt(SharedPref.CanBuyFruitShelf, PlayerPrefs.GetInt(SharedPref.CanBuyFruitShelf) - 1);
                            simpleShelfHandlers[i].Buy();
                            SimpleMessageHandler.Instance.ShowMessage("Congrats on your new shelf.");
                            PlayerPrefs.SetFloat(SharedPref.Cash, PlayerPrefs.GetFloat(SharedPref.Cash, 100) - 0);
                            isAllOK = false;
                            break;
                        }
                        else
                        {
                            Debug.Log("Code Should Be Here");
                            SimpleMessageHandler.Instance.ShowMessage("You don't have enough cash.");
                        }
                    }
                }
            }
            if (!isAllOK)
            {
                SimpleMessageHandler.Instance.ShowMessage("You have bought enough shelves for your current store level.");
            }
        }
        else
        {
            SimpleMessageHandler.Instance.ShowMessage("Upgrade Your Store Level to get new shelves.");
        }
    }
}
