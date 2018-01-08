using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        TransitionManager.I.FadeOut(0.3f);
    }
	
    public void PowerUp()
    {
        PowerUP.SetActive(true);
        PowerUP.transform.DOLocalMoveX(0, 0.1f);
    }

    public void ClosePowerUp()
    {
        PowerUP.SetActive(false);
        PowerUP.transform.DOLocalMoveX(-800, 0.1f);
    }
}
