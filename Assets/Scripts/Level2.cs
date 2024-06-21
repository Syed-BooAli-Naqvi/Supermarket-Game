using System.Collections;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    public GameObject[] enable, disable;
    public Rigidbody car;

    public void OnEnable()
    {
        SupermarketLevelsManager.Instance.ChangeToRcc();
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
        car.isKinematic = true;
        SimpleMessageHandler.Instance.ShowMessage("You need to get these boxes to your store.");
        yield return new WaitUntil(() => SimpleMessageHandler.Instance.GetComponent<CanvasGroup>().alpha == 0);
        yield return new WaitForSeconds(1);
        SimpleMessageHandler.Instance.ShowMessage("Go to store and drop these boxes.");
        car.isKinematic = false;
    }
}