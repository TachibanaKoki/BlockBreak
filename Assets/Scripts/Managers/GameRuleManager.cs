﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

/// <summary>
/// メインのゲームルールを制御するクラス
/// 主にUIで表示する必要のパラメータを制御します
/// </summary>
public class GameRuleManager : MonoBehaviour
{

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

    /// <summary>
    /// 初期化イベント
    /// ゲームのリトライなどでの初期化時に呼び出す\\\\\
    /// </summary>
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

    /// <summary>
    ///　このクラスの初期化
    /// </summary>
    private void Init()
    {
        state = GameState.Start;
        time = m_TimeLimit;
        gameInstance = GameObject.Instantiate(GameInstanceRef);
        gameInstance.SetActive(true);
        ResultObject.SetActive(false);
        SoundManager.PlayBGM("Main");
        //SoundManager.PlaySE("start");
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

    /// <summary>
    /// ゲームオーバー時に呼び出す
    /// </summary>
    public void GameOver()
    {
        if (state != GameState.Playing) return;

        state = GameState.Result;
        int score = GameManager.I.Score;
        Time.timeScale = 1.0f;

        ResultObject.transform.Find("Score").GetComponent<Text>().text = "スコア："+score.ToString();

        int bestScore = PlayerPrefs.GetInt("BestScore");

        if(bestScore< score)
        {
            //ニューレコード
            bestScore = score;
            PlayerPrefs.SetInt("BestScore",bestScore);
        }

        //自機を破壊する
        Destroy(gameInstance.transform.Find("bullet").gameObject);

        //動画広告を確率で表示する
        RandomShowAds();

        ResultObject.transform.Find("BestScore").GetComponent<Text>().text = "ベストスコア:"+bestScore.ToString();
        ResultObject.SetActive(true);
    }

    void RandomShowAds()
    {
        //確率で動画広告を表示する
        if (Random.Range(0, 2) == 0)
        {
            ResultObject.transform.Find("Ads").gameObject.SetActive(true);
        }
        else
        {
            ResultObject.transform.Find("Ads").gameObject.SetActive(false);
        }
    }

    /// <summary>
    ///  時間制限
    /// </summary>
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

    /// <summary>
    /// ゲームを新規に開始する
    /// </summary>
    public void ReTry()
    {
        Destroy(gameInstance);
        Initialize();
        GameManager.I.ScoretoCoin();
    }

    /// <summary>
    /// タイトルに戻る
    /// </summary>
    public void TitleBack()
    {
        GameManager.I.ScoretoCoin();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
    
    /// <summary>
    /// 全画面動画広告を再生する
    /// </summary>
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    /// <summary>
    /// 動画広告の結果
    /// </summary>
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                GameManager.I.AddCoin(100);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    /// <summary>
    /// ゲーム開始入力待機
    /// </summary>
    void StartWait()
    {
        if (TouchManager.I.IsTouchStart())
        {
            state = GameState.Playing;
        }
    }
}
