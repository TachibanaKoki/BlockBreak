using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Result : MonoBehaviour
{

    [SerializeField]
    Text m_CauseofDeathText;
    [SerializeField]
    Text m_BestScoreText;
    [SerializeField]
    Text m_ScoreText;

    [SerializeField]
    GameObject m_AdsObject;

    [SerializeField]
    GameObject m_TitleBack;

    [SerializeField]
    GameObject m_Retry;

	public void Open(string caseOfDeath)
    {
        //死因を表示&データ送信
        m_CauseofDeathText.text = caseOfDeath;
        UnityEngine.Analytics.Analytics.CustomEvent("caseOfDeath", new Dictionary<string, object>
        {
            { "caseOfDeath", caseOfDeath }
        });

        //各種ボタンを順に表示
        m_Retry.transform.DOLocalMove(m_Retry.transform.localPosition,0.5f);
        m_Retry.transform.localPosition -= Vector3.right*800;

        Vector3 target = m_TitleBack.transform.localPosition;
        m_TitleBack.transform.localPosition -= Vector3.right * 800;
        Util.Delay(() =>
        {
            m_TitleBack.transform.DOLocalMove(target, 0.5f);
        }, 0.1f);

        //スコアを表示
        int score = GameManager.I.Score;
        m_ScoreText.text = "スコア：" + score.ToString();
        int bestScore = PlayerPrefs.GetInt("BestScore");
        if (bestScore < score)
        {
            //ニューレコード
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        m_BestScoreText.text = "ベストスコア:" + bestScore.ToString();

        //動画広告を確率で表示する
        RandomShowAds();

        //オブジェクトを有効化
        gameObject.SetActive(true);
    }


void RandomShowAds()
{
    //確率で動画広告を表示する
    if (Random.Range(0, 2) == 0)
    {
        m_AdsObject.SetActive(true);
        Vector3 target = m_AdsObject.transform.localPosition;
        m_AdsObject.transform.localPosition -= Vector3.right * 800;
        Util.Delay(() =>
        {
            m_AdsObject.transform.DOLocalMove(target, 0.5f);
        }, 0.5f);
     }
    else
    {
        m_AdsObject.SetActive(false);
    }
}
}
