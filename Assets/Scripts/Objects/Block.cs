using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float HP = 1;
    public float MaxHp;


    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        MaxHp = HP;
    }

    void ObjectActive()
    {
        HP = MaxHp;
        renderer.enabled = true;
    }

    void ObjectDestroy()
    {
        Destroy(gameObject, 1.0f);
        renderer.enabled = false;
        EffectManager.I.BlockExprosion(transform.position);
    }

    

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Ball")
        {
            HP--;
        }

        if(col.transform.tag == "EndLine")
        {
            ObjectDestroy();
        }
        else if(HP<=0)
        {
            ObjectDestroy();
        }
    }
}
