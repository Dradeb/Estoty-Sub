using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissileTrigger : MonoBehaviour
{

    public int missilesAmount;
    public GameObject target;

    private Animator _animator;
    [SerializeField] TextMeshProUGUI text; 


    private void Start()
    {
        missilesAmount = Random.Range(LevelDifficulty.minTargetReq, LevelDifficulty.maxTargetReq);
        _animator = GetComponent<Animator>();
        text.text = "" + missilesAmount;
    }
    public void launch()
    {
        for( int i =0; i < missilesAmount; i++)
        {
            Target t = target.GetComponent<Target>();
            MissilesManager.Instance.FireMissile(t.getRandomChild());
        }
       
    }

    public void setTarget(GameObject t)
    {
        this.target = t; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            if(_animator)
            {
                _animator.Play("triggered");
            }
            launch();
        }
    }

}
