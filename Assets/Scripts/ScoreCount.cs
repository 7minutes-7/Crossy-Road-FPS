using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text bestScoreText;

    [SerializeField] float scoreDistance = 2.0f;

    static private int score = 0;
    static private int bestScore = 0;


    private void Start()
    {
        score = 0;
        bestScore = PlayerPrefs.GetInt("bestscore");
        bestScoreText.text = "Best " + bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {   
        if((transform.position.z / scoreDistance > score)) { 
            score++;
            scoreText.text = score.ToString();
        }

        if(score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = "Best " + bestScore.ToString();

            PlayerPrefs.SetInt("bestscore", bestScore);
        }

    }
}
