using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    private bool _isDead;
    public static StateManager instance; 
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    
    public void killPlayer()
    {
        _isDead = true;
        GetComponent<Movement>().killPlayer();
        StartCoroutine(reloadLevel());

    }

    IEnumerator reloadLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
