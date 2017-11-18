using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndLine : MonoBehaviour {

    [SerializeField]
    float HP = 30;

    float MaxHp = 0;

    Material mat;
    Color startColor;
    [SerializeField]
    Color endColor;

    [SerializeField]
    GameObject Model;

	// Use this for initialization
	void Start ()
    {
        if(PlayerPrefs.HasKey("DEF"))
        {
            HP = PlayerPrefs.GetFloat("DEF");
        }
        else
        {
            PlayerPrefs.SetFloat("DEF",HP);
        }

        MaxHp = HP;
        mat = Model.GetComponent<Renderer>().material;
        startColor = mat.color;
        GameRuleManager.I.Initialize += Init;
	}

    private void Init()
    {
        mat.color = startColor;
        HP = MaxHp;
    }

	
	// Update is called once per frame
	void Update () {
        mat.color = Color.Lerp(startColor,endColor,1-HP/MaxHp);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ひとまず何が当たってもダメージ
        HP -= 1;

        Model.transform.DOShakePosition(0.2f,0.1f,30).Restart();

        if(HP< 0)
        {
            HP = 0;
            GameRuleManager.I.GameOver();
        }
    }
}
