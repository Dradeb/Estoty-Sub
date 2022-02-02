using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExplosionManager : MonoBehaviour
{


    [SerializeField] private GameObject _explosion;
    public ObjectPool<GameObject> _explosionPool;


    public static ExplosionManager instance;
    private Stack<GameObject> _toRelease;
    private bool _isReleasing;
    
    // Start is called before the first frame update
    void Start()
    {
        _isReleasing = false;
        _toRelease = new Stack<GameObject>();
        instance = this;
        initPool();
    }
    void initPool()
    {
        _explosionPool = new ObjectPool<GameObject>(() => {
           
                return Instantiate(_explosion);
        },
        explosion =>
        {
            explosion.gameObject.SetActive(false);
            explosion.gameObject.SetActive(true);
        },
        explosion =>
        {
            if(explosion)
                explosion.gameObject.SetActive(false);
        },
        explosion =>
        {

            Destroy(explosion.gameObject);
        },
        false,
        5,
        10        
        );
    }

    public void releaseExplosion(GameObject exp)
    {
        if(exp !=null)
            _explosionPool.Release(exp);
    }

    public void kaboomAtLocation(Vector3 position)
    {
        GameObject a = _explosionPool.Get();
        _toRelease.Push(a);
        a.transform.position = position;
        if(!_isReleasing)
        {
            StartCoroutine(releaseExplosionCollection());
        }
       
    }

    IEnumerator releaseExplosionCollection()
    {
        _isReleasing = true;
        while(_toRelease.Count > 0)
        {
            releaseExplosion(_toRelease.Pop());
            //somehow wait till animation ends
            yield return new WaitForSeconds(0.4f);
        }
        _isReleasing = false;
        yield break;

    }
}
