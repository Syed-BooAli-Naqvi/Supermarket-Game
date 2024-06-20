using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleMessageHandler : Singleton<SimpleMessageHandler>
{
    public CanvasGroup myG;
    public TMP_Text msgTxt;

    public void ShowMessage(string msg)
    {
        msgTxt.text = msg;
        myG.alpha = 1;
        Invoke(nameof(HideMsg), 3f);
    }

    public void HideMsg()
    {
        myG.alpha = 0;
    }
}
