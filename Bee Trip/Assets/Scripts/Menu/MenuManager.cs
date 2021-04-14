using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{


    [SerializeField] private Button _btnRemoveAds;



    private void OnEnable()
    {
        _btnRemoveAds.interactable = PlayerPrefs.GetInt("RemoveADS", 0) == 0;
    }

    public void Play()
    {
        LoadScene("Worlds");
    }

    private void LoadScene(string scene)
    {
        TransitionManager.Instance.LoadScene(scene);
    }

    public void OpenPanel(GameObject openPanel)
    {
        TransitionManager.Instance.OpenPanel(openPanel);
        TransitionManager.Instance.ClosePanel(this.gameObject);
    }


    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.UdarGames.BeeTrip&hl=en-US&ah=Afkc9HLegbnlucRQrDEeMvJ36t4");
    }
}
