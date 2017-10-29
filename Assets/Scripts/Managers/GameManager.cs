using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum GameState
{
    Start, Playing, Result
}


public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [SerializeField]
    float m_TimeLimit = 300;

    [SerializeField]
    Text m_timeText;

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
    }

    private void Init()
    {
        time = m_TimeLimit;
        state = GameState.Start;
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
                TimeController();
                break;
            case GameState.Result:
                Result();
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
    }

    public void GameOver()
    {
        state = GameState.Result;
    }

    void TimeController()
    {
        time -= Time.deltaTime;
        //ゲームオーバー
        if (time < 0.0f)
        {
            time = 0.0f;
            GameOver();
        }

        if(TouchManager.I.IsTouchStart())
        {
            Time.timeScale = 0.1f;
        }
        else if(TouchManager.I.IsTouchEnd())
        {
            Time.timeScale = 1.0f;
        }
    }

    void Result()
    {
        if (TouchManager.I.IsTouchStart())
        {
            Initialize();
        }
    }

    void StartWait()
    {
        if (TouchManager.I.IsTouchStart())
        {
            state = GameState.Playing;
        }
    }
}
