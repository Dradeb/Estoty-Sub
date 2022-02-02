using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("enemy"))
        {
            EnemyMovement em = other.GetComponent<EnemyMovement>();
            em.Die();
        }
        else if(other.gameObject.tag.Equals("player"))
        {
            other.GetComponent<PlayerMovement>().Die();
            other.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
