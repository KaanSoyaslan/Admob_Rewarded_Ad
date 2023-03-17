using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using System;
using TMPro;

public class RewardedAd : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public Button rewardedbtn;//free coin button

    public static bool istek = false;
    public static int istenenID = 0; //reklamý izlenen araç  

    private void Start()
    {
        LoadAD(); //reklam yolla 
    }
    public void LoadAD()
    {
        string adUnitId;
#if UNITY_ANDROID
    // adUnitId = "ca-app-pub-3940256099942544/5224354917";//test id
        
#elif UNITY_IPHONE
                adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print(
        //    "HandleRewardedAdFailedToLoad event received with message: "
        //                     + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }


    public void HandleUserEarnedReward(object sender, Reward args)
    {

        if (istenenID != 0) //freeCoin deðilse
        {
            cARodul();
        }
        else
        {
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 100);
        }
    }

    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded()) //reklam yüklendiyse
        {
            this.rewardedAd.Show();
            LoadAD(); //tekrar reklam yükle
        }
    }
    void Update()
    {
        if (rewardedAd.IsLoaded()) //reklam yüklenmedikçe buton kapalý
        {
            rewardedbtn.interactable = true;
        }
        else
        {
            rewardedbtn.interactable = false;
        }



        if (istek) //CarSelcet kýsmýndan reklam isteði
        {
            istek = false;
            UserChoseToWatchAd();
        }
    }

    public void cARodul()
    {
        PlayerPrefs.SetInt("isCarAdWatched" + istenenID, PlayerPrefs.GetInt("isCarAdWatched" + istenenID) - 1);
        if (PlayerPrefs.GetInt("isCarAdWatched" + istenenID) == 1)
        {
            PlayerPrefs.SetInt("isCarBuyed?" + istenenID, 1);
        }
        CarSelect.DurumRefresh = true;
        istenenID = 0;
    }

}