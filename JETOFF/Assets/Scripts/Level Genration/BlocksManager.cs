using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{

    public static BlocksManager Instance; 
    [SerializeField] private GameObject _blockPrefab;
    private List<GameObject> _blocks;


    private GameObject _lastUsedBlock;

    // Start is called before the first frame update
    void Start()
    {
        _blocks = new List<GameObject>();
        Instance = this;

        StartCoroutine(generateBlockChain(10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public GameObject getLastBlock()
    {
        return _lastUsedBlock;
    }

    public void setLastUsedBlock(GameObject block)
    {
        this._lastUsedBlock = block;
    }

    public IEnumerator generateBlockChain(int num)
    {
        for(int i=0; i<num; i++)
        {
            if (i == 0)
            {
                GameObject a = Instantiate(_blockPrefab, Vector3.zero, Quaternion.identity);
                _blocks.Add(a);
                _lastUsedBlock = a;
            }
            else
            {
                Vector3 lastPos = _blocks[_blocks.Count -1].GetComponent<SingleBlock>().endPosition.position;
                GameObject a = Instantiate(_blockPrefab, lastPos, Quaternion.identity);
                _blocks.Add(a);
                _lastUsedBlock = a; 

            }
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
}
