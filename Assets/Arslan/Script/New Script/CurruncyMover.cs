using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CurruncyMover : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 areaSize;
    public GameObject objectToMove;
    float min = -45,max=45;
   public bool Coin;
    private void OnEnable()
    {
        if (!Coin)
        {
                int num =Random.Range(0, CurrencyPoolingSystem.instance.NoteMovingPosList.Count-1);
             targetPosition = CurrencyPoolingSystem.instance.NoteMovingPosList[num].position;

        }
        else if (Coin)
        {
            int num = Random.Range(0, CurrencyPoolingSystem.instance.CoinMovingPosList.Count - 1);
            targetPosition = CurrencyPoolingSystem.instance.CoinMovingPosList[num].position;

        }
        float rot= Random.Range(min, max);
        this.transform.DOMove(targetPosition, 1f).SetEase(Ease.Linear);
        this.transform.DORotate(new Vector3(-90, 0, rot), 0.1f).SetEase(Ease.Linear);
    }

}