using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHandler : Singleton<PlayerHandler>
{
    public GameObject boxCam;
    public static GameObject BoxCam { get { return Instance.boxCam; } }
    public Transform boxPosT;
    public static Transform BoxPosT { get { return Instance.boxPosT; } }
    public FirstPersonController fpc;
    public static FirstPersonController FCP { get { return Instance.fpc; } }
    public DeliveryBox currentDeliveryBox;
    public static DeliveryBox CurrentDeliveryBox { get { return Instance.currentDeliveryBox;  } }
    public BannerStand currentBannerStand;
    public static BannerStand CurrentBannerStand { get { return Instance.currentBannerStand; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
