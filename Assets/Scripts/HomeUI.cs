using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour {

    [SerializeField]
    Text coinText;

    public int UserCoin = 0;

	// Use this for initialization
	void Start ()
    {
        UserCoin = PlayerPrefs.GetInt("Coin");
        coinText.text = UserCoin.ToString();
	}

    public void NextBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
	
    public void PowerUp()
    {

    }
}
