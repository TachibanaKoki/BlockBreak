using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Advertisements;

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
        if (state != GameState.Playing) return;
        state = GameState.Result;
        int score = GameManager.I.Score;
        Time.timeScale = 1.0f;
        ResultObject.transform.Find("Score").GetComponent<Text>().text = score.ToString();

        int bestScore = PlayerPrefs.GetInt("BestScore");

        if(bestScore< score)
        {
            //ニューレコード
            bestScore = score;
            PlayerPrefs.SetInt("BestScore",bestScore);
        }

        //自機を破壊する
        Destroy(gameInstance.transform.Find("bullet").gameObject);

        ResultObject.transform.Find("BestScore").GetComponent<Text>().text = bestScore.ToString();
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

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

    void StartWait()
    {
        if (TouchManager.I.IsTouchStart())
        {
            state = GameState.Playing;
        }
    }
}
