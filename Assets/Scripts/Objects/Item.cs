using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///   プレイヤーに接触するとスコアが上がるオブジェクト
/// </summary>
public class Item : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        //自機
        if (col.transform.tag == "Ball")
        {
            Destroy(gameObject);
            GameManager.I.AddScore();
            EffectManager.I.ItemGetEffect(transform.position);
        }

        //防衛ライン
        if (col.transform.tag == "EndLine")
        {
            Destroy(gameObject);
        }
    }
}
