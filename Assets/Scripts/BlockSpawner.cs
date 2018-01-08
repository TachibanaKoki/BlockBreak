using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブロックの生成を行う
/// </summary>
public class BlockSpawner : MonoBehaviour
{
    [Tooltip("生成するブロックのプレハブ")]
    [SerializeField]
    GameObject m_Block;

    [Tooltip("生成するアイテムのプレハブ")]
    [SerializeField]
    GameObject m_itemPrefab;

    [Tooltip("ブロックを生成する間隔")]
    [SerializeField]
    float Interval = 3;

    [Tooltip("ブロック生成間隔をもとに戻す値")]
    [SerializeField]
    float resetValue = 0.3f;

    [SerializeField]
    bool isPowerUp = true;

    float startInterval;

    int spawnCount = 0;
    int level = 1;

    List<Block> m_meteoPool;
    List<Block> m_itemPool;


    // Use this for initialization
    void Start()
    {
        startInterval = Interval;
        GameRuleManager.I.Initialize += Init;
        m_meteoPool = new List<Block>();
        m_itemPool = new List<Block>();
        //CollectBlockSpawn();
        Init();
    }

    private void Init()
    {
        //StopAllCoroutines();
        Interval = startInterval;
        spawnCount = 0;
        StartCoroutine(LineSpawnPattern());
    }

    /// <summary>
    /// リトライ時にインスタンスを破棄するのでイベントの登録を解除
    /// </summary>
    void OnDestroy()
    {
        GameRuleManager.I.Initialize -= Init;
        StopAllCoroutines();
    }


    //列単位でオブジェクトを追加していく
    public IEnumerator LineSpawnPattern()
    {
        //m_Pool.Add(GameObject.Instantiate(m_Block, new Vector3(Random.Range(0, 10) - 4.0f, 11f, 0f), Quaternion.identity, transform).GetComponent<Block>());
        while (true)
        {
            yield return new WaitForSeconds(Interval);
            if (GameRuleManager.I.state != GameState.Playing) continue;

            spawnCount++;
            level = (spawnCount/3)+1;
            LineSpawn();
            Interval = -0.1f;
            if (Interval <= resetValue)
            {
                Interval = startInterval - resetValue;
            }
        }
    }
    private void LineSpawn()
    {
        //横一列分のブロック生成処理
        Block[] blocks = m_meteoPool.FindAll(n => n.isActive == false).ToArray();
        Block[] items = m_itemPool.FindAll(n => n.isActive == false).ToArray();
        for (int i = 0; i < 8; i++)
        {
            int random = Random.Range(0,2);
            if (random == 0)
            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    MeteoSpawn(blocks, i);
                }
                else
                {
                    ItemSpawn(items,i);
                }
            }
        }
    }
    
    private void MeteoSpawn(Block[] blocks,int index)
    {
        Block block = null;
        if (index < blocks.Length)
        {
            blocks[index]._transform.position = new Vector3((index * 1.2f) - 4.0f, 11f, 0f);
            blocks[index].ObjectActive();
            block = blocks[index];
        }
        else
        {
            GameObject go = GameObject.Instantiate(m_Block, new Vector3((index * 1.2f) - 4.0f, 11f, 0f), Quaternion.identity, transform);
            m_meteoPool.Add(go.GetComponent<Block>());
            block = go.GetComponent<Block>();
        }

        block.HP = Random.Range(1,level + 1);
        block.MaxHp = block.HP;
    }

    private void ItemSpawn(Block[] blocks, int index)
    {
        if (index < blocks.Length)
        {
            blocks[index]._transform.position = new Vector3((index * 1.2f) - 4.0f, 11f, 0f);
            blocks[index].ObjectActive();
        }
        else
        {
            GameObject go = GameObject.Instantiate(m_itemPrefab, new Vector3((index * 1.2f) - 4.0f, 11f, 0f), Quaternion.identity, transform);
            m_itemPool.Add(go.GetComponent<Block>());
        }
    }

}
