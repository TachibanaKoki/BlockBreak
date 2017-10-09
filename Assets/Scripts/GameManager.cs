using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Start, Playing, Result
}


public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [SerializeField]
    float m_TimeLimit = 30;

    [SerializeField]
    Text m_timeText;

    public GameState state;

    float time = 0;


    // Use this for initialization
    void Start()
    {
        I = this;
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

    void TimeController()
    {
        time -= Time.unscaledDeltaTime;
        //ゲームオーバー
        if (time < 0.0f)
        {
            time = 0.0f;
            state = GameState.Result;
        }
    }

    void Result()
    {
        if (TouchManager.I.IsTouchStart())
        {
            time = m_TimeLimit;
            state = GameState.Start;
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
