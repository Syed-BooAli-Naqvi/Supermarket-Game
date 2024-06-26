using System.Collections;
using UnityEngine;

public class Level01 : MonoBehaviour
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
        SimpleMessageHandler.Instance.ShowMessage("Pick up your employees on your way.");
        yield return new WaitUntil(() => SimpleMessageHandler.Instance.GetComponent<CanvasGroup>().alpha == 0);
        yield return new WaitForSeconds(1);
        SimpleMessageHandler.Instance.ShowMessage("You need to pick 3 employees.");
        car.isKinematic = false;
    }
}
