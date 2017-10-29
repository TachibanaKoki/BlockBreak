using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour {

    [SerializeField]
    float HP = 30;

    float MaxHp = 0;

	// Use this for initialization
	void Start ()
    {
        MaxHp = HP;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ひとまず何が当たってもダメージ
        HP -= 1;

        if(HP< 0)
        {
            HP = 0;
            GameManager.I.GameOver();
        }
    }
}
