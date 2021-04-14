using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRateUs : MonoBehaviour
{


    [SerializeField] private Animator _anim;



    public void ClosePanel()
    {
        _anim.SetTrigger("Close");
    }
    public void CloseAnimationFinished()
    {
        GameUIManager.Instance.OnCloseOtherPanel();
        this.gameObject.SetActive(false);
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.UdarGames.BeeTrip&hl=en-US&ah=Afkc9HLegbnlucRQrDEeMvJ36t4");
        ClosePanel();
        PlayerPrefs.SetInt("IsRateUs", 1);
    }
}
