using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class MissilesManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _missileCounter;



    [SerializeField] private Missile _missilePrefab;

    private ObjectPool<Missile> _missilesPool;
    public Transform missilesOrigin;


    public int availableMissiles;
    private int _availableMissilesTemp;
    private bool _counterRunning;




    [HideInInspector]
    public static MissilesManager Instance;


    [HideInInspector]
    public bool _missilesReady;

    // Start is called before the first frame update
    void Start()
    {

        _counterRunning = false;
        Instance = this; 
        _missilesReady = false;
        StartCoroutine(initMissiles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Creating Missiles Pools
    void initPool()
    {
        _missilesPool = new ObjectPool<Missile>(() => {
            return Instantiate(_missilePrefab);
        },
        missile =>
        {
            missile.gameObject.SetActive(true);
        },
        missile =>
        {

            missile.gameObject.SetActive(false);
        },
        missile =>
        {

            Destroy(missile.gameObject);
        },
        false,
        30,
        50
        );
        _missilesReady = true;
    }

    IEnumerator initMissiles()
    {
        //foreach()
        initPool();
        yield return null;
    }

    public Missile getMissile()
    {

        Missile m = _missilesPool.Get();
        m.unlockTarget();
        return m;
    }

    public void destroyMissile(Missile missile)
    {
        if (ExplosionManager.instance._explosionPool.CountActive >= 10)
        {
            _missilesPool.Release(missile);
            return;
        }
            
         ExplosionManager.instance.kaboomAtLocation(missile.transform.position);
        _missilesPool.Release(missile);

    }


    public void FireMissile( Transform target)
    {
        if(_availableMissilesTemp <= 0)
        {
            _availableMissilesTemp = 0;
            availableMissiles = 0;
            _missileCounter.text = "" + availableMissiles;
            return;
        }


        Missile m = getMissile();
        m.lockTarget(target);
        _availableMissilesTemp -= 1;
        if (_counterRunning)
        {
            return;
        }
        StartCoroutine(updateMissilesDown());

    }


    public void missileAquired(int number)
    {
        _availableMissilesTemp += number; 
        if(_counterRunning)
        {
            return;
        }
        else
        {
            StartCoroutine(updateMissilesUp()); 
        }
    }

    IEnumerator updateMissilesUp()
    {

        while(availableMissiles < _availableMissilesTemp)
        {
            _counterRunning = true;
            availableMissiles =availableMissiles + 1;
            _missileCounter.text = "" + availableMissiles;
            yield return new WaitForSeconds(0.1f);

        }

        _counterRunning = false; 
        yield break; 
    }

    IEnumerator updateMissilesDown()
    {

        while (availableMissiles > _availableMissilesTemp)
        {
            _counterRunning = true;
            availableMissiles =  availableMissiles - 1;
            _missileCounter.text = "" + availableMissiles;
            yield return new WaitForSeconds(0.1f);

        }

        _counterRunning = false;
        yield break;
    }
}
