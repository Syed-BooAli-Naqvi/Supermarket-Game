using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static PlayerHandler;
using static UIHandler;

public class SuperMarketSaleBannerHandler : MonoBehaviour, IInteractable
{
    public GameObject realBanner;
    public bool pick;
    public void Interact()
    {
        if (pick)
        {
            if (CurrentBannerStand == null)
            {
                Debug.Log("Added Listener");
                InteractHandBtn.gameObject.SetActive(true);
                InteractHandBtn.onClick.AddListener(() =>
                {
                    PlayerPrefs.SetInt(SharedPref.BannerComplete, 1);
                    FCP.canRotate = false;
                    FCP.canMove = false;
                    PlayerHandler.Instance.currentBannerStand = GetComponent<BannerStand>();
                    transform.SetParent(BoxPosT);

                    DOTween.To(() => transform.localScale, x => transform.localScale = x, new Vector3((float)(88.26/3), (float)(88.26 / 3), (float)(88.26 / 3)), 0.2f);
                    DOTween.To(() => transform.position, x => transform.position = x, BoxPosT.position, 0.5f).OnComplete(() =>
                    {
                        FCP.canRotate = true;
                        FCP.canMove = true;
                    });
                    GetComponent<Collider>().enabled = false;
                    DisableInteractBtn();
                    PlayerPrefs.SetInt(SharedPref.BannerPickTut,1);
                    if (PlayerPrefs.GetInt(SharedPref.BannerPlaceTut) != 1)
                        TutorialManager.Instance.StartBannerPlaceTut();
                });
            }
        }
        else
        {
            if (CurrentBannerStand != null)
            {
                Debug.Log("Added Listener");
                InteractHandBtn.gameObject.SetActive(true);
                InteractHandBtn.onClick.AddListener(() =>
                {
                    FCP.canRotate = false;
                    FCP.canMove = false;
                    DOTween.To(() => PlayerHandler.Instance.currentBannerStand.transform.localScale, x => PlayerHandler.Instance.currentBannerStand.transform.localScale = x, new Vector3(88.26f, 88.26f, 88.26f), 0.2f);
                    DOTween.To(() => PlayerHandler.Instance.currentBannerStand.transform.position, x => PlayerHandler.Instance.currentBannerStand.transform.position = x, transform.position, 0.5f).OnComplete(() =>
                    {
                        FCP.canRotate = true;
                        FCP.canMove = true;
                        PlayerHandler.Instance.currentBannerStand.gameObject.SetActive(false);
                        PlayerHandler.Instance.currentBannerStand = null;
                        realBanner.SetActive(true);
                        gameObject.SetActive(false);
                    });
                    DisableInteractBtn();
                    PlayerPrefs.SetInt(SharedPref.BannerPlaceTut, 1);
                    if (PlayerPrefs.GetInt(SharedPref.LaptopTut) != 1)
                        TutorialManager.Instance.StartComputerTutorial();
                });
            }
        }
    }

    public void NonInteract()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
