using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerControllerSupermarket : Singleton<PlayerControllerSupermarket>
{
    public GameObject player;
    public FirstPersonController fcp;
    public Transform[] cratePositions, cratePositions1, itemPositions;
    public int crateCount;
    public GameObject currentBox;
    public Transform playerBoxPos;

    public void PickBox(GameObject g)
    {
        if (currentBox == null)
        {
            currentBox = g;
            currentBox.transform.SetParent(playerBoxPos);
            DOTween.To(() => g.transform.position, x => g.transform.position = x, playerBoxPos.position, 0.7f);
            DOTween.To(() => g.transform.localEulerAngles, x => g.transform.localEulerAngles = x, playerBoxPos.localEulerAngles, 0.7f);
        }
        else
        {
            SimpleMessageHandler.Instance.ShowMessage("Place current box first.");
        }
    }
    public void PlaceObject()
    {
        if (currentBox != null)
        {
            SetCratePosition2(currentBox.transform.GetChild(0));
        }
        else
        {
            SimpleMessageHandler.Instance.ShowMessage("Pick box first.");
        }
    }

    public void SetCratePosition(Transform t)
    {
        DOTween.To(() => t.transform.position, x => t.transform.position = x, cratePositions[crateCount].position, 1f);
        t.GetComponent<Collider>().enabled = false;
        crateCount += 1;
        if (crateCount == 4)
        {
            SupermarketLevelsManager.Instance.ShowLevelCompletePanel();
        }
    }
    public void SetCratePosition1(Transform t)
    {
        DOTween.To(() => t.transform.position, x => t.transform.position = x, cratePositions1[crateCount].position, 1f);
        t.GetComponent<Collider>().enabled = false;
        crateCount += 1;
        if (crateCount == 4)
        {
            SimpleMessageHandler.Instance.ShowMessage("Great now tomorrow you will place these boxes in shelves.");
            SupermarketLevelsManager.Instance.ShowLevelComplteWithDelay(4);
        }
    }

    public void AddPromo()
    {
        crateCount += 1;
        if (crateCount == 6)
        {
            //SimpleMessageHandler.Instance.ShowMessage("Great now tomorrow you will open the market.");
            SupermarketLevelsManager.Instance.ShowLevelCompletePanel();
        }
    }

    public void SetCratePosition2(Transform t)
    {
        t.SetParent(itemPositions[crateCount]);
        Destroy(currentBox);
        currentBox = null;
        DOTween.To(() => t.transform.position, x => t.transform.position = x, itemPositions[crateCount].position, 1f);
        DOTween.To(() => t.transform.localEulerAngles, x => t.transform.localEulerAngles = x, itemPositions[crateCount].localEulerAngles, 0.7f);
        crateCount += 1;
        if (crateCount == 4)
        {
            SimpleMessageHandler.Instance.ShowMessage("Next you will promote your store.");
            SupermarketLevelsManager.Instance.ShowLevelComplteWithDelay(4);
        }
    }

    public async void SetPositionOfPlayer(Transform t)
    {
        fcp.canMove = false;
        player.transform.position = t.position;
        await Task.Delay(100);
        fcp.canMove = true;
    }
}
