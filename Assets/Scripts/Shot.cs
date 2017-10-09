using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    [SerializeField]
    GameObject m_bullet;

    [SerializeField]
    Vector3 m_inpulse = new Vector3(0,10,0);

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            GameObject go = GameObject.Instantiate(m_bullet,transform.position,Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(m_inpulse,ForceMode.Impulse);
        }
	}
}
