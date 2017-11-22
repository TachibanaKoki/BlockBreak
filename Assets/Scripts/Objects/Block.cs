using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float HP = 1;


    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Ball")
        {
            HP--;
        }

        if(col.transform.tag == "EndLine")
        {
            Destroy(gameObject);
            EffectManager.I.BlockExprosion(transform.position);
        }
        else if(HP<=0)
        {
            Destroy(gameObject);
            EffectManager.I.BlockExprosion(transform.position);
        }
    }
}
