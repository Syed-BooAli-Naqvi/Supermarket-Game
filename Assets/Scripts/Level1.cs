using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject[] enable, disable;
    public GameObject footPrintToCar;

    public void OnEnable()
    {
        SupermarketLevelsManager.Instance.ChangeToPlayer();
        foreach (GameObject item in enable)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in disable)
        {
            item.SetActive(false);
        }

        StartCoroutine(CheckLevelStatus());
    }

    public IEnumerator CheckLevelStatus()
    {
        SimpleMessageHandler.Instance.ShowMessage("Its a new day, lets try to do everything you have planned.");
        yield return new WaitUntil(() => SimpleMessageHandler.Instance.GetComponent<CanvasGroup>().alpha == 0);
        footPrintToCar.SetActive(true);
        yield return new WaitForSeconds(2);
        SimpleMessageHandler.Instance.ShowMessage("Now follow these footprint and go to your car.");
    }
}
