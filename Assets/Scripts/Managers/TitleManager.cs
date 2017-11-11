using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

    bool isNext = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isNext) return;
		if(TouchManager.I.IsTouchStart())
        {
            SceneManager.LoadSceneAsync("Main");
            isNext = true;
        }
	}
}
