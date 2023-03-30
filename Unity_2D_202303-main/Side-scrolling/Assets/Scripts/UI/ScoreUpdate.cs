using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{
    Text scoreLabel;

    void Awake()
    {
        scoreLabel = GetComponent<Text>();   
    }

    void Start()
    {
        
    }

    void Update()
    {
        scoreLabel.text = ScoreManager.score.ToString();
    }
}
