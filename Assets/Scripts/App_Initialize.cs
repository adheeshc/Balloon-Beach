using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;


public class App_Initialize : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject mainMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject player;
    public GameObject adButton;
    public GameObject restartButton;

    private bool hasGameStarted = false;
    private bool hasSeenRewardedAd = false;
    private bool testMode = true;

    public string myGameIdAndroid = "5286201";
    public string myGameIdIOS = "5286200";
    public string adUnitIdAndroid = "Rewarded_Android";
    public string adUnitIdIOS = "Rewarded_iOS";
    public string myAdUnitId;

    private void Awake() {
        Shader.SetGlobalFloat("_Curvature", 2.0f);
        Shader.SetGlobalFloat("_Trimming", 0.1f);
        Application.targetFrameRate = 60;
    }

    private void Start() {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        inGameUI.gameObject.SetActive(false);
        mainMenuUI.gameObject.SetActive(true);
        gameOverMenuUI.gameObject.SetActive(false);

#if UNITY_IOS
        Advertisement.Initialize(myGameIdIOS, testMode);
        myAdUnitId = adUnitIdIOS;
#else
	    Advertisement.Initialize(myGameIdAndroid, testMode);
	    myAdUnitId = adUnitIdAndroid;
#endif
    }

    public void PlayGame() {
        if(hasGameStarted == true) {
            StartCoroutine(StartGame(1.0f));
        }
        else {
            StartCoroutine(StartGame(0.0f));
        }
    }

    public void PauseGame() {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        hasGameStarted = true;
        inGameUI.gameObject.SetActive(false);
        mainMenuUI.gameObject.SetActive(true);
        gameOverMenuUI.gameObject.SetActive(false);
    }

    public void GameOver() {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        hasGameStarted = true;
        inGameUI.gameObject.SetActive(false);
        mainMenuUI.gameObject.SetActive(false);
        gameOverMenuUI.gameObject.SetActive(true);
        if (hasSeenRewardedAd == true) {
            adButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            adButton.GetComponent<Button>().enabled = false;
            adButton.GetComponent<Animator>().enabled = false;
            restartButton.GetComponent<Animator>().enabled = true;
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(0); //loads whatever scene is at index 0 in build settings
    }

    public void ShowAd() {
        if(Advertisement.isInitialized) {
            Advertisement.Load(myAdUnitId);
            Advertisement.Show(myAdUnitId);
            hasSeenRewardedAd = true;
        }
        StartCoroutine(StartGame(5.0f));
    }

    private IEnumerator StartGame(float waitTime) {
        inGameUI.gameObject.SetActive(true);
        mainMenuUI.gameObject.SetActive(false);
        gameOverMenuUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }
}