using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自機の制御を行う
/// </summary>
public class Ball : MonoBehaviour {

    //移動時にかける力
    [SerializeField]
    float pow = 10;

    //移動方向を示すオブジェトの参照
    [SerializeField]
    GameObject m_arrow;

    public int m_atk = 1;
    public int m_speed= 1;

    Rigidbody2D rigidbody2d;
    Vector3 startPosition;

	// Use this for initialization
	void Start ()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        m_arrow.SetActive(false);

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
        //タッチ開始時動きを止めて照準用のオブジェクトをアクティブ化
		if(TouchManager.I.IsTouchStart())
        {
            rigidbody2d.simulated = false;
            startPosition = TouchManager.I.result.position;
            m_arrow.SetActive(true);
        }
        //タッチ中
        //引っ張っている方向に合わせてオブジェクトを回転させる
        else if(TouchManager.I.IsTouched())
        {
            Vector3 dir = (startPosition - TouchManager.I.result.position).normalized;
            float rota = -Mathf.Atan2(dir.x,dir.y);
            gameObject.transform.localRotation = Quaternion.Euler(0,0,rota*Mathf.Rad2Deg);
        } 
        //離した時の方向に合わせてオブジェクトを飛ばす
        else if(TouchManager.I.IsTouchEnd())
        {
            rigidbody2d.simulated = true;
            Vector3 dir = (startPosition - TouchManager.I.result.position).normalized;
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.AddForce(dir*pow* (1+m_speed*0.1f), ForceMode2D.Impulse);
            m_arrow.SetActive(false);
        }
        //通常に移動時は進行方向に合わせてオブジェクトを回転させる
        else
        {
            Vector3 vel = rigidbody2d.velocity;
            float rota = -Mathf.Atan2(vel.x, vel.y);
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, rota * Mathf.Rad2Deg);
        }
	}
}
