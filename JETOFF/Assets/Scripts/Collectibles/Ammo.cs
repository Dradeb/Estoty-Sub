using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    [SerializeField] int _amount;
    [SerializeField] TextMeshProUGUI _amountText;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _amount = Random.Range(LevelDifficulty.minAmmoAmount, LevelDifficulty.maxAmmoAmount);
        _amountText.text = _amount + "";
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (_animator)
            {
                MissilesManager.Instance.missileAquired(_amount);
                gameObject.SetActive(false);
            }
           
        }
    }
}
