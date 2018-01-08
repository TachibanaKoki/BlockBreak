using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField]
    Slider m_slider;

    [SerializeField]
    GlitchFx glitch;

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
        glitch = Camera.main.GetComponent<GlitchFx>();
	}

    private void Init()
    {
        mat.color = startColor;
        HP = MaxHp;
    }

	
	// Update is called once per frame
	void Update () {
        mat.color = Color.Lerp(startColor,endColor,1-HP/MaxHp);
        Util.SetSliderValue(m_slider,HP/MaxHp);
	}

    IEnumerator GlitchEffect()
    {
        float time = 0;
        while(true)
        {
            time += Time.deltaTime;

            glitch.intensity = Mathf.Abs(Mathf.Sin((time*20.0f)*Mathf.PI))*0.5f;

            if (time >= 0.1f)
            {
                glitch.intensity = 0;
                break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ひとまず何が当たってもダメージ
        if (collision.transform.tag == "Meteo")
        {
            HP -= 1;

            Model.transform.DOShakePosition(0.2f, 0.1f, 30).Restart();
            StartCoroutine(GlitchEffect());
            if (HP <= 0)
            {
                HP = 0;
                GameRuleManager.I.GameOver("地球滅亡");
            }
        }
        else if(collision.transform.tag == "Ball")
        {
            GameRuleManager.I.GameOver("地球に墜落");
        }
    }
}
