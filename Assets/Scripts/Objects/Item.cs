using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Ball")
        {
            Destroy(gameObject);
            GameManager.I.AddScore();
            EffectManager.I.ItemGetEffect(transform.position);
        }

        if (col.transform.tag == "EndLine")
        {
            Destroy(gameObject);
        }
    }
}
