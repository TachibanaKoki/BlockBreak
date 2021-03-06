﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public static EffectManager I;

    [SerializeField]
    GameObject m_BlockExporsion;

    [SerializeField]
    GameObject m_ItemGetEffect;

	// Use this for initialization
	void Awake ()
    {
        I = this;
	}

    public void BlockExprosion(Vector3 posion)
    {
        Destroy(GameObject.Instantiate(m_BlockExporsion, posion,Quaternion.identity),0.5f);
        SoundManager.PlaySE("explosion");
    }

    public void ItemGetEffect(Vector3 posion)
    {
        Destroy(GameObject.Instantiate(m_ItemGetEffect, posion, Quaternion.identity),0.5f);
        SoundManager.PlaySE("powerup");
    }
}
