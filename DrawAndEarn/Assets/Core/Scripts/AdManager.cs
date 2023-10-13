using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    // REAL ID : ca-app-pub-6788314039807028/5337352546

#if UNITY_ANDROID
    string _adUnitID = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
string _adUnitID = "ca-app-pub-3940256099942544/1712485313";
#else
string _adUnitID = "unused";
#endif

    [SerializeField] private GameManager _gameManager;
    
    private RewardedAd _rewardedAd;
    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });

        StartCoroutine(LoadRewardAd());
    }
    IEnumerator  LoadRewardAd()
    {
        yield return new WaitForSeconds(3f);
        LoadRewardedAd();
    }
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitID, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
                RegisterEventHandlers(_rewardedAd);
            });
    }
    
    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                _gameManager.CarpiIkiSkor();
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }
    private void RegisterEventHandlers(RewardedAd ad)
    {

        
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            LoadRewardedAd();
        };
    }
}
