using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自機の制御を行う
/// </summary>
public class Player : MonoBehaviour {

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
        m_atk = GameManager.PlayerATK;
        m_speed = GameManager.PlayerSPEED;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(TouchManager.I.IsTouchStart())
        {
            TouchStart();
            return;
        }
        else if(TouchManager.I.IsTouched())
        {
            Touched();
            return;
        } 
        else if(TouchManager.I.IsTouchEnd())
        {
            TouchEnd();
            return;
        }

        //通常移動時は進行方向に合わせてオブジェクトを回転させる
        Vector3 vel = rigidbody2d.velocity;
        float rota = -Mathf.Atan2(vel.x, vel.y);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, rota * Mathf.Rad2Deg);
	}

    //タッチ開始時動きを止めて照準用のオブジェクトをアクティブ化
    void TouchStart()
    {
        rigidbody2d.simulated = false;
        startPosition = TouchManager.I.result.position;
        m_arrow.SetActive(true);
    }

    //タッチ中
    //引っ張っている方向に合わせてオブジェクトを回転させる
    void Touched()
    {
        Vector3 dir = (startPosition - TouchManager.I.result.position).normalized;
        float rota = -Mathf.Atan2(dir.x, dir.y);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, rota * Mathf.Rad2Deg);
    }

    //離した時の方向に合わせてオブジェクトを飛ばす
    void TouchEnd()
    {
        //SE再生
        SoundManager.PlaySE("shot");

        //発射処理
        rigidbody2d.simulated = true;
        Vector3 dir = (startPosition - TouchManager.I.result.position).normalized;
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.AddForce(dir * pow * (1 + m_speed * 0.1f), ForceMode2D.Impulse);
        m_arrow.SetActive(false);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        rigidbody2d.angularVelocity = 0;
        rigidbody2d.velocity = (rigidbody2d.velocity.normalized) * pow * (1 + m_speed * 0.1f);
        Debug.Log("velocity"+rigidbody2d.velocity.magnitude);
    }

}
