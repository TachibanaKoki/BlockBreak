using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {
    [SerializeField]
    GameObject m_Block;
	// Use this for initialization
	void Start ()
    {
        BlockSpawn();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void BlockSpawn()
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
