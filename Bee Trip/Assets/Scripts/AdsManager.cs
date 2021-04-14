
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class AdsManager : MonoBehaviour
{

    public static AdsManager Instance { private set; get; }


    public bool isPublish;

    [Space]
    public string bannerID;
    public string interstitalID;
    public string rewardVideoID;


    private string _testBannerID = "ca-app-pub-3940256099942544/6300978111";
    private string _testInterstitialID = "ca-app-pub-3940256099942544/1033173712";
    private string _testRewardVideoID = "ca-app-pub-3940256099942544/5224354917";


    private BannerView _bannerView;
    private InterstitialAd _interstitial;
    private RewardBasedVideoAd _rewardBasedVideo;


    public bool isRemovedADS;


    //Events
    public Action OnCompleteWatchRewardedVideo;

    private void Awake()
    {
        isRemovedADS = PlayerPrefs.GetInt("RemoveADS", 0) == 1;
        if (Instance == null)
        {
            Instance = this;  
            DontDestroyOnLoad(this.gameObject);
            Setup();
        }
        else
        {
            Instance.ShowBanner();
            //Returned to menu
            Destroy(this.gameObject);     
        }

       

    }

    private void OnEnable()
    {
        if(!isRemovedADS)
             SubscribeEvents(true);
    }

    private void OnDisable()
    {
        if (!isRemovedADS)
            SubscribeEvents(false);
    }


    private void Setup()
    {
        if (isPublish)
        {
            MobileAds.Initialize(initStatus => { });
        }
        else
        {
            bannerID = _testBannerID;
            interstitalID = _testInterstitialID;
            rewardVideoID = _testRewardVideoID;
        }


        _bannerView = new BannerView(bannerID,AdSize.SmartBanner, AdPosition.Bottom);
        _interstitial = new InterstitialAd(interstitalID);
        _rewardBasedVideo = RewardBasedVideoAd.Instance;

        if(!isRemovedADS)
        {
            RequestBanner();
            RequestInterstitial();
            RequestRewardVideo();
        }
       
    }



    private void SubscribeEvents(bool active)
    {
        if(active)
        {
            _bannerView.OnAdFailedToLoad += OnBannerFailedLoad;

            _interstitial.OnAdFailedToLoad += OnInterstitialFailedLoad;
            _interstitial.OnAdClosed += OnInterstitialClosed;

            _rewardBasedVideo.OnAdFailedToLoad += OnRewardBasedVideoFailedToLoad;
            _rewardBasedVideo.OnAdClosed += OnRewardBasedVideoClosed;
            _rewardBasedVideo.OnAdRewarded += OnRewardBasedVideoRewarded;
        }
        else
        {
            _bannerView.OnAdFailedToLoad -= OnBannerFailedLoad;

            _interstitial.OnAdFailedToLoad -= OnInterstitialFailedLoad;
            _interstitial.OnAdClosed -= OnInterstitialClosed;

            _rewardBasedVideo.OnAdFailedToLoad -= OnRewardBasedVideoFailedToLoad;
            _rewardBasedVideo.OnAdClosed -= OnRewardBasedVideoClosed;
            _rewardBasedVideo.OnAdRewarded -= OnRewardBasedVideoRewarded;
        }
    }
    

    // Banner
    #region Banner
    private void RequestBanner()
    {
        AdRequest request = new AdRequest.Builder().Build();
        _bannerView.LoadAd(request);
    }


    public void ShowBanner()
    {
        if (!isRemovedADS)
            _bannerView.Show();
    }

    public void HideBanner()
    {
        if (!isRemovedADS)
            _bannerView.Hide();
    }


    private void OnBannerFailedLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if (!isRemovedADS)
            RequestBanner();
    }

    #endregion


    //Interstitial
    #region Interstitial
    private void RequestInterstitial()
    {
        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);
    }


    public void ShowInterstitial()
    {
        if (!isRemovedADS)
        {
            if (_interstitial.IsLoaded())
                _interstitial.Show();
            //else
            //    RequestInterstitial();

        }
            
    }

    public void ShowInterstitial_ByChance(int chance)
    {
        //int randomNum = UnityEngine.Random.Range(0, 101);
        //int tempChance = 100 - chance;
        //if (randomNum > tempChance)
        //    ShowInterstitial();
        if (UnityEngine.Random.Range(1, chance + 1) == 1)
            ShowInterstitial();
    }


    private void OnInterstitialFailedLoad(object sender, AdFailedToLoadEventArgs e)
    {
        RequestInterstitial();
    }

    private void OnInterstitialClosed(object sender, EventArgs e)
    {
        RequestInterstitial();
    }
    #endregion


    //Reward Video
    #region Reward Video
    private void RequestRewardVideo()
    {
        AdRequest request = new AdRequest.Builder().Build();
        _rewardBasedVideo.LoadAd(request,rewardVideoID);
    }

    public void ShowRewardVideo()
    {
        if(!isRemovedADS)
        {
            if (_rewardBasedVideo.IsLoaded())
                _rewardBasedVideo.Show();
            //else
            //    RequestRewardVideo();
        }
        else
        {
            if (OnCompleteWatchRewardedVideo != null)
                OnCompleteWatchRewardedVideo();
        }
        
    }

    public void OnRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestRewardVideo();
    }

    private void OnRewardBasedVideoClosed(object sender, EventArgs args)
    {
        RequestRewardVideo();
    }

    private void OnRewardBasedVideoRewarded(object sender, Reward args)
    {
        if (OnCompleteWatchRewardedVideo != null)
            OnCompleteWatchRewardedVideo();
    }


    #endregion
}
