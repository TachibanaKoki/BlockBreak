using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Util : MonoBehaviour {

    public static Util I;

	// Use this for initialization
	void Start () {
        I = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Delay(UnityAction action,float duration = 1.0f)
    {
        I.RunDelay(action,duration);
    }

    public void RunDelay(UnityAction action, float duration = 1.0f)
    {
        StartCoroutine(DelayCoroutine(action,duration));
    }

    private IEnumerator DelayCoroutine(UnityAction action, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (action != null) { action.Invoke(); }
    }

    public static void SetSliderValue(Slider slider,float value)
    {
        slider.value = Mathf.Lerp(slider.value, value, 0.7f);
    }
}
