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

    private int m_score;
    public int Score { get { return m_score; } }

    private int BestScore;

    private int coin;

    private void Awake()
    {
        I = this;
        coin = PlayerPrefs.GetInt("Coin");
    }
    // Use this for initialization
    void Start()
    {
        m_score = 0;
    }

    //スコアをコインに変換する
    public void ScoretoCoin()
    {
        coin =coin + Score;
        PlayerPrefs.SetInt("Coin",coin);
        m_score = 0;
    }

    public void AddScore(int value = 1)
    {
        m_score += value;
    }

    public void TimeController()
    {
        if (TouchManager.I.IsTouchStart())
        {
            Time.timeScale = 0.1f;
        }
        else if (TouchManager.I.IsTouchEnd())
        {
            Time.timeScale = 1.0f;
        }
    }
}
