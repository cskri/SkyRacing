using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Data;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;

public class TimeOverlay : MonoBehaviour
{
    public AudioClip countDownSound;
    public GameObject TimerOverlayUI;
    public GameObject TimeLabel;
    public GameObject CountDownLabel;
    public GameObject BestTimeLabel;
    public GameObject InfoLabel;
    public GameObject car;
    public GameObject SpeedLabel;
    private CarController carController;
    TextMeshProUGUI cText;
    TextMeshProUGUI tText;
    TextMeshProUGUI bText;
    TextMeshProUGUI sText;
    public static Stopwatch watch;
    private Stopwatch countDownWatch;
    public static bool countdown = true;
    private bool soundStarted = false;
    private bool gameStarted = false;
    private string elapsedMinutes;
    private string elapsedSeconds;
    private string elapsedMs;
    private string endTime;
    private string SaveString;
    
    

    // Start is called before the first frame update
    void Start()
    {
        countdown = true;
        gameStarted = false;
        carController = car.GetComponent<CarController>();
        cText = CountDownLabel.GetComponent<TextMeshProUGUI>();
        tText = TimeLabel.GetComponent<TextMeshProUGUI>();
        sText = SpeedLabel.GetComponent<TextMeshProUGUI>();
        Time.timeScale = 0f;
        watch = new Stopwatch();
        countDownWatch = new Stopwatch();
        countDownWatch.Start();
        bText = BestTimeLabel.GetComponent<TextMeshProUGUI>();
        if (SceneManager.GetActiveScene().name.Equals("RaceTrack1"))
        {
            SaveString = "highscoreTable";
        }
        else if (SceneManager.GetActiveScene().name.Equals("RaceTrack2"))
        {
            SaveString = "highscoreTable2";
        }
        else if (SceneManager.GetActiveScene().name.Equals("RaceTrack3"))
        {
            SaveString = "highscoreTable3";
        }
        bText.text = getBestTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            if(countDownWatch.ElapsedMilliseconds <= 1300)
            {
                cText.SetText("3");
                InfoLabel.SetActive(true);
                if(!soundStarted)
                {
                    AudioSource source = gameObject.AddComponent<AudioSource>();
                    source.clip = countDownSound;
                    source.volume = 100;
                    source.loop = false;
                    source.Play();
                    soundStarted = true;
                }
            }
            else if(countDownWatch.ElapsedMilliseconds <= 2300)
            {
                cText.SetText("2");
            }
            else if(countDownWatch.ElapsedMilliseconds <= 3300)
            {
                cText.SetText("1");
            }
            else if(countDownWatch.ElapsedMilliseconds <= 4300)
            {
                cText.SetText("GO!"); 
                InfoLabel.SetActive(false);
                Time.timeScale = 1f;
                gameStarted = true;
                watch.Start();
            }
            else if(countDownWatch.ElapsedMilliseconds > 4300)
            {
                countdown = false;
                countDownWatch.Stop();
                countDownWatch.Reset();
                cText.SetText("");
            }
        }
        if(gameStarted)
        {
            if(PauseMenu.GameIsPaused)
            {
                if(watch.IsRunning)
                {
                    watch.Stop();
                }
            }
            else
            {
                if(!watch.IsRunning)
                {
                   watch.Start(); 
                }
                if(watch.Elapsed.Minutes<10)
                {
                    elapsedMinutes = "0" + watch.Elapsed.Minutes;
                }
                else{elapsedMinutes="" + watch.Elapsed.Minutes;}
                if(watch.Elapsed.Seconds<10)
                {
                    elapsedSeconds = "0" + watch.Elapsed.Seconds;
                }
                else{elapsedSeconds="" + watch.Elapsed.Seconds;}
                if(watch.Elapsed.Milliseconds < 10)
                {
                    elapsedMs = "00" + watch.Elapsed.Milliseconds;
                }
                else if(watch.Elapsed.Milliseconds<100)
                {
                    elapsedMs = "0" + watch.Elapsed.Milliseconds;
                }
                else{elapsedMs="" + watch.Elapsed.Milliseconds;}

                endTime = elapsedMinutes + ":" + elapsedSeconds + "." + elapsedMs;

                tText.SetText("YOU: " + endTime);
                float tmp = carController.CurrentSpeed;
                float rounded = UnityEngine.Mathf.Round(tmp);
                sText.SetText(rounded + " kph");
            }
        }
    }

    private string getBestTime()
    {
        string jsonString = PlayerPrefs.GetString(SaveString);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        string temp = highscores.highscoreEntryList[0].scoreString;
        return "BEST: " + temp;
    }
    public static void StopTime()
    {
        watch.Stop();
    }
    public string getTimeString()
    {
        return endTime;
    }
    public static double getTimeMs()
    {
        double temp = watch.Elapsed.TotalMilliseconds;
        return temp;
    }
    private class Highscores{
        public Highscores (){
            
        }
        public List<HighScoreTable.HighscoreEntry> highscoreEntryList;
    }
}
