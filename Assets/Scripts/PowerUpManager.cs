using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{

    [SerializeField]
    Text m_atkText;

    [SerializeField]
    Text m_speedText;

    [SerializeField]
    Text m_defText;

    [SerializeField]
    Text m_atkCoin;

    [SerializeField]
    Text m_speedCoin;

    [SerializeField]
    Text m_defCoin;

    [SerializeField]
    Text m_userCoin;

    private int atk;
    private int speed;
    private float def;

    private void OnEnable()
    {
        atk = PlayerPrefs.GetInt("ATK");
        speed = PlayerPrefs.GetInt("SPEED");
        def = PlayerPrefs.GetFloat("DEF");
        m_atkText.text = "攻撃力：" + atk.ToString();
        m_speedText.text = "速度：" + speed.ToString();
        m_defText.text = "耐久力：" + def.ToString();

        m_atkCoin.text = (atk * 10).ToString();
        m_speedCoin.text = (speed * 10).ToString();
        m_defCoin.text = (def * 10).ToString();
    }

    public void AtkUp()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        int coinCount = atk * 10;
        if (coin <coinCount) return;
        coin -= coinCount;
        atk++;
        m_atkCoin.text = (atk * 10).ToString();
        PlayerPrefs.SetInt("ATK",atk);
        PlayerPrefs.SetInt("Coin",coin);
        m_userCoin.text = coin.ToString();
        m_atkText.text = "攻撃力：" + atk.ToString();
    }

    public void SpeedUp()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        int coinCount = speed * 10;
        if (coin < coinCount) return;
        coin -= coinCount;
        speed++;
        m_speedCoin.text = (speed * 10).ToString();
        PlayerPrefs.SetInt("SPEED", speed);
        PlayerPrefs.SetInt("Coin", coin);
        m_userCoin.text = coin.ToString();
        m_speedText.text = "速度：" + speed.ToString();
    }

    public void DefUp()
    {
        int coin = PlayerPrefs.GetInt("Coin");
        int coinCount = (int)def * 10;
        if (coin < coinCount) return;
        coin -= coinCount;
        def++;
        m_defCoin.text = (def * 10).ToString();
        PlayerPrefs.SetFloat("DEF", def);
        PlayerPrefs.SetInt("Coin", coin);
        m_userCoin.text = coin.ToString();
        m_defText.text = "耐久力：" + def.ToString();
    }
}
