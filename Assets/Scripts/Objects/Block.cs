using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float HP = 1;

    [HideInInspector]
    public float MaxHp;
    [HideInInspector]
    public Transform _transform;
    [HideInInspector]
    private Vector3 m_velocity = new Vector3(0, -3, 0);


    protected Renderer[] m_renderers;
    protected Collider2D m_collision;
    private UnityEngine.Events.UnityAction ObjectDisable;


    public bool isActive { get { return m_collision.enabled; } }


    private void Start()
    {
        m_renderers = transform.GetComponentsInChildren<Renderer>();
        m_collision = gameObject.GetComponent<Collider2D>();
        MaxHp = HP;
        ObjectDisable += ObjectDestroy;
        _transform = gameObject.transform;
        ObjectActive();
     }

    private void Update()
    {
        Move();
    }

    protected void Move()
    {
        _transform.Translate(m_velocity*Time.deltaTime);
    }

    public void ObjectActive()
    {
        HP = MaxHp;
        foreach(var renderer in m_renderers)
        {
            renderer.enabled = true;
        }
        m_collision.enabled = true;
    }   

    protected virtual void ObjectDestroy()
    {
        foreach (var renderer in m_renderers)
        {
            renderer.enabled = false;
        }
        m_collision.enabled = false;
        EffectManager.I.BlockExprosion(transform.position);
    }   

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Ball")
        {
            HP= Mathf.Max(0, HP - col.gameObject.GetComponent<Player>().m_atk);
        }

        if(col.transform.tag == "EndLine"|| HP <= 0)
        {
            ObjectDestroy();
        }
    }
}
