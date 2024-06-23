using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimpleInteractableObj : MonoBehaviour,IInteractable
{
    public UnityEvent instantInteractEvent, interactEvent;
    public UnityEvent instantNonInteractEvent;
    public bool canSimpleInteract = true;
    public bool canChange = true;

    public void Interact()
    {
        instantInteractEvent.Invoke();
        if (canSimpleInteract)
        {
            var InteractHandBtn = SupermarketLevelsManager.Instance != null ? SupermarketLevelsManager.InteractHandBtn : UIHandler.InteractHandBtn;
            var CFCG = SupermarketLevelsManager.Instance != null ? SupermarketLevelsManager.CFCG : UIHandler.CFCG;
            InteractHandBtn.gameObject.SetActive(true);
            InteractHandBtn.onClick.AddListener(() =>
            {
                if (canChange)
                    CFCG.alpha = 0;
                interactEvent.Invoke();
                InteractHandBtn.gameObject.SetActive(false);
                InteractHandBtn.onClick.RemoveAllListeners();
                if (canChange)
                    StartCoroutine(EnableCFCG(CFCG));
            });
        }
    }
    public IEnumerator EnableCFCG(CanvasGroup CFCG)
    {
        yield return new WaitForSeconds(1);
        CFCG.alpha = 1;
    }

    public void NonInteract()
    {
        instantNonInteractEvent.Invoke();
    }
}
