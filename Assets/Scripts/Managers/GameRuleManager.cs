using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public enum GameMode
{
    Survivor,
    TimeAttack
}


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
    Text m_coinText;

    [SerializeField]
    Result ResultObject;

    [SerializeField]
    SpriteRenderer BG_Image;

    [SerializeField]
    GameObject GameInstanceRef;

    GameObject gameInstance;

    public GameState state;

    public GameMode mode = GameMode.Survivor;

    float time = 0;

    [SerializeField]
    GameObject GameStartObject;
    [SerializeField]
    GameObject GameStartEffect;

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
        m_coinText.text = GameManager.I.coin.ToString();
    }

    /// <summary>
    ///　このクラスの初期化
    /// </summary>
    private void Init()
    {
        state = GameState.Start;
        time = m_TimeLimit;
        m_timeText.enabled = (mode == GameMode.TimeAttack);
        gameInstance = GameObject.Instantiate(GameInstanceRef);
        gameInstance.SetActive(true);
        ResultObject.gameObject.SetActive(false);
        SoundManager.PlayBGM("Main");
        GameStartObject.SetActive(true);
        //SoundManager.PlaySE("start");
    }

    IEnumerator ColorFade(float duration = 0.1f, bool isFadeOut=true)
    {
        float timer = 0;

        while(true)
        {
            timer += Time.unscaledDeltaTime;
            
            if(isFadeOut)
            {
                fadeColor = Mathf.Clamp01(fadeColor - (Time.unscaledDeltaTime*(1/duration)));
            }
            else
            {
                fadeColor = Mathf.Clamp01(fadeColor + (Time.unscaledDeltaTime * (1 / duration)));
            }
            BG_Image.material.color = new Color(1,1,1,fadeColor*0.5f+0.5f);
            if (fadeColor<=0||fadeColor>=1) { break; }
            yield return null;
        }
    }

    float fadeColor = 1;

    Coroutine fadeCoroutine = null;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.Start:
                StartWait();
                return;
            case GameState.Playing:
                TimeCountor();
                GameManager.I.TimeController();

                if(TouchManager.I.IsTouchStart())
                {
                    if (fadeCoroutine != null) { StopCoroutine(fadeCoroutine); }
                    StartCoroutine(ColorFade(0.1f,true));
                }
                else if(TouchManager.I.IsTouchEnd())
                {
                    fadeCoroutine = StartCoroutine(ColorFade(0.1f, false));
                }
                break;
            case GameState.Result:
                break;
        }

        if (mode == GameMode.TimeAttack)
        {
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

        m_scoreText.text = GameManager.I.Score.ToString();
        m_coinText.text = GameManager.I.coin.ToString();
    }

    /// <summary>
    /// ゲームオーバー時に呼び出す
    /// </summary>
    public void GameOver(string caseOfDeath)
    {
        if (state != GameState.Playing) return;

        state = GameState.Result;

        Time.timeScale = 1.0f;
        ResultObject.Open(caseOfDeath);

        //自機を破壊する
        GameObject player = gameInstance.transform.Find("bullet").gameObject;
        Destroy(player);
        EffectManager.I.BlockExprosion(player.transform.position);
    }

    /// <summary>
    ///  時間制限
    /// </summary>
    void TimeCountor()
    {
        if (mode != GameMode.TimeAttack) return;
        time -= Time.deltaTime;
        //ゲームオーバー
        if (time < 0.0f)
        {
            time = 0.0f;
            GameOver("時間切れ");
        }
    }

    /// <summary>
    /// ゲームを新規に開始する
    /// </summary>
    public void ReTry()
    {
        Destroy(gameInstance);
        Initialize();
        //GameManager.I.ScoretoCoin();
    }

    /// <summary>
    /// タイトルに戻る
    /// </summary>
    public void TitleBack()
    {
        //GameManager.I.ScoretoCoin();
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
            GameStartObject.SetActive(false);
            GameStartEffect.SetActive(true);
        }
    }
}
