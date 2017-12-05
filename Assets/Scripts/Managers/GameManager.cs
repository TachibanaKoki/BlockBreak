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

    public static int PlayerATK
    {
     get {
            return PlayerPrefs.GetInt("ATK",1);
        }   
    }

    public static int PlayerSPEED
    {
        get
        {
           return  PlayerPrefs.GetInt("SPEED", 1);

        }
    }

    public static int PlayerDEF
    {
        get
        {
            return PlayerPrefs.GetInt("DEF", 3);
        }
    }

    private int BestScore;

    private int coin;


    private void Awake()
    {
        if (I == null)
        {
            DontDestroyOnLoad(gameObject);
            I = this;
            coin = PlayerPrefs.GetInt("Coin");
        }
        else
        {
            Destroy(gameObject);
        }
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

    public void AddCoin(int _coin)
    {
        coin = coin + _coin;
        PlayerPrefs.SetInt("Coin", coin);
        Debug.Log(_coin+"のコインを取得した");
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
