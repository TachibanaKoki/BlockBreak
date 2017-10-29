using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchState
{
    None,Start,Touched,End
}


public struct TouchResult
{
    public Vector3 oldPosition;
    public Vector3 position;
    public RaycastHit rayCastResult;
    public TouchState state;
}

public class TouchManager : MonoBehaviour {

    public static TouchManager I;

    public TouchResult result;

    public Vector3 MoveDirection
    {
        get { return Vector3.Normalize(result.oldPosition - result.position); }
    }

    public GameObject GetHitGameObject
    {
        get
        {
            if (result.state == TouchState.None) return null;
            if (result.rayCastResult.transform == null) return null;
            return result.rayCastResult.transform.gameObject;
        }

    }


	// Use this for initialization
	void Start ()
    {
        TouchManager.I = this;
        result = new TouchResult();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (IsTouchStart())
        {
            HitCheck();
        }
        else if(IsTouched())
        {
            HitCheck();
        }
        else if(IsTouchEnd())
        {
            HitCheck();
        }
        else
        {
            result.state = TouchState.None;
        }
    }

    public bool IsTouchStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            result.state = TouchState.Start;
            return true;
        }
        return false;
    }

    public bool IsTouched()
    {
        if (Input.GetMouseButton(0))
        {
            result.state = TouchState.Touched;
            return true;
        }
        return false;
    }
    public bool IsTouchEnd()
    {
        if (Input.GetMouseButtonUp(0))
        {
            result.state = TouchState.End;
            return true;
        }
        return false;
    }

    private void HitCheck()
    {
        result.oldPosition = result.position;
        result.position = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            result.rayCastResult = hit;
        }
    }
}
