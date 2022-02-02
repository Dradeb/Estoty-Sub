using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    [SerializeField] private bool _isDead; 

    [SerializeField] private float _camZ;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minX;


    Vector3 lastMousePos;
    Vector3 mousePos;
    Vector3 mousePosTrans;
    Vector3 rot;

    [SerializeField] float _swipeSpeed;
    [SerializeField] float _rotationSpeed;


    private Rigidbody _rigidbody;

    float offset;

    // Start is called before the first frame update
    void Start()
    {
        _isDead = false; 
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {   
        if(!_isDead)
        {

            if (Input.GetMouseButtonDown(0))
            {
                lastMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camZ));
            }
            else if (Input.GetMouseButton(0))
            {

                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camZ));
                offset = mousePos.x - lastMousePos.x;
                mousePosTrans = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
                lastMousePos = Vector3.Lerp(lastMousePos, mousePos, Time.deltaTime * _swipeSpeed);

                rot = new Vector3(0f, 0f, -10f * offset);


            }
            else
            {
                rot = Vector3.zero;
            }


        }
        else
        {

            _rigidbody.constraints = RigidbodyConstraints.None;
            _rotationSpeed = Mathf.Lerp(_rotationSpeed, 0f, Time.deltaTime * 0.5f);
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 0.5f);
            _swipeSpeed = Mathf.Lerp(_swipeSpeed, 0f, Time.deltaTime * 0.5f);
        }




        //Controlling SideWays Movement
        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(mousePosTrans.x, _minX, _maxX), transform.position.y, transform.position.z), Time.deltaTime * _swipeSpeed);
        offset = Mathf.Lerp(offset, 0f, Time.deltaTime * speed);

        //Controlling Forward Movement
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * speed);


        //Lerping Rotation For Smooth Effect
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rot), Time.deltaTime * _rotationSpeed);


    }

    public void killPlayer()
    {
        if(_rigidbody)
        {
            _rigidbody.constraints = RigidbodyConstraints.None;

        }
        _isDead = true;

    }
}
