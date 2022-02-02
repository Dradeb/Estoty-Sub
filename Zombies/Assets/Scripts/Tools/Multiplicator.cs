using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Multiplicator : MonoBehaviour
{



    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _number;
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] bool _spawned;
    [SerializeField] bool _add;



    // Start is called before the first frame update
    void Start()
    {
        _numberText = GetComponentInChildren<TextMeshProUGUI>();
          _spawned = false;
        _numberText.text = _add ? "+ " + _number : "x " + _number;
    }

    public bool isAdd()
    {
        return _add;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("player") && !_spawned)
        {
            StartCoroutine(spawnClones());
            _spawned = true;
        }
    }



    public IEnumerator spawnClones()
    {
        int number = 0;
        if (_add)
        {
            number =  _number;
        }
        else
        {
           number  = (_number * LevelManager.Instance.enemiesNumber) - LevelManager.Instance.enemiesNumber;
        }
        
        Debug.Log(number);
        for(int i = 0;i < number;i++)
        {
            Vector3 lastpos = Player.Instance.transform.position - Player.Instance.transform.forward;
            if (LevelManager.Instance.enemies.Count > 0)
            {
                lastpos = LevelManager.Instance.enemies[LevelManager.Instance.enemies.Count - 1].transform.position;
            }
            else
            {
                LevelManager.Instance.enemies = new List<GameObject>();
            }
            
            GameObject a = Instantiate(_enemyPrefab, lastpos, transform.rotation); ;
            LevelManager.Instance.enemies.Add(a);
            LevelManager.Instance.enemiesNumber++;
            
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.SetActive(false);
       
    }
}
