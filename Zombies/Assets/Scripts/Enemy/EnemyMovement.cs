using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Transform _target;
    private Quaternion _lookRot;
    private Animator _anim;
    private float _distance;
    private float _randomDistance;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    private bool _isMoving;
    private float _tempSpeed;
    public bool isDead; 



    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(Player.Instance == null)
            yield return new WaitForSeconds(1f);
        _tempSpeed = _speed;
        _isMoving = true;
        _target = Player.Instance.transform;
        _anim = gameObject.GetComponent<Animator>();
        _randomDistance = Random.Range(1f, 1.3f);
        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (!_target || isDead )
            return;

        _distance = Vector3.Distance(transform.position, _target.position);
        if(_distance<= 0.2f && _target.gameObject.activeSelf)
        {
            _speed = 0f;
            _isMoving = false;

            if(Player.Instance.GetComponent<PlayerMovement>() != null)
                Player.Instance.GetComponent<PlayerMovement>().Die();

        }
        else
        {
            _speed = _tempSpeed;
            _isMoving = true;
        }

        if (_anim)
        {
            _anim.SetBool("isMoving", _isMoving);
        }

        _lookRot = Quaternion.LookRotation(_target.position - transform.position , Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookRot, _rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward  * Time.deltaTime * _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("enemy"))
        {
            _isMoving = false;
               _speed = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("enemy") && _distance > _randomDistance)
        {
            _isMoving = true;
               _speed = _tempSpeed;
        }
    }

    public void Die()
    {


        LevelManager.Instance.enemies.Remove(this.gameObject);
        LevelManager.Instance.enemiesNumber--;

        LevelManager.Instance.numberKilled++;
        LevelManager.Instance.oneIsKilled();

        this.gameObject.SetActive(false);
    }
}
