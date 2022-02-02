using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    [SerializeField] private Transform _target; 

    [Header("Missile Properties")]
    [SerializeField] private float missileSpeed ; 
    [SerializeField] private float rotationSpeed;

    Vector3 targetPosition; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void unlockTarget()
    {
        this._target = null;
    }    

    // Update is called once per frame
    void Update()
    {
        if(!MissilesManager.Instance._missilesReady)
        {
            return;
        }

        if(_target)
        {
            float _distance = Vector3.Distance(transform.position, _target.position);
            //float x = Mathf.Sin(Time.time * (frequency)) * (amplitude + Random.Range(1, 4)) ;

            
           
            if (_distance <= 1f)
            {
                Debug.Log("TARGET LOCKED ");
                transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * missileSpeed);
                Vector3 relativePosition = _target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePosition, transform.forward);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed * 10f);
            }
            else
            {
                //loopRotation();
                transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * missileSpeed);
                Vector3 relativePosition = (_target.position + new Vector3(0f, 0f, 0f)) - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * (rotationSpeed));

                //Add random slight rotation 
                //transform.localEulerAngles += ;
            }

            

        }


    }


    public void lockTarget(Transform target)
    {
        this._target = target;
        randomizeInitRotation();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("cube"))
            gameObject.SetActive(false);
    }

    void randomizeInitRotation()
    {
        rotationSpeed = Random.Range(3f, 4f);
        transform.position = MissilesManager.Instance.missilesOrigin.position; 
        Vector3 relativePosition = _target.position - transform.position;
        float distance = Vector3.Distance(transform.position, _target.position);
        Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.forward);
        transform.rotation = rotation;
        float randomX = distance <2f ? Random.Range(0f, 10f) : Random.Range(0f, 4f);
        float randomY = distance < 2f ? Random.Range(-10f, 10f) : Random.Range(-20f, 20f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(-randomX, -randomY, 0f));
    }
}
