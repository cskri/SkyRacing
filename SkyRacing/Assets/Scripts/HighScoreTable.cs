using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private string SaveString;
    void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        setSaveString();

        string jsonString = PlayerPrefs.GetString(SaveString);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        highscores = SortListByScore(highscores);

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

    }

    private void setSaveString()
    {
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
    }
    private Highscores SortListByScore(Highscores highscores)
    {
        // Sort entry list by score
        if (!(highscores.highscoreEntryList.Count == 0))
        {
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].scoreMs < highscores.highscoreEntryList[i].scoreMs)
                    {
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }
        }
        while(highscores.highscoreEntryList.Count>11)
        {
            highscores.highscoreEntryList.RemoveAt(highscores.highscoreEntryList.Count -1);
        }
        return highscores;
    }
    private void CreateHighScoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 0: rankString = "AUTEURS"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().SetText(rankString);
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().SetText(highscoreEntry.scoreString);
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(highscoreEntry.name);

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        if (rank == 0)
        {
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().color = Color.green;
        }

        switch (rank)
        {
            default:
                entryTransform.Find("trophy").gameObject.SetActive(false); break;
            case 0:
                entryTransform.Find("trophy").GetComponent<Image>().color = new Color32(18, 150, 0, 255); break;
            case 1:
                entryTransform.Find("trophy").GetComponent<Image>().color = new Color32(251, 185, 41, 255); break;
            case 2:
                entryTransform.Find("trophy").GetComponent<Image>().color = new Color32(200, 200, 200, 255); break;
            case 3:
                entryTransform.Find("trophy").GetComponent<Image>().color = new Color32(133, 63, 20, 255); break;
        }

        transformList.Add(entryTransform);
    }
    
    public void AddHighscoreEntry(double scoreMs, string scoreString, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry{ scoreMs = scoreMs, scoreString = scoreString, name = name};
        setSaveString();
        Highscores highscores;

        string jsonString = PlayerPrefs.GetString(SaveString);
        highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if(highscores == null)
        {
            highscores = new Highscores();
            highscores.highscoreEntryList = new List<HighscoreEntry>();
        }

        highscores.highscoreEntryList.Add(highscoreEntry);
        highscores = SortListByScore(highscores);
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(SaveString, json);
        PlayerPrefs.Save();
    }
    private class Highscores{
        public Highscores (){
            
        }
        public List<HighscoreEntry> highscoreEntryList;
    }
    [System.Serializable]
    public class HighscoreEntry
    {
        public double scoreMs;
        public string scoreString;
        public string name;
    }
}
