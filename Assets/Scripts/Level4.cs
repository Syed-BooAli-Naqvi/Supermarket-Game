using UnityEngine;

public class Level4 : MonoBehaviour
{
    public GameObject[] enable, disable;
    public Transform initPos;

    public void OnEnable()
    {
        SupermarketLevelsManager.Instance.ChangeToPlayer();
        PlayerControllerSupermarket.Instance.SetPositionOfPlayer(initPos);
        foreach (GameObject item in enable)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in disable)
        {
            item.SetActive(false);
        }
        SimpleMessageHandler.Instance.ShowMessage("Lets place promotional banners in our store.");
    }
}