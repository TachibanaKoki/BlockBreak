using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// プレイヤーの強化用クラス
/// </summary>
public class PowerUpManager : MonoBehaviour
{
    [Tooltip("攻撃力を表示するテキスト")]
    [SerializeField]
    Text m_atkText;

    [Tooltip("移動速度を表示するテキスト")]
    [SerializeField]
    Text m_speedText;

    [Tooltip("防衛ラインの耐久度を表示するテキスト")]
    [SerializeField]
    Text m_defText;

    [Tooltip("攻撃力を強化するのに必要なコインの数を表示する")]
    [SerializeField]
    Text m_atkCoin;

    [Tooltip("移動速度を強化するのに必要なコインの数を表示する")]
    [SerializeField]
    Text m_speedCoin;

    [Tooltip("耐久度を強化するのに必要なコインの数を表示する")]
    [SerializeField]
    Text m_defCoin;

    [SerializeField]
    Text m_userCoin;

    private int atk;
    private int speed;
    private int def;

    //攻撃力強化時に要求されるコインの上昇率
    private int atkRate = 10;
    //速度強化時に要求されるコインの上昇率
    private int speedRate = 10;
    //防御強化時に要求されるコインの上昇率
    private int defRate = 10;

    private void OnEnable()
    {
        //各種パラメータを取得
        atk = GameManager.PlayerATK;
        speed = GameManager.PlayerSPEED;
        def = GameManager.PlayerDEF;

        //パラメータを表示に反映する
        m_atkText.text = "Attack：" + atk.ToString();
        m_speedText.text = "Speed：" + speed.ToString();
        m_defText.text = "Hp：" + def.ToString();

        m_atkCoin.text = (atk * atkRate).ToString();
        m_speedCoin.text = (speed * speedRate).ToString();
        m_defCoin.text = (def * defRate).ToString();
    }

    /// <summary>
    /// 攻撃力を上昇させる
    /// </summary>
    public void AtkUp()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        int coinCount = atk * atkRate;

        //要求コインに達していれば能力を強化
        if (coin <coinCount) return;
        coin -= coinCount;
        atk++;
        m_atkCoin.text = (atk * atkRate).ToString();
        PlayerPrefs.SetInt("ATK",atk);
        PlayerPrefs.SetInt("Coin",coin);
        PlayerPrefs.Save();
        m_userCoin.text = coin.ToString();
        m_atkText.text = "攻撃力：" + atk.ToString();
    }

    /// <summary>
    /// 速度を上昇させる
    /// </summary>
    public void SpeedUp()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        int coinCount = speed * speedRate;

        //要求コインに達していれば能力を強化
        if (coin < coinCount) return;
        coin -= coinCount;
        speed++;
        m_speedCoin.text = (speed * speedRate).ToString();
        PlayerPrefs.SetInt("SPEED", speed);
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.Save();
        m_userCoin.text = coin.ToString();
        m_speedText.text = "速度：" + speed.ToString();
    }

    /// <summary>
    /// 耐久度を上昇させる
    /// </summary>
    public void DefUp()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        int coinCount = (int)def * defRate;

        //要求コインに達していれば能力を強化
        if (coin < coinCount) { return; }
        coin -= coinCount;
        def++;
        m_defCoin.text = (def * defRate).ToString();
        PlayerPrefs.SetInt("DEF", def);
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.Save();
        m_userCoin.text = coin.ToString();
        m_defText.text = "耐久力：" + def.ToString();
    }
}
