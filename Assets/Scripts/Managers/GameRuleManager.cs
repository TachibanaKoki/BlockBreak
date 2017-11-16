﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameRuleManager : MonoBehaviour {

    public static GameRuleManager I;

    [SerializeField]
    float m_TimeLimit = 300;

    [SerializeField]
    Text m_timeText;

    [SerializeField]
    Text m_scoreText;

    [SerializeField]
    GameObject ResultObject;

    [SerializeField]
    GameObject GameInstanceRef;

    GameObject gameInstance;

    public GameState state;

    float time = 0;

    public UnityAction Initialize;

    private void Awake()
    {
        I = this;
    }
    // Use this for initialization
    void Start()
    {
        time = m_TimeLimit;
        state = GameState.Start;
        Initialize += Init;
        Init();
        m_scoreText.text = "0";
    }

    private void Init()
    {
        state = GameState.Start;
        time = m_TimeLimit;
        gameInstance = GameObject.Instantiate(GameInstanceRef);
        gameInstance.SetActive(true);
        ResultObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.Start:
                StartWait();
                break;
            case GameState.Playing:
                TimeCountor();
                GameManager.I.TimeController();
                break;
            case GameState.Result:
                break;
        }

        float val = time - Mathf.Floor(time);
        val *= 100;
        val = Mathf.Floor(val);
        if (val < 10.0f)
        {
            m_timeText.text = Mathf.Floor(time).ToString() + ":0" + val;
        }
        else
        {
            m_timeText.text = Mathf.Floor(time).ToString() + ":" + val;
        }

        m_scoreText.text = GameManager.I.Score.ToString();
    }

    public void GameOver()
    {
        state = GameState.Result;
        ResultObject.SetActive(true);
    }

    void TimeCountor()
    {
        time -= Time.deltaTime;
        //ゲームオーバー
        if (time < 0.0f)
        {
            time = 0.0f;
            GameOver();
        }
    }

    public void Result()
    {
        Destroy(gameInstance);
        Initialize();
        GameManager.I.ScoretoCoin();
    }

    public void TitleBack()
    {
        GameManager.I.ScoretoCoin();
    }

    void StartWait()
    {
        if (TouchManager.I.IsTouchStart())
        {
            state = GameState.Playing;
        }
    }
}
