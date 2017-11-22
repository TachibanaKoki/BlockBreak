using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField]
    float pow = 10;

    [SerializeField]
    GameObject arrow;

    public int m_atk = 1;
    public int m_speed= 1;

    Rigidbody2D rigidbody2d;
    Vector3 startPosition;

	// Use this for initialization
	void Start ()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        arrow.SetActive(false);

        if (PlayerPrefs.HasKey("ATK"))
        {
            m_atk = PlayerPrefs.GetInt("ATK");
        }
        else
        {
            PlayerPrefs.SetInt("ATK", m_atk);
        }

        if (PlayerPrefs.HasKey("SPEED"))
        {
            m_speed = PlayerPrefs.GetInt("SPEED");
        }
        else
        {
            PlayerPrefs.SetInt("SPEED", m_speed);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(TouchManager.I.IsTouchStart())
        {
            rigidbody2d.simulated = false;
            startPosition = TouchManager.I.result.position;
            arrow.SetActive(true);
        }
        else if(TouchManager.I.IsTouched())
        {
            Vector3 dir = (startPosition - TouchManager.I.result.position).normalized;
            float rota = -Mathf.Atan2(dir.x,dir.y);
            gameObject.transform.localRotation = Quaternion.Euler(0,0,rota*Mathf.Rad2Deg);
        } 
        else if(TouchManager.I.IsTouchEnd())
        {
            rigidbody2d.simulated = true;
            Vector3 dir = (startPosition - TouchManager.I.result.position).normalized;
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.AddForce(dir*pow* (1+m_speed*0.1f), ForceMode2D.Impulse);
            arrow.SetActive(false);
        }
        else
        {
            Vector3 vel = rigidbody2d.velocity;
            float rota = -Mathf.Atan2(vel.x, vel.y);
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, rota * Mathf.Rad2Deg);
        }
	}
}
