using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploser : MonoBehaviour
{

    [SerializeField] int _numberToKill;
    [SerializeField] GameObject _explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            kaboom(_numberToKill,other.gameObject);

        }else if (other.tag.Equals("player"))
        {
            kaboom(1,null);
            other.GetComponent<PlayerMovement>().Die();
            other.GetComponent<PlayerMovement>().enabled= false;
        }
    }

    public void kaboom(int max , GameObject victim = null)
    {

        Vector3 explosionPos = transform.position;
       

        Debug.Log("DIEEE");

        if (_numberToKill <= 0 || (victim != null && victim.GetComponent<EnemyMovement>().isDead) )
            return;


        _explosion.SetActive(false);
        _explosion.transform.position = transform.position;
        _explosion.SetActive(true);

        if (victim == null)
            return;


        Rigidbody rb = victim.GetComponent<Rigidbody>();
        Vector3 force = Vector3.right * Random.Range(-10, 10);
        force += Vector3.up * Random.Range(10, 12);
        rb.constraints = RigidbodyConstraints.None;
        rb.AddRelativeForce(force, ForceMode.Impulse);
        rb.GetComponent<Animator>().SetBool("isMoving", false);
        //rb.GetComponent<Animator>().SetBool("Dead", true);
        rb.GetComponent<EnemyMovement>().isDead = true;
        rb.AddTorque(new Vector3(5f, 5f, 5f));


        _numberToKill--;
        LevelManager.Instance.enemies.Remove(victim);
        LevelManager.Instance.enemiesNumber--;

        LevelManager.Instance.numberKilled++;
        LevelManager.Instance.oneIsKilled();

        this.gameObject.SetActive(false);



        
    }


}
