using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private bool _isSmall;
    [SerializeField] private GameObject _child;
    [SerializeField] private GameObject _counter;
    private TextMeshProUGUI _counterText;
    private int _counterInt;
    private bool _isDestroyed;


    // Start is called before the first frame update
    void Start()
    {
        _isDestroyed = false;
        _counterText = _counter.GetComponent<TextMeshProUGUI>();
        
        _counterInt = _isSmall ? Random.Range(2, 6) : Random.Range(LevelDifficulty.minTargetReq, LevelDifficulty.maxTargetReq);
        _counterText.text = "" +_counterInt;

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("missile"))
        {
            Missile m = other.GetComponent<Missile>();
          
            MissilesManager.Instance.destroyMissile(m);
            _counterInt -= 1;
            _counterText.text = "" + _counterInt;
            if (_counterInt <= 0)
            {
                _counter.SetActive(false);
                Kaboom();
            }
        }else if (other.tag.Equals("player"))
        {
            if(!_isDestroyed)
            {
                StateManager.instance.killPlayer();
            }
        }
    }


    private void Kaboom()
    {
        
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 10);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Vector3 force = Vector3.forward * Random.Range(0, 10); 
            force += Vector3.up * Random.Range(0, 3);
            
            if (rb != null && !_isDestroyed && !rb.tag.Equals("player") && !rb.tag.Equals("missile"))
            {
                rb.constraints = RigidbodyConstraints.None;
                //rb.AddExplosionForce(200f, transform.position, 10f);

                rb.AddRelativeForce(force, ForceMode.Impulse);

            }
               
        }
        _isDestroyed = true;

    }


    public Transform getRandomChild()
    {
        int i = Random.Range(0, _child.transform.childCount);
        return _child.transform.GetChild(i).transform;
    }
}
