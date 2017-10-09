using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField]
    float pow = 10;

    Rigidbody2D rigidbody2d;
    Vector3 startPosition;

	// Use this for initialization
	void Start ()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(TouchManager.I.IsTouchStart())
        {
            rigidbody2d.simulated = false;
            startPosition = TouchManager.I.result.position;
        
        }
        
        if(TouchManager.I.IsTouchEnd())
        {
            rigidbody2d.simulated = true;
            Vector3 dir = (startPosition - TouchManager.I.result.position).normalized;
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.AddForce(dir*pow,ForceMode2D.Impulse);
        }
	}
}
