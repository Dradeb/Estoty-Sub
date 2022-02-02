using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActivation : MonoBehaviour
{

    public float delay;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Animator>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
