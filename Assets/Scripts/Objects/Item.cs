using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///   プレイヤーに接触するとスコアが上がるオブジェクト
/// </summary>
public class Item : Block {

    protected override void OnCollisionEnter2D(Collision2D col)
    {
    }
    protected override void ObjectDestroy()
    {
        foreach (var renderer in m_renderers)
        {
            renderer.enabled = false;
        }
        m_collision.enabled = false;
        EffectManager.I.ItemGetEffect(transform.position);
    }

    protected void  OnTriggerEnter2D(Collider2D col)
    {
        //自機
        if (col.transform.tag == "Ball")
        {
            ObjectDestroy();
            GameManager.I.AddScore();
        }

        //防衛ライン
        if (col.transform.tag == "EndLine")
        {
            ObjectDestroy();
        }
    }
}
