using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour {

    [SerializeField]
    Text coinText;

    [SerializeField]
    GameObject PowerUP;

    public int UserCoin = 0;

	// Use this for initialization
	void Start ()
    {
        UserCoin = PlayerPrefs.GetInt("Coin");
        coinText.text = UserCoin.ToString();
        SoundManager.PlayBGM("Home");
	}

    public void NextBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
	
    public void PowerUp()
    {
        PowerUP.SetActive(true);
    }

    public void ClosePowerUp()
    {
        PowerUP.SetActive(false);
    }
}
