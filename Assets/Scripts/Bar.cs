using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //このオブジェクトをタッチしている場合
        if(TouchManager.I.result.state == TouchState.Touched&&TouchManager.I.GetHitGameObject == gameObject)
        {
            transform.Translate(new Vector3(-TouchManager.I.MoveDirection.x*0.1f,0,0));

        }
		
	}
}
