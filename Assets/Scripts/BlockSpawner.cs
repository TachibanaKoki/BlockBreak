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

    [SerializeField]
    bool isPowerUp = true;

    float startInterval;

    int spawnCount = 0;


	// Use this for initialization
	void Start ()
    {
        startInterval = Interval;
        GameRuleManager.I.Initialize += Init;
        //CollectBlockSpawn();
        Init();
	}

    private void Init()
    {
        //StopAllCoroutines();
        Interval = startInterval;
        spawnCount = 0;
        StartCoroutine(IntervalBlockSpawn());
    }
	
	// Update is called once per frame
	void OnDestroy()
    {
        GameRuleManager.I.Initialize -= Init;
        StopAllCoroutines();
	}

    public IEnumerator IntervalBlockSpawn()
    {
        GameObject.Instantiate(m_Block, new Vector3(Random.Range(0, 10) - 4.0f, 11f, 0f), Quaternion.identity,transform);
        while (true)
        {
            yield return new WaitForSeconds(Interval);
            if (isPowerUp)
            {
                spawnCount++;
            }
            for (int i = 0; i < (spawnCount / 5)+1; i++)
            {
                GameObject go =  GameObject.Instantiate(m_Block, new Vector3(Random.Range(0, 10) - 4.0f, 11f, 0f), Quaternion.identity, transform);
                //go.GetComponent<Block>().HP = (spawnCount / 8)+1;
            }
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
                GameObject.Instantiate(m_Block, new Vector3((i % 10) - 4.0f, (i / 10) - 3.0f, 0), Quaternion.identity,transform);
            }
        }
    }
}
