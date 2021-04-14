
using UnityEngine;
using TMPro;
public class WorldsManager : MonoBehaviour
{

    [SerializeField] private World[] _worlds;
    [SerializeField] private TextMeshProUGUI _flowersAmountText;



    private void OnEnable()
    {
        _flowersAmountText.text = PlayerPrefs.GetInt("FlowersAmount", 0).ToString();
        Setup();
    }

    private void Setup()
    {
        int flowersAmount = PlayerPrefs.GetInt("FlowersAmount", 0);
        for (int i=1;i<_worlds.Length;i++)
        {
           
            if(flowersAmount>= _worlds[i].price)
            {
                _worlds[i].lockImage.SetActive(false);
                _worlds[i].priceOB.SetActive(false);
                _worlds[i].btn.interactable = true;
            }

        }
    }


    public void OpenPanel(GameObject openPanel)
    {
        AdsManager.Instance.ShowInterstitial_ByChance(5);
        TransitionManager.Instance.OpenPanel(openPanel);
        TransitionManager.Instance.ClosePanel(this.gameObject);
    }

    public void BackToMenu()
    {
        AdsManager.Instance.ShowInterstitial_ByChance(5);
        LoadScene("Menu");
    }

    private void LoadScene(string scene)
    {
        TransitionManager.Instance.LoadScene(scene);
    }




    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.UdarGames.BeeTrip&hl=en-US&ah=Afkc9HLegbnlucRQrDEeMvJ36t4");
    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8140650053281162522&hl=en_US");
    }

    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/udargames/");
    }

}
