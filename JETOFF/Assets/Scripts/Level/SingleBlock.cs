using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBlock : MonoBehaviour
{
    public float Offset; 
    public Transform endPosition;
    public Transform leftPosition;
    public Transform rightPosition;
    StateManager state;


    [Header("BLOCK COMPONENTS PREFABS")]
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private List<GameObject> _TargetPrefabs;
    [SerializeField] private GameObject _TriggerPrefab;
    [SerializeField] private List<GameObject> _ObstaclePrefab;


    [Header("BLOCK Z OFFSET")]
    [SerializeField] private float _Zoffset;

    private List<GameObject> _ammoList;
    private List<GameObject> _targetsList;

    // Start is called before the first frame update
    void Start()
    {
        _ammoList = new List<GameObject>();
        _targetsList = new List<GameObject>();
        Offset = 10f;
        state = StateManager.instance;
        generateAmmo();
        generateTriggersAndtarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(endPosition.position.z < (state.transform.position.z-Offset))
        {
            transform.position = BlocksManager.Instance.getLastBlock().GetComponent<SingleBlock>().endPosition.position;
            refresh();
            BlocksManager.Instance.setLastUsedBlock(this.gameObject);
        }
    }

    public void refresh()
    {

        repositionAmmo();
    }

    public void generateObstacles()
    {

    }

    public void generateTargets()
    {

    }
    public void generateAmmo()
    {
        float zDistance = Random.Range(_Zoffset, _Zoffset*2f);
            
        for(int i = 0; i <= LevelDifficulty.maxAmmo; i++)
        {
            if(_ammoList.Count >= LevelDifficulty.maxAmmo)
            {
            }
            else
            {
                Vector3 pos; 
                int leftOrRight = Random.Range(1, 4) ;
                switch(leftOrRight)
                {
                    case 1:
                        pos = leftPosition.transform.position;
                        break;
                    case 2:
                        pos = rightPosition.transform.position;
                        break;
                    case 3:
                        pos = leftPosition.transform.position;
                        break;
                    default:
                        pos = leftPosition.transform.position;
                        break;

                }



                //Adding Distance Between Ammo
                pos += new Vector3(0f, 0.4f, zDistance);
                
                GameObject a = Instantiate(_ammoPrefab,pos, _ammoPrefab.transform.rotation);
                a.transform.SetParent(this.transform);

                _ammoList.Add(a);
                zDistance += _Zoffset;
                //if 3 then generate Ammo Boxs
                if (leftOrRight == 3)
                {
                    GameObject b = Instantiate(_ammoPrefab, rightPosition.transform.position + new Vector3(0f, 0.4f, 0f), _ammoPrefab.transform.rotation);
                    b.transform.SetParent(this.transform);
                    _ammoList.Add(b);
                }
            }

        }
    }


    public void generateTriggersAndtarget()
    {
        float zDistance = Random.Range(_Zoffset, _Zoffset * 2f);

        for (int i = 0; i <= LevelDifficulty.maxTargets; i++)
        {
            if (_targetsList.Count >= LevelDifficulty.maxTargets)
            {
            }
            else
            {
                Vector3 pos;
                int leftOrRight = Random.Range(1, 3);
                switch (leftOrRight)
                {
                    case 1:
                        pos = leftPosition.transform.position;
                        break;
                    case 2:
                        pos = rightPosition.transform.position;
                        break;
                    case 3:
                        pos = rightPosition.transform.position;
                        break;
                    default:
                        pos = leftPosition.transform.position;
                        break;

                }



                //Adding Distance Between Ammo
                pos += new Vector3(0f, 0f, zDistance);
                int r = Random.Range(0, _TargetPrefabs.Count);
                GameObject a = Instantiate(_TriggerPrefab, pos, _TriggerPrefab.transform.rotation);
                a.transform.SetParent(this.transform);

                GameObject aT = Instantiate(_TargetPrefabs[r], new Vector3(endPosition.position.x, endPosition.position.y ,pos.z + 10f) + Vector3.forward *10, _TargetPrefabs[r].transform.rotation);
                aT.transform.SetParent(this.transform);

                a.GetComponent<MissileTrigger>().setTarget(aT);



                _targetsList.Add(a);
                zDistance += _Zoffset;

                //if 3 then generate TwoTriggers
                if (leftOrRight == 3)
                {
                    GameObject b = Instantiate(_TriggerPrefab, rightPosition.position, _TriggerPrefab.transform.rotation);
                    b.transform.SetParent(this.transform);

                    GameObject bT = Instantiate(_TargetPrefabs[r], new Vector3(endPosition.position.x, endPosition.position.y, pos.z + 10f) + Vector3.forward * 2, _TargetPrefabs[r].transform.rotation);
                    bT.transform.SetParent(this.transform);

                    b.GetComponent<MissileTrigger>().setTarget(aT);

                    _targetsList.Add(b);
                }

            }

        }
    }

    public void repositionAmmo()
    {
        foreach(GameObject a in _ammoList)
        {
            Vector3 pos;
            int leftOrRight = Random.Range(1, 3);
            switch (leftOrRight)
            {
                case 1:
                    pos = leftPosition.transform.position;
                    break;
                case 2:
                    pos = rightPosition.transform.position;
                    break;
                default:
                    pos = leftPosition.transform.position;
                    break;

            }
            a.transform.position = new Vector3(pos.x, a.transform.position.y, a.transform.position.z);
        }
    }
}
