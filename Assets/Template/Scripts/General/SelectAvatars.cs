using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAvatars : MonoBehaviour
{
    public EnterNameAndAvatar enterNameAndAvatar;


    private void Start()
    {
        SelectAvatar(PlayerPrefs.GetInt(SharedPref.PlayerAvatarNum));
        #region Name And Country Buttons
        enterNameAndAvatar.continueBtn.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString(SharedPref.IsRegistered, "true");
            OpenClosePopup(false);
        });
        enterNameAndAvatar.field.onEndEdit.AddListener(delegate
        {
            Debug.LogError("End Edit");
            PlayerPrefs.SetString(SharedPref.UserName, enterNameAndAvatar.field.text);
        });
        for (int i = 0; i < enterNameAndAvatar.selectBtns.Length; i++)
        {
            int j = enterNameAndAvatar.selectBtns[i].transform.GetSiblingIndex();
            enterNameAndAvatar.selectBtns[i].onClick.AddListener(() => SelectAvatar(j));
        }

        if (PlayerPrefs.GetString(SharedPref.UserName) != "")
        {
            enterNameAndAvatar.field.text = PlayerPrefs.GetString(SharedPref.UserName);
        }

        enterNameAndAvatar.selectIcons[PlayerPrefs.GetInt(SharedPref.PlayerAvatarNum)].SetActive(true);
        if (PlayerPrefs.GetString(SharedPref.IsRegistered, "false") != "true")
        {
            OpenClosePopup(true);
        }
        else
        {
            OpenClosePopup(false);
        }
        #endregion

    }
    public void SelectAvatar(int index)
    {
        for (int i = 0; i < enterNameAndAvatar.selectIcons.Length; i++)
        {
            enterNameAndAvatar.selectIcons[i].SetActive(false);
        }
        PlayerPrefs.SetInt(SharedPref.PlayerAvatarNum, index);
        enterNameAndAvatar.selectIcons[index].SetActive(true);
    }

    public void OpenClosePopup(bool enable)
    {
        enterNameAndAvatar.targetPopup.SetActive(enable);
    }


    [System.Serializable]
    public struct EnterNameAndAvatar
    {
        public GameObject targetPopup;
        public Button continueBtn;
        public InputField field;
        public GameObject[] selectIcons;
        public Button[] selectBtns;
        public ScrollRect scroll;
    }
}
