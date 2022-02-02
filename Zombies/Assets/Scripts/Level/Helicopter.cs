using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{

   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("player"))
        {
            other.gameObject.SetActive(false);
            GetComponent<Animator>().SetBool("fly",true);
            StartCoroutine(LevelManager.Instance.nextLevel());
        }
    }
}
