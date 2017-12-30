using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

    bool isNext = true;

	// Use this for initialization
	void Start ()
    {
        isNext = true;
        StartCoroutine(Wait());
	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        isNext = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isNext) return;
		if(TouchManager.I.IsTouchStart())
        {
            SoundManager.PlaySE("select");
            SceneManager.LoadSceneAsync("Home");
            isNext = true;
        }
	}
}
