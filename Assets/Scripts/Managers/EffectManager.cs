using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public static EffectManager I;

    [SerializeField]
    GameObject m_BlockExporsion;

	// Use this for initialization
	void Awake ()
    {
        I = this;
	}

    public void BlockExprosion(Vector3 posion)
    {
        GameObject.Instantiate(m_BlockExporsion, posion,Quaternion.identity);
    }
}
