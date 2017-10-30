using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {
    [SerializeField]
    GameObject m_Block;

    [SerializeField]
    float Interval = 3;

    [SerializeField]
    float resetValue = 0.3f;

    float startInterval;
	// Use this for initialization
	void Start ()
    {
        startInterval = Interval;
        GameManager.I.Initialize += Init;
        //CollectBlockSpawn();
        Init();
	}

    private void Init()
    {
        StopAllCoroutines();
        Interval = startInterval;
        StartCoroutine(IntervalBlockSpawn());
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public IEnumerator IntervalBlockSpawn()
    {
        GameObject.Instantiate(m_Block, new Vector3(Random.Range(0, 10) - 4.0f, 11f, 0f), Quaternion.identity);
        while (true)
        {
            yield return new WaitForSeconds(Interval);
            GameObject.Instantiate(m_Block, new Vector3(Random.Range(0,10) - 4.0f,11f,0f), Quaternion.identity);
            Interval = -0.1f;
            if(Interval<= resetValue)
            {
                Interval = startInterval - resetValue;
            }
        }
    }

    public void CollectBlockSpawn()
    {
        for (int i = 0; i < 100; i++)
        {
            if (Random.Range(0, 10) == 0)
            {
                GameObject.Instantiate(m_Block, new Vector3((i % 10) - 4.0f, (i / 10) - 3.0f, 0), Quaternion.identity);
            }
        }
    }
}
