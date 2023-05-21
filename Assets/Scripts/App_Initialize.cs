using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App_Initialize : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject mainMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject player;
    private bool hasGameStarted = false; 

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
    }

    public void RestartGame() {
        SceneManager.LoadScene(0); //loads whatever scene is at index 0 in build settings
    }

    public void ShowAd() {
        StartCoroutine(StartGame(1.0f));
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