using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScores : MonoBehaviour
{
    [SerializeField] private string _levelName = "Level ";
    [SerializeField] private ScoreUi[] _scores;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<HighscoreStruct> highscores = DatabaseManager.Instance.GetHighscores(_levelName);
        int i = 0;
        foreach (HighscoreStruct score in highscores)
        {
            _scores[i]._nameText.text = score.name;
            _scores[i]._rocketText.text = score.rocket.ToString();
            
            float minutes = score.time / 60;
            float seconds = score.time % 60;
            float milliseconds = score.time - (int)score.time;

            _scores[i]._timeText.text = string.Format("{0:00}:{1:00}{2:.000}", minutes, seconds, milliseconds);
            i++;
        }
        
    }
}
