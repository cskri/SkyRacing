using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class FinishLineScript : MonoBehaviour
{
    public GameObject endMenuUI;
    public GameObject TimeOverlayUI;
    public GameObject car;
    public static bool GameIsFinished=false;
    void Start()
    {
        GameIsFinished = false;
    }
    void OnTriggerEnter()
    {
        GameIsFinished = true;
        TimeOverlay.StopTime();
        TimeOverlayUI.SetActive(false);
        car.SetActive(false);
        endMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        

    }
    
}
