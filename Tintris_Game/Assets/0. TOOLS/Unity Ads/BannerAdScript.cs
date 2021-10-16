
// This Script was found on Unity Docs at:
// https://docs.unity3d.com/Packages/com.unity.ads@3.2/manual/MonetizationBannerAdsUnity.html

using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdScript : MonoBehaviour {

    public string gameId = "1234567";
    public string placementId = "bannerPlacement";
    public bool testMode = true;

    private WaitForSeconds _waitForSecondsObj;

    void Start ()
    {
        _waitForSecondsObj = new WaitForSeconds(0.5f);
        // Initialize the SDK if you haven't already done so:
        Advertisement.Initialize (gameId, testMode);
        StartCoroutine (ShowBannerWhenReady ());
    }

    IEnumerator ShowBannerWhenReady () {
        while (!Advertisement.IsReady (placementId)) {
            yield return _waitForSecondsObj;
        }
        Advertisement.Banner.Show (placementId);
    }
}