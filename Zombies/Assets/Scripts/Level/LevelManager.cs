using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    public int enemiesNumber;
    public int totalToKill;
  
    public int numberKilled;
    UIManager uiManagerInstance;

    [SerializeField] List<Transform> spawnPos;
    public List<GameObject> enemies;

    [SerializeField] private GameObject _winDoor;



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        uiManagerInstance = FindObjectOfType<UIManager>();
        uiManagerInstance.initUI();
        uiManagerInstance.initIntro(totalToKill);
     
        
    }

    // Update is called once per frame
   

    public Vector3 getRandomSpawnPos()
    {
        Vector3 pos = spawnPos[Random.Range(0, spawnPos.Count)].position; 
        return  new Vector3(pos.x, Player.Instance.transform.position.y,pos.z);
    }

    public void oneIsKilled()
    {
        UIManager.Instance.startFilling();
        checkIfLost();
    }

    public void checkIfLost()
    {
        if (numberKilled >= totalToKill)
        {

            Debug.Log("GG");
            _winDoor.GetComponent<Animator>().Play("open");

            return;
        }
        if (enemiesNumber <=0 )
        {
             Multiplicator[] add = FindObjectsOfType<Multiplicator>();
            foreach(Multiplicator m in add)
            {
                if (m.isAdd())
                    return;
            }

            StartCoroutine(reset());
           

        }
    }

    public IEnumerator reset()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public IEnumerator nextLevel()
    {

        yield return new WaitForSeconds(4f);
        if (SceneManager.GetActiveScene().buildIndex >= 4)
        {
            SceneManager.LoadScene(0);
        }
        else
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
