using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public AudioClip FinishSound;
    public GameObject TimeLabel;
    public GameObject SavedLabel;
    public GameObject inputField;
    public HighScoreTable HTable;
    public TimeOverlay tOverlay;
    TextMeshProUGUI tText;
    private string endTime;
    private double endTimeMs;
    private bool timeSet = false;
    private bool soundStarted = false;
    AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        tText = TimeLabel.GetComponent<TextMeshProUGUI>();
        source = gameObject.AddComponent<AudioSource>();
        source.Stop();
        soundStarted=false;
        timeSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(FinishLineScript.GameIsFinished && !timeSet)
        {
            if(!soundStarted)
            {
                source.clip = FinishSound;
                source.volume = 100;
                source.loop = false;
                source.Play();
                soundStarted = true;
            }
            timeSet = true;
            endTime = tOverlay.getTimeString();
            endTimeMs = TimeOverlay.getTimeMs();
            
            tText.SetText("TIME: " + endTime);
        }
    }
    public void Save()
    {
        string iName = inputField.GetComponent<TMP_InputField>().text;
        HTable.AddHighscoreEntry(endTimeMs, endTime, iName);
        //HTable.AddHighscoreEntry(18000, "00:18.00", iName);
        
        SavedLabel.SetActive(true);
    }
   
}
