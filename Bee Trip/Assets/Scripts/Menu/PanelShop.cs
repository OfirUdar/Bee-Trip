using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PanelShop : MonoBehaviour
{
    public Button[] btnsPlayers;

    [Space]

    public Color colorSelect;
    public Color colorUnselect;
    public GameObject btnWatchVideos;


    private Image[] _imagesBtnsPlayers;

    private Button _btnSelected;

    private TextMeshProUGUI _textVideosLeft;

    private int _defultLeftVideos=5;



    private void OnEnable()
    {
        AdsManager.Instance.OnCompleteWatchRewardedVideo += OnCompleteWatchRewardVideo;
    }

    private void OnDisable()
    {
        AdsManager.Instance.OnCompleteWatchRewardedVideo -= OnCompleteWatchRewardVideo;
    }

    private void Start()
    {
        Setup();
    }



    private void Setup()
    {
        SetImagesBtns();
        SetActiveLocksOfSprites();
        SetCurrentPlayerToSelect();

        _textVideosLeft = btnWatchVideos.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        SetVideosLeft();
    }

    private void SetImagesBtns()
    {
        int i = 0;
        _imagesBtnsPlayers = new Image[btnsPlayers.Length];
        foreach (Button btn in btnsPlayers)
        {
            _imagesBtnsPlayers[i] = btn.GetComponent<Image>();
            i++;
        }
    }
    private void SetActiveLocksOfSprites()
    {
        foreach (Button btn in btnsPlayers)
        {
            Image image = btn.transform.GetChild(0).GetComponent<Image>();
            int videosLeft = PlayerPrefs.GetInt("VideosLeft" + btn.name, _defultLeftVideos);
            if (videosLeft <= 0)
            {
                image.color = Color.white;
                btn.transform.GetChild(1).gameObject.SetActive(false); //disable the lock image
            }
            else
                image.color = Color.black;

            if(btn.name.Equals("Bee"))
                image.color = Color.white;


        }
    } //disable all the locks of sprite that the player has
    private void SetCurrentPlayerToSelect()
    {
        string currentPlayer = PlayerPrefs.GetString("CurrentPlayer", "Bee");
        foreach (Image image in _imagesBtnsPlayers)
        {
            if(image.name.Equals(currentPlayer))
            {
                _btnSelected = image.GetComponent<Button>();
                image.color = colorSelect;
            }
                
            else
                 image.color = colorUnselect;
        }


        


    } //set the current sprite
    private int SetVideosLeft()
    {  
       
        int videosLeft = PlayerPrefs.GetInt("VideosLeft" + _btnSelected.name, _defultLeftVideos);
        if (_btnSelected.name.Equals("Bee"))
            videosLeft = 0;

        if (videosLeft <= 0)
            btnWatchVideos.SetActive(false);
        else
        {
            btnWatchVideos.SetActive(true);
            _textVideosLeft.text = videosLeft + " Left";
        }

        return videosLeft;


    } //display the amount of videos that left to purchase the sprites


    public void SelectPlayer(Button btnSelected)
    {
        this._btnSelected = btnSelected;
        foreach (Image image in _imagesBtnsPlayers)
            image.color = colorUnselect;
        this._btnSelected.GetComponent<Image>().color = colorSelect;


        int videosLeft = SetVideosLeft(); // set the text of count videos left and return the number of it
        if (videosLeft <= 0)
        {
            PlayerPrefs.SetString("CurrentPlayer", _btnSelected.name);//set the curret player
        }
    }// select player

    public void WatchVideo()
    {
        //  ****** I NEED TO CHANGE IT *******
       // PlayerPrefs.SetString("CurrentPlayer", _btnSelected.name);
   
        int videosLeft = PlayerPrefs.GetInt("VideosLeft" + _btnSelected.name, _defultLeftVideos);
        AdsManager.Instance.ShowRewardVideo();

        ////after watch video
        //PlayerPrefs.SetInt("VideosLeft" + _btnSelected.name, videosLeft-1);
    }

    private void OnCompleteWatchRewardVideo()
    {
        int videosLeft = PlayerPrefs.GetInt("VideosLeft" + _btnSelected.name, _defultLeftVideos);
        videosLeft--;
        PlayerPrefs.SetInt("VideosLeft" + _btnSelected.name, videosLeft);
        if (videosLeft <= 0)
        {
            PlayerPrefs.SetString("CurrentPlayer", _btnSelected.name);//set the curret player
            _btnSelected.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            _btnSelected.transform.GetChild(1).gameObject.SetActive(false); //disable the lock image 
            SoundsManager.Instance.PlayPurchase();
        }
        SetVideosLeft();
    }


    public void ClosePanelShop(GameObject openPanel)
    {
        AdsManager.Instance.ShowInterstitial_ByChance(4);
        TransitionManager.Instance.OpenPanel(openPanel);
        TransitionManager.Instance.ClosePanel(this.gameObject);
    }
}
