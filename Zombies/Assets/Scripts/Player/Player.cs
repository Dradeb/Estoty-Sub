using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance;
    public Transform spawnPos; 
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

}
