﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    private void Awake()
    {
        SetUpSignleTon();
    }

    private void SetUpSignleTon()
    {
        int numberSessions = FindObjectsOfType<GameSession>().Length;
        if (numberSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
    public int GetScore() 
    {
        return score;
    }
    public void AddToScore(int scoreValue) 
    {
        score += scoreValue;
    }

    public void RestGame() 
    {
        Destroy(gameObject);
    }
}
